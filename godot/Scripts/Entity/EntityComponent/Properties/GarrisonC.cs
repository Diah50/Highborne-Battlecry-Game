using System.Collections.Generic;
using Godot;

namespace EntityComponent
{
    public partial class GarrisonC : Node
    {
        [Export] public Godot.Collections.Array<string> GarrisonTags = new();
        [Export] public int GarrisonAmount = 5;
        [Export] public int GarrisonHealBonus = 0;
    }
}