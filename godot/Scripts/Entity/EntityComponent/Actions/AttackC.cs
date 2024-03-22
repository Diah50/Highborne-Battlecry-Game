using System.Collections.Generic;
using Godot;

namespace EntityComponent
{
    public partial class AttackC : Node2D
    {
        [Export] public int Damage = 0;
        [Export] public int Time = 1000;
        [Export] public int Range = 0;
        [Export] public Godot.Collections.Array<string> AttackableTypes;
        [Export] public Godot.Collections.Dictionary<string, int> AttackBonuses;

        // some sort of backswing is probably recommended
    }
}