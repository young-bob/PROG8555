using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Models.DTOs
{
    public class MovieCreateDto
    {
        [Required]
        [RegularExpression(@"^[A-Z].*", 
        ErrorMessage = "Title must start with a capital letter")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Range(1900, 2025)]
        [Display(Name = "Release Year")]
        public string ReleaseYear { get; set; } = string.Empty;

        [Required]
        [RegularExpression("^(Action|Comedy|Drama|Horror|SciFi)$", 
        ErrorMessage = "Genre must be one of: Action, Comedy, Drama, Horror, SciFi")]
        public string Genre { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Image URL")]
        public string ImgUrl { get; set; } = string.Empty;
    }
}
