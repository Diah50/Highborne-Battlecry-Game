using System.Collections.Generic;
using Godot;

namespace EntityComponent
{
    public partial class BuilderTC : ATaskC
    {
        [Export] public Entity.Entity Building;
        [Export] public int StartProgress;
        [Export] public int Range = 0;
    }
}