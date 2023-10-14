using Highborne.Common.EventBus;
using Highborne.Common.EventBus.Events.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Highborne.Presentation.Input
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputHandler : MonoBehaviour
    {
        private InputActions _inputs;
        private IEventBus _eventBus;

        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            _inputs = new InputActions();

            _inputs.Default.LeftClick.performed += LeftClickPerformed;
            _inputs.Default.LeftClick.canceled += LeftClickReleased;

            _inputs.Default.RightClick.performed += RightClickPerformed;
            _inputs.Default.RightClick.canceled += RightClickReleased;

            _inputs.Default.Enable();
        }

        private void Update()
        {           
            _eventBus.Fire(new MovementInputEvent(_inputs.Default.Move.ReadValue<Vector2>()));
        }

        private void LeftClickPerformed(InputAction.CallbackContext obj)
        {
            //_eventBus.Fire(new LeftClickPerformedEvent());
        }

        private void LeftClickReleased(InputAction.CallbackContext obj)
        {
            // _eventBus.Fire(new LeftClickReleasedEvent());
        }

        private void RightClickPerformed(InputAction.CallbackContext obj)
        {
            // _eventBus.Fire(new RightClickPerformedEvent());
        }

        private void RightClickReleased(InputAction.CallbackContext obj)
        {
            //_eventBus.Fire(new RightClickReleasedEvent());
        }

    }
}