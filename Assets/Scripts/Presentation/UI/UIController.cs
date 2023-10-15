using Highborne.Common.EventBus;
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

        private IEventBus _eventBus;

        [Inject]
        public void Construct(IEventBus eventBus)     
        {
            _eventBus = eventBus;
        }

        private void Start()
        {
            _eventBus.Fire(new ShowMainMenuEvent());
        }
    }
}