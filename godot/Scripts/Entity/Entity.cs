using System;
using Godot;


// does not affect capabilities, e.g. a building can still move (though it must make room for patrol/stop/attackmove hotkeys)
// will affect selection though, e.g. units are drag selectable
public enum EntitySuperType { Building, Resource, Unit };
namespace Entity
{
    public partial class Entity : Node2D
    {
        [Export] public EntitySuperType EntitySuperType; // see enum above
        [Export] public string ID; // unique identifier for the entity type
        [Export] public string TypeName;
        [Export] public string Description; // displayed on UI
        [Export] public Sprite2D Icon; // displayed on UI
        [Export] public String[] Tags;
        [Export] public int Radius; // for collision and such, assumes circular hitbox
        public int FactionId = -1; // -1 = N/A, 0 = neutral, 0XX = player, 1XX+ = other. All with > -1 should be FactionEntity.
    }
}