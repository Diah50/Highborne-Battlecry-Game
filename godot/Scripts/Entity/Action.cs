using System;
using Godot;

public enum ActionType { Move };

namespace Entity
{
    public partial class Action
    {
        public ActionType Type;
        public Godot.Vector2 VectorTarget;
        public Entity EntityTarget;
    }
}