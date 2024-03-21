using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Manager
{
    public partial class InputM : Node2D
    {
        [Export] public double DOUBLETAP_MSECS = 500;
        private readonly HashSet<string> holdableActions = new() { "pan_left", "pan_right", "pan_up", "pan_down", "left_click" };

        public HashSet<string> newlyHeldActions = new();
        public HashSet<string> heldActions = new();
        public HashSet<string> justReleasedActions = new();


        private CameraM cameraM;
        private SelectionM selectionM;

        public override void _Ready()
        {
            cameraM = this.GetNode<CameraM>("../CameraM");
            selectionM = this.GetNode<SelectionM>("../SelectionM");
        }


        public override void _UnhandledInput(InputEvent @event)
        {
            // reduces ghosting on held actions compared to Input.IsActionPressed()
            // not necessary for presses, just use Input.IsActionJustPressed()
            newlyHeldActions = holdableActions
                .Except(heldActions)
                .Where(action => @event.IsActionPressed(action))
                .ToHashSet();
            justReleasedActions = heldActions
                .Where(action => @event.IsActionReleased(action))
                .ToHashSet();
            heldActions = heldActions.Concat(
                    newlyHeldActions
                )
                .Where(action => !@event.IsActionReleased(action))
                .ToHashSet();

            selectionM.HandleInput(@event);

            newlyHeldActions.Clear();
            // if(control_group != CONTROL_GROUP_NUMBER.NONE and 
            //     event.is_action_pressed("add_to_control_group_" + str(control_group))):
            //     if(self not in G.player_selection):
            //             G.player_selection.append(self)
            // if(control_group != CONTROL_GROUP_NUMBER.NONE and 
            //     event.is_action_pressed("control_group_" + str(control_group))):
            //     if(last_time_selection_hotkey_pressed != -1):
            //         var current_time = Time.get_ticks_msec()
            //         if((current_time - last_time_selection_hotkey_pressed) <= G.DOUBLETAP_MSECS):
            //             G.current_camera = $PlayerCamera
            //             $PlayerCamera.make_current()
            //         last_time_selection_hotkey_pressed = -1
            //     else:
            //         G.player_selection = [self] as Array[Unit]
            //         last_time_selection_hotkey_pressed = Time.get_ticks_msec()
        }

        public override void _Process(double delta)
        {
            cameraM.InputProcess(delta);
        }
    }
}