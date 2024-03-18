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


        public override void _Ready()
        {
            inputM = this.GetNode<InputM>("../InputM");
        }

        public void SelectionInput(double delta)
        {
            if (inputM.newlyHeldActions.Contains("left_click"))
            {
                selecting = true;
                selectionStart = GetGlobalMousePosition();
            }
            if (inputM.justReleasedActions.Contains("left_click"))
            {
                selecting = false;
                var selectionEnd = GetGlobalMousePosition();
                GD.Print(selectionStart, selectionEnd);
            }
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

        public override void _Process(double delta)
        {
            QueueRedraw();
        }
    }
}