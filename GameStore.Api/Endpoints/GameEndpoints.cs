using System;
using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GameEndpoints
{
    private static string GetGameEndpointName = "GetGame";

    private static List<GameDto> games = [
        new(
            1,
            "Batman Arkam City",
            "Superhero",
            19.99M,
            new DateOnly(1990, 12, 24)
        ),
        new(
            2,
            "Lost",
            "Adventure",
            29.99M,
            new DateOnly(1998, 06, 13)
        ),
        new(
            3,
            "John Wick",
            "Fighting",
            68.99M,
            new DateOnly(1990, 12, 24)
        )
    ];    

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();

        // GET /games/
        group.MapGet("/", () => games);

        // GET /games/id
        group.MapGet("/{id}", (int id) =>
        {
            var game = games.Find(game => game.Id == id);

            if (game is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(game);
        }).WithName(GetGameEndpointName);

        // PUT/games/id
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            int index = games.FindIndex(game => game.Id == id);

            if (index == -1)
                return Results.NotFound();

            games[index] = new GameDto(
                updatedGame.Id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate);

            return Results.NoContent();
        });

        // POST /games/
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate);

            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });

        // DELETE /games/id
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });

        return group;
    }
}
