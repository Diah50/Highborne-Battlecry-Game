using System.Collections.Generic;
using Godot;

// Each custom action component adds one button with relatively custom behavior

namespace EntityComponent
{
    public partial class ACustomActionC : Node2D
    {
        [Export] public string ID; // unique identifier for the task
        [Export] public string TypeName;
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