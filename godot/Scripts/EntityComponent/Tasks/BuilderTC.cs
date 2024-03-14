using System.Collections.Generic;
using Godot;

namespace EntityComponent
{
    public partial class BuilderTC : ATaskC
    {
        [Export] public Entity.Entity Buildable;
        [Export] public int StartProgress;
    }
}