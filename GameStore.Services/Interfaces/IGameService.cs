using GameStore.Data.Entities;

namespace GameStore.Services.Interfaces;

public interface IGameService
{
    Task<IEnumerable<Game>> GetAllGamesAsync();
    Task<Game?> GetGameByIdAsync(int id);
    Task CreateGameAsync(Game game);
    Task UpdateGameAsync(Game game);
    Task DeleteGameAsync(int id);
    Task<IEnumerable<Game>> GetGamesByCategoryAsync(int categoryId);
}
