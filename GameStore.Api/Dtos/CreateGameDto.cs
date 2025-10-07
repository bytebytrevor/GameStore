using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record class CreateGameDto(
    int Id,
    [Required(ErrorMessage="{0} field is required")]
    [StringLength(50, ErrorMessage ="{0} field cannot exceed {1} characters")]
    string Name,

    int GenreId,

    [Range(1, 100, ErrorMessage = "{0} range should be between {1} and {2}")]
    decimal Price,

    DateOnly ReleaseDate);
