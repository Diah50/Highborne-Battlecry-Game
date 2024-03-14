using System.Collections.Generic;
using Godot;

namespace EntityComponent
{
    public partial class MoveC : Node
    {
        [Export] public int Speed = 0;
        [Export] public bool CanGarrison = true;
        [Export] public Godot.Collections.Array<string> WalkableTerrain = new(){ "land" };
    }
}