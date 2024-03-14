using System.Collections.Generic;
using Godot;

namespace EntityComponent
{
    public partial class SubmenuC : Node
    {
        [Export] public string ID; // unique identifier for the task
        [Export] public string _Name;
        [Export] public string Description; // displayed on UI
        [Export] public Sprite2D Icon;
        [Export] public int Slot;
    }
}