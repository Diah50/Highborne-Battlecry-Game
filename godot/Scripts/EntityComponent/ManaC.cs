using System.Collections.Generic;
using Godot;

namespace EntityComponent
{
    public partial class ManaC : Node
    {
        [Export] public int StartingMana = 0;
        [Export] public int Mana = 10;
        public override void _Ready()
        {
            Mana = StartingMana;
        }
    }
}