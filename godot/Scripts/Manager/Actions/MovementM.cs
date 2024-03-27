using EntityComponent;
using EntityNS;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Manager
{
    public partial class MovementM : Node2D
    {
        // @return whether the action was completed
        public bool ProcessMovementAction(EntityAction action, Entity entity, double delta){
            var target = action.VectorTarget;
            var movementC = Util.Util.TryGetComponent<MovementC>(entity);
            var moveDist = movementC.Speed * delta;
            var remainder = target - entity.GlobalPosition;

            if(remainder.Length() < moveDist){
                entity.GlobalPosition = target;
                return true;
            }else{
                entity.GlobalPosition += (float) moveDist * remainder.Normalized();
                return false;
            }
        }
    }
}