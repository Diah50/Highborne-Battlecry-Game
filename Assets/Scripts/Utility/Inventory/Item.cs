/* Item.cs - Highborne Universe
 * 
 * Creation Date: 30/07/2023
 * Authors: C137
 * Original : C137
 * 
 * Changes: 
 *      [30/07/2023] - Initial implementation (C137)
 *      [01/08/2023] - Made description a text area in the inspector (C137)
 *      [10/08/2023] - Made item an interface and added an enum for the item type (C137)
 *      [12/08/2023] - Added an icon variable to show the item in a slot with a graphic (C137)
 *      [15/08/2023] - Reverted item back to being a class (C137)
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    HeadArmor,
    HandArmor,
    ChestArmor,
    LegArmor,
    FeetArmor,

    Ring,
    Potion,

    Misc
}

[CreateAssetMenu(fileName = "ItemName", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    /// <summary>
    /// The unique identifier of the item
    /// </summary>
    public string uniqueId;

    /// <summary>
    /// The type of the item
    /// </summary>
    public ItemType type;

    /// <summary>
    /// The display name of the item which will be shown to the player
    /// </summary>
    public string displayName;

    /// <summary>
    /// The icon for the item
    /// </summary>
    public Sprite icon; 

    /// <summary>
    /// The description of the item
    /// </summary>
    public string description;

    /// <summary>
    /// The maximum amount of this item that an inventory slot can contain
    /// </summary>
    public int maxStack;

    /// <summary>
    /// Called when the item is picked up
    /// </summary>
    public virtual void OnItemPickup() { }

    /// <summary>
    /// Called when the item is dropped
    /// </summary>
    public virtual void OnItemDrop() { }

    /// <summary>
    /// Called when the item is equipped
    /// </summary>
    public virtual void OnItemEquip() { }

    /// <summary>
    /// Called when the item is unequipped
    /// </summary>
    public virtual void OnItemUnEquip() { }

    /// <summary>
    /// Called when the item changes slots
    /// </summary>
    public virtual void OnItemSlotChange() { }
}
