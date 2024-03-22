using System.Collections.Generic;
using Godot;

namespace EntityComponent
{
    // for all abilities using the build queue/bar
    // mostly spawning units and researching technology, but could be used similar to spells
    public partial class ShopTC : ACustomActionC
    {
        [Export] Godot.Collections.Dictionary<int, int> SpawnsUnits; // entitycode, quantity
        [Export] bool Tech = false; // Techs are non-repeatable, recorded, and suggested to use persistent effects
        [Export] Godot.Collections.Dictionary<int, int> UpgradesCurrentUnits; // entitycode, entitycode
        [Export] Godot.Collections.Dictionary<int, int> UpgradesFutureUnits; // entitycode
    }
}