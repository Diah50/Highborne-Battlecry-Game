using System.Collections.Generic;
using Godot;


namespace EntityComponent
{

    public partial class HealerC : Node2D
    {
        [Export] public int HealPower;
        [Export] public string[] HealClasses;
        [Export] public int HealResourcCostPercent;
        [Export] public int HealManaCost;
    }
}