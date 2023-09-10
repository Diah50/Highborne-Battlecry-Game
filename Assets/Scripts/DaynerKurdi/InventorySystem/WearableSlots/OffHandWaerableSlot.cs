/* InventorySlotManager.cs - Highborne Universe
 * 
 * Creation Date: 09/09/2023
 * Authors: DaynerKurdi
 * Original: DaynerKurdi
 * 
 * Changes: 
 *      [09/09/2023] - Initial implementation (DaynerKurdi)
 */

using UnityEngine;
using UnityEngine.UI;

public class OffHandWaerableSlot : MonoBehaviour
{
    /// <summary>
    /// The black outline around the box or "Background"
    /// </summary>
    [SerializeField]
    private Image selectorImage;

    /// <summary>
    /// The icon for box slot or "Item Icon"
    /// </summary>
    [SerializeField]
    private Image iconImage;

    /// <summary>
    /// Where equipment data is stored and lives 
    /// </summary>
    [SerializeField]
    private OffHandScOb offHandScOb;

    public void SetEquimentScOb(OffHandScOb equipment)
    {
        iconImage.sprite = equipment.equipmentIconSprite;
        iconImage.color = Color.white;

        offHandScOb = equipment;
    }

    public void RemoveEquimentScOb()
    {
        iconImage.sprite = null;
        iconImage.color = Color.black;

        offHandScOb = null;
    }

    public void SetSelectorOn()
    {
        selectorImage.color = Color.green;
    }

    public void SetSelectorOff()
    {
        selectorImage.color = Color.white;
    }

}
