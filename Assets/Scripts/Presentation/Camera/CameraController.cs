using Highborne.Common.EventBus;
using Highborne.Common.EventBus.Events.Input;
using UnityEngine;
using Zenject;

namespace Highborne.Presentation.Input
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float _cameraMoveSpeed = 10f;
        [SerializeField] private float _cameraSmoothness = 5f;

        private IEventBus _eventBus;
        private Vector2 targetPosition = new(0, 0);

        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;

            _eventBus.Subscribe<MovementInputEvent>(MovementInputEventHandler);
        }

        private void MovementInputEventHandler(MovementInputEvent movementInputEvent)
        {
            var moveDelta = new Vector2(movementInputEvent.MoveInput.x, movementInputEvent.MoveInput.y);
            targetPosition += _cameraMoveSpeed * Time.deltaTime * moveDelta;
        
            float distanceToTarget = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), targetPosition);

            if (distanceToTarget <= 0.001f) 
            {
                transform.position = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
            }
            else 
            {
                var lerpPosition = Vector2.Lerp(new Vector2(transform.position.x, transform.position.y), targetPosition, _cameraSmoothness * Time.deltaTime);
                transform.position = new Vector3(lerpPosition.x, lerpPosition.y, transform.position.z);
            }
        }
    }
}