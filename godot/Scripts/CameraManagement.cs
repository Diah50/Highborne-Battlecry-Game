using Godot;
using System;
using System.Numerics;

public partial class CameraManagement : Node2D
{
    public double ZOOM_SPEED = 5; // in current zoom per second
    public int MIN_ZOOM = 1; // higher zoom = more zoomed in
    public int MAX_ZOOM = 100;
    public double PAN_SPEED = 300; // in units per second
    public double DOUBLETAP_MSECS = 500;

    private int pfps;
    private double pdelta;
    private Camera2D camera;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        pfps = (int)ProjectSettings.GetSetting("physics/common/physics_fps");
        pdelta = 1.0 / pfps;
        camera = GetNode<Camera2D>("Camera2D");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        CameraInput(camera, delta);
    }

    public void CameraInput(Camera2D c, double delta)
    {
        float panIncrement = (float)(PAN_SPEED * delta / c.Zoom.X);
		float zoomIncrement = (float)(ZOOM_SPEED * delta);

        var pan = new Godot.Vector2(
            ((Input.IsActionPressed("pan_right") ? 1 : 0) - (Input.IsActionPressed("pan_left") ? 1 : 0)) * panIncrement,
            ((Input.IsActionPressed("pan_down") ? 1 : 0) - (Input.IsActionPressed("pan_up") ? 1 : 0)) * panIncrement);
        c.Position += pan;

        float zoomChange = 1f + 
			((Input.IsActionJustReleased("zoom_in") ? 1 : 0) - (Input.IsActionJustReleased("zoom_out") ? 1 : 0)) 
			* zoomIncrement;
		float zoom = Mathf.Clamp(c.Zoom.X * zoomChange, MIN_ZOOM, MAX_ZOOM);
		c.Zoom = new Godot.Vector2(zoom, zoom);
    }
}
