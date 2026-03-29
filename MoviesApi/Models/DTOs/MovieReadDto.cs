namespace MoviesApi.Models.DTOs
{
    public class MovieReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ReleaseYear { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string ImgUrl { get; set; } = string.Empty;
    }
}
