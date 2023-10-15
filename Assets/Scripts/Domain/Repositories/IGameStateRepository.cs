using Highborne.Domain.Entities;

namespace Highborne.Domain.Repository
{
    public interface IGameStateRepository
    {
        GameState GetGameState();
        public void IncrementMatchTimer(float increment);
    }
}
