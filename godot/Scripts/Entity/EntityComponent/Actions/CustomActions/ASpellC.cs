using System.Collections.Generic;
using Godot;

namespace EntityComponent
{
    public partial class ASpellTC : ACustomActionC
    {
        [Export] public bool UseShopQueue = false; // will block Shop tasks instead of all other (generally unit-related) tasks  
        [Export] public int Range = 0;
    }
}