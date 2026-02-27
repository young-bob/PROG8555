using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Models
{
    // TODO: Bo Yang
    // TODO: 9086117
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z].*", ErrorMessage = "Title must start with a capital letter")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Range(1900, 2025)]
        [Display(Name = "Release Year")]
        public string ReleaseYear { get; set; } = string.Empty;

        [Required]
        [RegularExpression("Action|Comedy|Drama|Horror|SciFi", ErrorMessage = "Genre must be one of: Action, Comedy, Drama, Horror, SciFi")]
        public string Genre { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Image URL")]
        public string ImgUrl { get; set; } = string.Empty;
    }
}
