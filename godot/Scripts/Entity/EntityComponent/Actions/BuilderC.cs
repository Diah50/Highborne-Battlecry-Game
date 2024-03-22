using System.Collections.Generic;
using Godot;


namespace EntityComponent
{
    public enum BuildType{normal, auto, multibuild, sacrifice}

    public partial class BuilderC : Node2D
    {
        [Export] public int BuildPower;
        [Export] public BuildType BuildType;
    }
}