using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Vet_Application.Utilities;
using Vet_Domain.Interfaces;
using Vet_Infrastructure.Data;
using Vet_Infrastructure.Services.Interfaces;
using Vet_System.Services.DTOs.Response;


#pragma warning disable IDE0290 // Use primary constructor
namespace Vet_System.Controllers.Base
{
    public class CustomBaseController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;
        private readonly IOutputCacheStore outputCacheStore;
        private readonly string cacheTag;
        private readonly IFileStorage fileStorage;

        public CustomBaseController(ApplicationDbContext applicationDbContext, IMapper mapper, IOutputCacheStore outputCacheStore, string cacheTag, IFileStorage fileStorage)
        {
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
            this.outputCacheStore = outputCacheStore;
            this.cacheTag = cacheTag;
            this.fileStorage = fileStorage;
        }
        //Get
        protected async Task<List<TDTO>> Get<TEntity, TDTO>(PaginationResponseDTO paginationResponseDTO, Expression<Func<TEntity, object>> orderBy)
            where TEntity : class
        {
            var queryable = applicationDbContext.Set<TEntity>().AsQueryable();
            await HttpContextExtensions.AddPaginationHeader(HttpContext, queryable);
            return await queryable
                .OrderBy(orderBy)
                .Paginate(paginationResponseDTO)
                .ProjectTo<TDTO>(mapper.ConfigurationProvider).ToListAsync();
        }
        //Get by id
        protected async Task<ActionResult<TDTO>> Get<TEntity, TDTO>(int id)
            where TEntity : class, IId
            where TDTO : IId
        {
            var entity = await applicationDbContext.Set<TEntity>()
                .ProjectTo<TDTO>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (entity is null)
            {
                return NotFound();
            }
            return entity;
        }
        //Get by id with include
        protected async Task<ActionResult<TDTO>> Get<TEntity, TDTO>(
            int id,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null!)
            where TEntity : class, IId
            where TDTO : IId
        {
            var query = applicationDbContext.Set<TEntity>().AsQueryable();

            if (include != null)
            {
                query = include(query);
            }

            var entity = await query
                .ProjectTo<TDTO>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == id);

            return entity is null ? NotFound() : entity;
        }
        //Post
        protected async Task<IActionResult> Post<TRequestDTO, TEntity, TDTO>(TRequestDTO requestDTO, string pathName)
            where TEntity : class, IId
        {
            var entity = mapper.Map<TEntity>(requestDTO);
            applicationDbContext.Add(entity);
            await applicationDbContext.SaveChangesAsync();
            await outputCacheStore.EvictByTagAsync(cacheTag, default);
            var entityDTO = mapper.Map<TDTO>(entity);
            return CreatedAtRoute(pathName, new { id = entity.Id }, entityDTO);
        }
        //Post with image
        protected async Task<IActionResult> PostWithImage<TRequestDTO, TEntity, TDTO>(TRequestDTO requestDTO, Func<TRequestDTO, IFormFile?>
            getFileFunc, string containerName, string pathName)
            where TEntity : class, IId
        {
            var entity = mapper.Map<TEntity>(requestDTO);

            var file = getFileFunc(requestDTO);
            if (file is not null)
            {
                var url = await fileStorage.Store(containerName, file);
                SetFileUrl(entity, url);
            }

            applicationDbContext.Add(entity);
            await applicationDbContext.SaveChangesAsync();

            await outputCacheStore.EvictByTagAsync(cacheTag, default);
            var entityDTO = mapper.Map<TDTO>(entity);
            return CreatedAtRoute(pathName, new { id = entity.Id }, entityDTO);
        }
        //Put
        protected async Task<IActionResult> Put<TUpdateRequestDTO, TEntity>(int id, TUpdateRequestDTO updateRequestDTO)
            where TEntity : class, IId
        {
            var entityExist = await applicationDbContext.Set<TEntity>().AnyAsync(e => e.Id == id);

            if (!entityExist)
            {
                return NotFound();
            }
            var entity = mapper.Map<TEntity>(updateRequestDTO);
            entity.Id = id;
            applicationDbContext.Update(entity);
            await applicationDbContext.SaveChangesAsync();
            await outputCacheStore.EvictByTagAsync(cacheTag, default);
            return NoContent();
        }
        //Put with image
        protected async Task<IActionResult> PutWithImage<TUpdateRequestDTO, TEntity>(int id, TUpdateRequestDTO updateRequestDTO,
            Func<TUpdateRequestDTO, IFormFile?>
            getFileFunc, string containerName)
            where TEntity : class, IId, IHasFileUrl
        {
            var entityExist = await applicationDbContext.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
            if (entityExist == null)
            {
                return NotFound();
            }
            var entity = mapper.Map<TEntity>(updateRequestDTO);
            entity.Id = id;
            var file = getFileFunc(updateRequestDTO);
            if (file is not null)
            {
                var url = await fileStorage.Edit(containerName, file, entityExist.UrlImage);
                SetFileUrl(entity, url);
            }
            applicationDbContext.Update(entity);
            await applicationDbContext.SaveChangesAsync();
            await outputCacheStore.EvictByTagAsync(cacheTag, default);
            return NoContent();
        }
        //Delete
        protected async Task<IActionResult> Delete<TEntity>(int id)
            where TEntity : class, IId
        {
            var entity = await applicationDbContext.Set<TEntity>().Where(c => c.Id == id).ExecuteDeleteAsync();

            if (entity == 0)
            {
                return NotFound();
            }
            await outputCacheStore.EvictByTagAsync(cacheTag, default);
            return NoContent();
        }
        private void SetFileUrl<TEntity>(TEntity entity, string url)
            where TEntity : class
        {
            var property = entity.GetType().GetProperty("UrlImage");
            if (property is not null)
            {
                property.SetValue(entity, url);
            }
        }
    }
}
