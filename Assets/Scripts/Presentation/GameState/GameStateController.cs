using System.Collections;
using Highborne.Common.EventBus;
using Highborne.Common.EventBus.Events.GameState;
using Highborne.UseCases.GameState.IncrementMatchTimer;
using UnityEngine;
using Zenject;

namespace Highborne.Presentation.GameState
{
    public class GameStateController : MonoBehaviour
    {
        private IEventBus _eventBus;
        private IIncrementMatchTimerUseCase _incrementMatchTimerUseCase;

        [Inject]
        public void Construct(IEventBus eventBus,
            IIncrementMatchTimerUseCase incrementMatchTimerUseCase)
        {
            _incrementMatchTimerUseCase = incrementMatchTimerUseCase;

            _eventBus = eventBus;

            _eventBus.Subscribe<StartMatchEvent>(StartMatch);
            _eventBus.Subscribe<PauseMatchEvent>(PauseMatch);
            _eventBus.Subscribe<ResumeMatchEvent>(ResumeMatch);
            _eventBus.Subscribe<EndMatchEvent>(EndMatch);
        }

        private void StartMatch(StartMatchEvent startMatchEvent) => StartCoroutine(MatchTimer());

        private void PauseMatch(PauseMatchEvent pauseMatchEvent) => StopCoroutine(MatchTimer());

        private void ResumeMatch(ResumeMatchEvent resumeMatchEvent) => StartCoroutine(MatchTimer());

        private void EndMatch(EndMatchEvent endMatchEvent) => StopCoroutine(MatchTimer());

        private IEnumerator MatchTimer()
        {
            yield return new WaitForSeconds(HighborneConstants.GAME_TIME_INCREMENT);
            _incrementMatchTimerUseCase.Handle();
        }
    }
}