using System;

namespace GameStore.Api.Entities;

public class Game
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int GenreId { get; set; }
    public string Genre { get; set; }
    public decimal Price { get; set; }
    public DateOnly ReleaseDate { get; set; }
}