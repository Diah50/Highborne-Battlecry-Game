using System.Collections.Generic;
using Godot;


namespace EntityComponent
{
    public enum BuildType{normal, auto, multibuild, sacrifice}

    public partial class BuilderC : Node
    {
        [Export] public int BuildPower;
        [Export] public int RepairPower = 0;
        [Export] public int Range = 0;
        [Export] public BuildType BuildType;
    }
}