using System.Security.Cryptography.X509Certificates;

namespace GameStore.Api.Dtos;

public record class GameDto(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly RealeaseDate);