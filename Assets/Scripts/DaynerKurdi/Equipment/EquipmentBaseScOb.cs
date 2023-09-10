/* EquipmentBaseScOb.cs - Highborne Universe
 * 
 * Creation Date: 08/09/2023
 * Authors: DaynerKurdi
 * Original: DaynerKurdi
 * 
 * Changes: 
 *      [08/09/2023] - Initial implementation (DaynerKurdi)
 */
using UnityEngine;
public abstract class EquipmentBaseScOb : ScriptableObject
{
    /// <summary>
    /// Used to store the equipment Id
    /// </summary>
    public int itemID;

    /// <summary>
    /// The equipment name
    /// </summary>
    public string equipmentName;

    /// <summary>
    /// The equipment description
    /// </summary>
    public string equipmentDescription;

    /// <summary>
    /// The sprite that will be used on the map field
    /// </summary>
    public Sprite equipmentFieldSprite; 

    /// <summary>
    /// The sprite that will be used as the UI icon
    /// </summary>
    public Sprite equipmentIconSprite;
}
