using System;
using System.Collections.Generic;
using Godot;

namespace EntityComponent
{
    public partial class DamageableC : Node
    {
        [Export] public int MaxHealth = 10;
        [Export] public int Armor = 0;

        public int Health;

        public override void _Ready()
        {
            Health = MaxHealth;
        }
    }
}