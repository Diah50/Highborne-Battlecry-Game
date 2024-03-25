using System.Collections.Generic;
using EntityNS;
using Godot;

namespace EntityComponent
{
    public partial class BuilderTC : ACustomActionC
    {
        [Export] public Entity Building;
        [Export] public int StartProgress;
        [Export] public int Range = 0;
    }
}