using Entity;
using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;

namespace Manager
{
    public partial class SelectionM : Node2D
    {
        private InputM inputM;
        private Godot.Vector2 selectionStart = Godot.Vector2.Zero;
        private bool selecting = false;
        private List<Entity.Entity> selected = new();


        public override void _Ready()
        {
            inputM = this.GetNode<InputM>("../InputM");
        }

        public void HandleInput(InputEvent @event)
        {
            if (!selecting && inputM.newlyHeldActions.Contains("left_click"))
            {
                selecting = true;
                selectionStart = GetGlobalMousePosition();
            }
            if (selecting && inputM.justReleasedActions.Contains("left_click"))
            {
                ApplySelectionBox();
                selecting = false;
                QueueRedraw();
            }
        }

        private void ApplySelectionBox(){
            var currentMousePos = GetGlobalMousePosition();

            var space = GetWorld2D().DirectSpaceState;
            var shape = new RectangleShape2D
            {
                Size = (currentMousePos - selectionStart).Abs() / 2
            };
            var query = new PhysicsShapeQueryParameters2D
            {
                Shape = shape,
                CollisionMask = 1,
                Transform = new Transform2D(0, (currentMousePos + selectionStart) / 2)
            };

            foreach(var entity in selected){
                entity.Selected = false;
            }

            var newlySelected = new List<Entity.Entity>();
            var intersectingShapes = space.IntersectShape(query);
            foreach(var intersecting in intersectingShapes){
                intersecting.TryGetValue("collider", out Variant collider);
                var entity = (Entity.Entity)collider;
                newlySelected.Add(entity);
                entity.Selected = true;
            }
            selected = newlySelected;
        }

        public override void _Process(double delta){
            if(selecting)
                QueueRedraw();
        }

        public override void _Draw()
        {
            var currentMousePos = GetGlobalMousePosition();
            if (selecting)
            {
                DrawRect(new Rect2(
                    selectionStart.X,
                    selectionStart.Y,
                    currentMousePos.X - selectionStart.X,
                    currentMousePos.Y - selectionStart.Y
                ), Colors.White, false, 5);
            }
        }
    }
}