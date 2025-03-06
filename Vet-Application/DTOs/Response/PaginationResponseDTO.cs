namespace Vet_System.Services.DTOs.Response
{
    public class PaginationResponseDTO
    {
        public int Page { get; set; } = 1;
        private int recordsPerPage = 10;
        private readonly int maxRecsPerPage = 50;

        public int RecordsPerPage
        {
            get { return recordsPerPage; }
            set
            {
                recordsPerPage = (value > maxRecsPerPage) ? maxRecsPerPage : value;
            }
        }

    }
}
