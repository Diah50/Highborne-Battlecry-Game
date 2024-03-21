using System;
using System.Collections.Generic;
using Godot;

namespace EntityComponent
{
    public partial class DamageableC : Node
    {
        [Export] public int MaxHealth = 10;
        [Export] public int HealthRegen = 0;
        [Export] public int MaxShield = -1;
        [Export] public int ShieldRegen = -1;
        [Export] public int Armor = 0; // flat reduction
        [Export] public int MagicResistance = 0; // % reduction
        [Export] public bool CantHealOverride = false;

        public int Health;

        public override void _Ready()
        {
            Health = MaxHealth;
        }
    }
}