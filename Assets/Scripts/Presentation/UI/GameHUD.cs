using Highborne.Common.EventBus;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Highborne.Presentation.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class GameHUD : MonoBehaviour
    {
        [SerializeField] private UIDocument _document;
        [SerializeField] private StyleSheet _styleSheet;

        private IEventBus _eventBus;
        private VisualElement _gameHUD;

        [Inject]
        public void Construct(IEventBus eventBus)     
        {
            _eventBus = eventBus;
        }
    }
}