using System;
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GameEndpoints
{
    const string GetGameEndpointName = "GetGame";


    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();

        // GET /games/
        group.MapGet("/", async (GameStoreContext dbContext) =>
            await dbContext.Games
                .Include(game => game.Genre)
                .Select(Game => Game.ToSummaryDto())
                .AsNoTracking()
                .ToListAsync());

        // GET /games/id
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            Game? game = await dbContext.Games.FindAsync(id);


            return game is null ?
                Results.NotFound() : Results.Ok(game.ToDetailsDto());

        }).WithName(GetGameEndpointName);

        // PUT/games/id
        group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
        {
            var existingGame = await dbContext.Games.FindAsync(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingGame)
                .CurrentValues
                .SetValues(updatedGame.ToEntity(id));

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        // POST /games/
        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            Game game = GameMapping.ToEntity(newGame);
            dbContext.Games.Add(game);

            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(
                GetGameEndpointName,
                new { id = game.Id },
                game.ToDetailsDto());
        });

        // DELETE /games/id
        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            await dbContext.Games.Where(game => game.Id == id)
                .ExecuteDeleteAsync();

            return Results.NoContent();
        });

        return group;
    }
}
