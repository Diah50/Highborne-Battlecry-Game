using System.Collections.Generic;
using Godot;

namespace EntityComponent
{
    public partial class ManaC : Node2D
    {
        [Export] public int StartingMana = 0;
        [Export] public int Mana = 10;
        [Export] public int ManaRegen = 1;
        public override void _Ready()
        {
            Mana = StartingMana;
        }
    }
}