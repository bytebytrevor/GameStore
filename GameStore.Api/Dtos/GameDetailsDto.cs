using System.Security.Cryptography.X509Certificates;

namespace GameStore.Api.Dtos;

public record class GameDetailsDto(
    int Id,
    string Name,
    int GenreId,
    decimal Price,
    DateOnly RealeaseDate);