using Godot;

public  partial class Entity : Node2D{
    public enum EntityType {Building,  Resource, Unit};
    [Export] public EntityType entityType;
    [Export] public string EntityName; // name of the entity type, not of the object which would be Node.Name
    [Export] public string ID; // unique identifier for the entity type
    [Export] public string Description; // displayed on UI
    [Export] public Sprite2D Icon; // displayed on UI
    [Export] public int Radius; // for collision and such, assumes circular hitbox
    [Export] public bool Interactable = true; // If false, won't be targeted by others or AoE, nor take orders from the player
    public int FactionId = -1; // -1 = N/A, 0 = neutral, 0XX = player, 1XX+ = other. All with > -1 should be FactionEntity.
}
