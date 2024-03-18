using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Manager
{
    public partial class BuildingM : Node2D
    {
        [Export] public Godot.Collections.Dictionary<int, int> MultibuildSpeed; // number of builders, additional speed multiplier in %
    }
}