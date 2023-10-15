using Highborne.Common.Interfaces;
using UnityEngine;

namespace Highborne.Common.EventBus.Events.Input
{
    public sealed record MovementInputEvent(Vector2 MoveInput) : ISignalEvent
    {
        public Vector2 MoveInput { get; set; } = MoveInput;
    }
}