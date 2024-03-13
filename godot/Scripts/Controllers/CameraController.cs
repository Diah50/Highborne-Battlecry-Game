using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public partial class CameraController : Node
{
	[Export]
	public double ZOOM_SPEED = 5; // in current zoom per second
	[Export] 
	public int MIN_ZOOM = 1; // higher zoom = more zoomed in
	[Export] 
	public int MAX_ZOOM = 100;
	[Export] 
	public double PAN_SPEED = 300; // in units per second
	[Export] 
	public double DOUBLETAP_MSECS = 500;
	[Export] 
	public Camera2D camera;
	private readonly HashSet<string> holdableActions = new() { "pan_left", "pan_right", "pan_up", "pan_down" };

	private HashSet<string> heldActions = new();


	public override void _Input(InputEvent @event)
	{
		// reduces ghosting on held actions compared to Input.IsActionPressed()
		// not necessary for presses, just use Input.IsActionJustPressed()
		var newlyHeldActions = holdableActions.Except(heldActions).Where(action => @event.IsActionPressed(action));
		heldActions = heldActions.Concat(
				newlyHeldActions
			)
			.Where(action => !@event.IsActionReleased(action))
			.ToHashSet();
	}

	public void CameraInput(Camera2D c, double delta)
	{
		float panIncrement = (float)(PAN_SPEED * delta / c.Zoom.X);
		float zoomIncrement = (float)(ZOOM_SPEED * delta);

		var pan = new Godot.Vector2(
			((heldActions.Contains("pan_right") ? 1 : 0) - (heldActions.Contains("pan_left") ? 1 : 0)) * panIncrement,
			((heldActions.Contains("pan_down") ? 1 : 0) - (heldActions.Contains("pan_up") ? 1 : 0)) * panIncrement);
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