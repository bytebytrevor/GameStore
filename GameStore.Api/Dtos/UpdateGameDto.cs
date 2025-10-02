using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record class UpdateGameDto(
    int Id,

    [Required(ErrorMessage = "{0} field is required")]
    [StringLength(50, ErrorMessage = "{0 field cannot exceed {1} characters}")]
    string Name,

    [Required]
    [StringLength(20, ErrorMessage = "{0} field cannot exceed {1} characters")]
    string Genre,
    
    [Range(1, 100, ErrorMessage = "{0} should be between {1} and {2}" )]
    decimal Price,
    DateOnly ReleaseDate);
