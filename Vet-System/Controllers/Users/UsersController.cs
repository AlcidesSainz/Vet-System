using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vet_Application.DTOs.Response;
using Vet_Application.DTOs.UsersEntity;
using Vet_Application.Resources;
using Vet_Application.Utilities;
using Vet_Infrastructure.Data;
using Vet_System.Services.DTOs.Response;

namespace Vet_System.Controllers.Users
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;

        public UsersController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration,ApplicationDbContext applicationDbContext,IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
        }

        [HttpGet("UserList")]
        public async Task<ActionResult<List<UserResponseDTO>>> UserList([FromQuery] PaginationResponseDTO paginationResponseDTO)
        {
             var queryable = applicationDbContext.Users.AsQueryable();
            await HttpContext.AddPaginationHeader(queryable);
            var users = await queryable.ProjectTo<UserResponseDTO>(mapper.ConfigurationProvider)
                .OrderBy(x=>x.Email)
                .Paginate(paginationResponseDTO)
                .ToListAsync();
            return users;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthenticationResponseDTO>> Register(UserCredentialsDTO userCredentialsDTO)
        {
            var user = new IdentityUser
            {
                UserName = userCredentialsDTO.Email,
                Email = userCredentialsDTO.Email
            };
            var result = await userManager.CreateAsync(user, userCredentialsDTO.Password);
            if (result.Succeeded)
            {
                return await BuildToken(user);
            }
            return BadRequest(result.Errors);
        }
        [HttpPost("login")]
        [AllowAnonymous]

        public async Task<ActionResult<AuthenticationResponseDTO>> Login(UserCredentialsDTO userCredentialsDTO)
        {
            var user = await userManager.FindByEmailAsync(userCredentialsDTO.Email);
            if (user is null)
            {
                var errors = BuildWrongLogin();
                return BadRequest(errors);
            }
            var result = await signInManager.CheckPasswordSignInAsync(user, userCredentialsDTO.Password, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return await BuildToken(user);
            }
            else
            {
                var errors = BuildWrongLogin();
                return BadRequest(errors);
            }
        }
        [HttpPost("make-admin")]
        public async Task<IActionResult> MakeAdmin(EditClaimDTO editClaimDTO)
        {
            var user = await userManager.FindByEmailAsync(editClaimDTO.Email);
            if (user is null)
            {
                return NotFound();
            }
            await userManager.AddClaimAsync(user, new Claim("IsAdmin", "true"));
            return NoContent();
        }
        [HttpPost("remove-admin")]
        public async Task<IActionResult> RemoveAdmin(EditClaimDTO editClaimDTO)
        {
            var user = await userManager.FindByEmailAsync(editClaimDTO.Email);
            if (user is null)
            {
                return NotFound();
            }
            await userManager.RemoveClaimAsync(user, new Claim("IsAdmin", "true"));
            return NoContent();
        }

        private IEnumerable<IdentityError> BuildWrongLogin()
        {   
            var identityError = new IdentityError() { Description = Messages.ERROR_WRONG_LOGIN };
            var errors = new List<IdentityError>();
            errors.Add(identityError);
            return errors;
        }

        private async Task<AuthenticationResponseDTO> BuildToken(IdentityUser identityUser)
        {
            var claims = new List<Claim>
            {
                new Claim("email", identityUser.Email!),
                new Claim("any", "any value")
            };
            var claimsDB = await userManager.GetClaimsAsync(identityUser);
            claims.AddRange(claimsDB);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTkey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddYears(1);
            var securityToken = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );
            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return new AuthenticationResponseDTO
            {
                Token = token,
                Expiration = expiration
            };
        }
    }
}
