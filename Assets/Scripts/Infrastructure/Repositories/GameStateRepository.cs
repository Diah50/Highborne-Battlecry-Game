using Highborne.Domain.Entities;
using Highborne.Domain.Repository;

namespace Highborne.Infrastructure.Repositories
{
    public class GameStateRepository : IGameStateRepository
    {
        private readonly GameState _gameState = new();

        public GameState GetGameState() => _gameState;
        public void IncrementMatchTimer(float increment) => _gameState.gameTime += increment;
    }
}