/* Item.cs - Highborne Universe
 * 
 * Creation Date: 30/07/2023
 * Authors: C137
 * Original : C137
 * 
 * Changes: 
 *      [30/07/2023] - Initial implementation (C137)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemName", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    /// <summary>
    /// The unique identifier of the item
    /// </summary>
    public string uniqueId = "0000";

    /// <summary>
    /// The display name of the item which will be shown to the player
    /// </summary>
    public string displayName;

    /// <summary>
    /// The description of the item
    /// </summary>
    public string description;

    /// <summary>
    /// The maximum amount of this item that an inventory slot can contain
    /// </summary>
    public int maxStack;
}
