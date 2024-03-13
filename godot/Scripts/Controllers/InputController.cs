using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public partial class InputController : Node
{
    public double DOUBLETAP_MSECS = 500;
    private readonly HashSet<string> holdableActions = new() { "pan_left", "pan_right", "pan_up", "pan_down" };

    private HashSet<string> heldActions = new();

    [Export]
    private CameraController cameraController;

    public override void _Ready(){

    }


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

    public override void _Process(double delta)
    {
        cameraController.CameraInput(cameraController.camera, delta);
    }
}
