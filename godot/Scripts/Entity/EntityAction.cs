using System;
using Godot;

public enum ActionType { Move };

namespace EntityNS
{
    public partial class EntityAction
    {
        public ActionType Type;
        public Godot.Vector2 VectorTarget;
        public Entity EntityTarget;
    }
}