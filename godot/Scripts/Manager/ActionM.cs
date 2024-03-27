using EntityNS;
using EntityComponent;
using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;

namespace Manager
{
    public partial class ActionM : Node2D
    {
        private SelectionM selectionM;
        private InputM inputM;
        private MovementM movementM;

        public override void _Ready()
        {
            selectionM = GetNode<SelectionM>("../SelectionM");
            inputM = this.GetNode<InputM>("../InputM");
            movementM = this.GetNode<MovementM>("../MovementM");
        }

        public void HandleInput(InputEvent @event)
        {
            if (@event.IsActionPressed("right_click"))
            {
                selectionM.selected.ForEach(entity =>
                {
                    var movementC = Util.Util.TryGetComponent<MovementC>(entity);
                    
                    if (movementC != null)
                    {
                        if (!inputM.heldActions.Contains("shift"))
                            entity.ActionQueue.Clear();
                        var moveAction = new EntityAction
                        {
                            VectorTarget = GetGlobalMousePosition(),
                            Type = ActionType.Move
                        };
                        entity.ActionQueue.Add(moveAction);
                    }
                });
            }
        }

        public override void _Process(double delta)
        {
            foreach (Entity entity in GetTree().GetNodesInGroup("Entity").Select(x => (Entity)x))
            {
                var nextAction = entity.ActionQueue.FirstOrDefault();
                if (nextAction != null)
                {
                    switch (nextAction.Type)
                    {
                        case ActionType.Move:
                            if (movementM.ProcessMovementAction(nextAction, entity, delta))
                            {
                                entity.ActionQueue.RemoveAt(0);
                            }
                            break;
                        default:
                            GD.PushError("Action behavior not implemented", nextAction.Type);
                            break;
                    }
                }
                var nextShopAction = entity.ShopActionQueue.FirstOrDefault();
                if (nextShopAction != null)
                {
                    // switch (nextShopAction.Type){
                    //     default:
                    GD.PushError("Action behavior not implemented", nextShopAction.Type);
                    // }
                }
            }
        }
    }
}