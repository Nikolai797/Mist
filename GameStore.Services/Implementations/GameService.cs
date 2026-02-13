#nullable disable

using GameStore.Data.Data;
using GameStore.Data.Entities;
using GameStore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Services.Implementations;

public class GameService : IGameService
{
    private readonly ApplicationDbContext _context;

    public GameService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Game>> GetAllGamesAsync()
    {
        return await _context.Games
            .Include(g => g.Category)
            .ToListAsync();
    }

    public async Task<Game?> GetGameByIdAsync(int id)
    {
        return await _context.Games
            .Include(g => g.Category)
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task CreateGameAsync(Game game)
    {
        _context.Games.Add(game);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateGameAsync(Game game)
    {
        _context.Games.Update(game);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteGameAsync(int id)
    {
        var game = await _context.Games.FindAsync(id);
        if (game != null)
        {
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Game>> GetGamesByCategoryAsync(int categoryId)
    {
        return await _context.Games
            .Where(g => g.CategoryId == categoryId)
            .Include(g => g.Category)
            .ToListAsync();
    }
}
