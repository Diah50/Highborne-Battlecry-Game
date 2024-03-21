using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Manager
{
    public partial class CameraM : Node2D
    {
        [Export] public double ZOOM_SPEED = 5; // in current zoom per second
        [Export] public int MIN_ZOOM = 1; // higher zoom = more zoomed in
        [Export] public int MAX_ZOOM = 100;
        [Export] public double PAN_SPEED = 300; // in units per second
        [Export] public Camera2D camera;

        private InputM inputM;

        public override void _Ready()
        {
            inputM = this.GetNode<InputM>("../InputM");
            camera = this.GetNode<Camera2D>("Camera2D");
        }

        public void InputProcess(double delta)
        {
            float panIncrement = (float)(PAN_SPEED * delta / camera.Zoom.X);
            float zoomIncrement = (float)(ZOOM_SPEED * delta);

            var pan = new Godot.Vector2(
                ((inputM.heldActions.Contains("pan_right") ? 1 : 0) - (inputM.heldActions.Contains("pan_left") ? 1 : 0)) * panIncrement,
                ((inputM.heldActions.Contains("pan_down") ? 1 : 0) - (inputM.heldActions.Contains("pan_up") ? 1 : 0)) * panIncrement);
            if (pan != Godot.Vector2.Zero)
                camera.Position += pan;

            float zoomMult = 1f +
                ((Input.IsActionJustPressed("zoom_in") ? 1 : 0) - (Input.IsActionJustPressed("zoom_out") ? 1 : 0))
                * zoomIncrement;
            if (zoomMult != 1f)
            {
                float zoom = Mathf.Clamp(camera.Zoom.X * zoomMult, MIN_ZOOM, MAX_ZOOM);
                camera.Zoom = new Godot.Vector2(zoom, zoom);
            }
        }
    }
}
