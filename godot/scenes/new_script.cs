using Godot;
using System;
using System.Diagnostics;

public partial class new_script : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Debug.Print("hi");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
