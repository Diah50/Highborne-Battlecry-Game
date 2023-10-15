using Highborne.Common.Interfaces;

namespace Highborne.UseCases.GameState.IncrementMatchTimer
{
    public interface IIncrementMatchTimerUseCase : IUseCase
    {
        bool Handle();
    }
}