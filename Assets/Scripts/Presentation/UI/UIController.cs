using Highborne.Common.EventBus;
using Highborne.Common.EventBus.Events.Input;
using Highborne.Common.EventBus.Events.UI;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Highborne.Presentation.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class UIController : MonoBehaviour
    {
        [SerializeField] private UIDocument _document;
        [SerializeField] private bool _isDevConsoleEnabled = true;

        private IEventBus _eventBus;
        private bool _isDevConsoleVisible = false;

        [Inject]
        public void Construct(IEventBus eventBus)     
        {
            _eventBus = eventBus;

            _eventBus.Subscribe<DevConsolePerformedEvent>(DevConsolePerformedEventHandler);
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<DevConsolePerformedEvent>(DevConsolePerformedEventHandler);
        }

        private void Start()
        {
            _eventBus.Fire(new ShowMainMenuEvent());
        }

        private void DevConsolePerformedEventHandler(DevConsolePerformedEvent devConsolePerformedEvent)
        {
            if (!_isDevConsoleEnabled) return;

            if (_isDevConsoleVisible)
            {
                _eventBus.Fire(new HideDevConsoleEvent());
                _isDevConsoleVisible = false;
            }
            else
            {
                _eventBus.Fire(new ShowDevConsoleEvent());
                _isDevConsoleVisible = true;
            }
        }
    }
}