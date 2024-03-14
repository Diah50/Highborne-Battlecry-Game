using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Manager
{
    public partial class CameraM : Node
    {
        [Export] public double ZOOM_SPEED = 5; // in current zoom per second
        [Export] public int MIN_ZOOM = 1; // higher zoom = more zoomed in
        [Export] public int MAX_ZOOM = 100;
        [Export] public double PAN_SPEED = 300; // in units per second
        [Export] public Camera2D camera;
        private readonly HashSet<string> holdableActions = new() { "pan_left", "pan_right", "pan_up", "pan_down" };

        private InputM inputM;

        public override void _Ready()
        {
            inputM = this.GetNode<InputM>("../InputManager");
            camera = this.GetNode<Camera2D>("Camera2D");
        }

        public void CameraInput(Camera2D c, double delta)
        {
            float panIncrement = (float)(PAN_SPEED * delta / c.Zoom.X);
            float zoomIncrement = (float)(ZOOM_SPEED * delta);

            var pan = new Godot.Vector2(
                ((inputM.heldActions.Contains("pan_right") ? 1 : 0) - (inputM.heldActions.Contains("pan_left") ? 1 : 0)) * panIncrement,
                ((inputM.heldActions.Contains("pan_down") ? 1 : 0) - (inputM.heldActions.Contains("pan_up") ? 1 : 0)) * panIncrement);
            if (pan != Godot.Vector2.Zero)
                c.Position += pan;

            float zoomMult = 1f +
                ((Input.IsActionJustPressed("zoom_in") ? 1 : 0) - (Input.IsActionJustPressed("zoom_out") ? 1 : 0))
                * zoomIncrement;
            if (zoomMult != 1f)
            {
                float zoom = Mathf.Clamp(c.Zoom.X * zoomMult, MIN_ZOOM, MAX_ZOOM);
                c.Zoom = new Godot.Vector2(zoom, zoom);
            }
        }
    }
}
