/* InventorySlotManager.cs - Highborne Universe
 * 
 * Creation Date: 08/09/2023
 * Authors: DaynerKurdi
 * Original: DaynerKurdi
 * 
 * Changes: 
 *      [08/09/2023] - Initial implementation (DaynerKurdi)
 */

using UnityEngine;
using UnityEngine.UI;

public class InventorySlotManager : MonoBehaviour
{
    /// <summary>
    /// The white outline around the box or "InventorySlot"
    /// </summary>
    [SerializeField]
    private Image selectorImage;

    /// <summary>
    /// The icon for box slot or "InventoryIcon"
    /// </summary>
    [SerializeField]
    private Image iconImage;

    /// <summary>
    /// Where the equipment data is stored and lives 
    /// </summary>
    [SerializeField]
    private EquipmentBaseScOb storedEquipmentScOb;

    public void SetEquimentScOb(EquipmentBaseScOb equipment)
    {
        iconImage.sprite = equipment.equipmentIconSprite;
        iconImage.color = Color.white;

        storedEquipmentScOb = equipment;
    }

    public void RemoveEquimentScOb()
    {
        iconImage.sprite = null;
        iconImage.color = Color.black;

        storedEquipmentScOb = null;
    }

    public void SetSelectorOn()
    {
        selectorImage.color = Color.green;
    }

    public void SetSelectorOff()
    {
        selectorImage.color= Color.white;
    }
}
