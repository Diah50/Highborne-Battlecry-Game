using System.Collections.Generic;
using Godot;

// Each task component adds 1 custom task

namespace EntityComponent
{
    public abstract class ATaskC : Node
    {
        [Export] public string ID; // unique identifier for the task
        [Export] public string _Name;
        [Export] public string Description; // displayed on UI
        [Export] public Sprite2D Icon;
        [Export] public int Slot;
        [Export] public int GoldCost = -1;
        [Export] public int MetalCost = -1;
        [Export] public int ManaCost = -1;
        [Export] public int Time = -1;
        [Export] public string RequiresLivingEntity = null;
        [Export] public string RequiresUnlock = null;
        [Export] public bool ShowBeforeUnlocked = true;
        [Export] public Sprite2D IconBeforeUnlocked;
        [Export] public SubmenuC Submenu = null;
    }
}