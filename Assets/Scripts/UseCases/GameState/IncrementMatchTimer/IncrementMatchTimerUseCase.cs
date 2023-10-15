using System;
using Highborne.Common.Logger;
using Highborne.Domain.Repository;

namespace Highborne.UseCases.GameState.IncrementMatchTimer
{
    public sealed class IncrementMatchTimerUseCase : IIncrementMatchTimerUseCase
    {
        private readonly ILoggerService _loggerService;
        private readonly IGameStateRepository _gameStateRepository;

        public IncrementMatchTimerUseCase(ILoggerService loggerService,
            IGameStateRepository gameStateRepository)
        {
            _loggerService = loggerService;
            _gameStateRepository = gameStateRepository;
        }

        public bool Handle()
        {
            try
            {
                _gameStateRepository.IncrementMatchTimer(HighborneConstants.GAME_TIME_INCREMENT);
                return true;
            }
            catch (Exception exception)
            {
                _loggerService.LogException(exception);
                return false;
            }
        }
    }
}