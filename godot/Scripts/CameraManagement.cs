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
		pfps = (int) ProjectSettings.GetSetting("physics/common/physics_fps");
		pdelta = 1.0/pfps;
		camera = GetNode<Camera2D>("Camera2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		CameraInput(camera, delta);
	}

	public void CameraInput(Camera2D c, double delta){
		var pan = (float) (PAN_SPEED * c.Zoom.X * delta);
		var zoom = (float) (ZOOM_SPEED * delta);
		if (Input.IsActionPressed("pan_left"))
			c.MoveLocalX(-pan);
		if (Input.IsActionPressed("pan_right"))
			c.MoveLocalX(pan);
		if (Input.IsActionPressed("pan_up"))
			c.MoveLocalY(-pan);
		if (Input.IsActionPressed("pan_down"))
			c.MoveLocalY(pan);
		if (Input.IsActionJustReleased("zoom_in"))
			Zoom(zoom, c);
		if (Input.IsActionJustReleased("zoom_out"))
			Zoom(-zoom, c);
	}

	private void Zoom(float i, Camera2D c){
		float scale = 1 + i;
		camera.Zoom = new Godot.Vector2(camera.Zoom.X * scale, camera.Zoom.Y * scale);
	}
}
