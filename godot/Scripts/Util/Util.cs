using System;
using Godot;

namespace Util{
    class Util{
        public static T TryGetComponent<T>(Entity.Entity entity) where T : Node2D{
            var componentName = typeof(T).ToString().TrimPrefix("EntityComponent.");
            return entity.HasNode($"Components/{componentName}") ? entity.GetNode<T>($"Components/{componentName}") : null;
        }
    }
}