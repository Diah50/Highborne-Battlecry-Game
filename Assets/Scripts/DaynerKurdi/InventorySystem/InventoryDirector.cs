/* InventoryDirector.cs - Highborne Universe
 * 
 * Creation Date: 08/09/2023
 * Authors: DaynerKurdi
 * Original: DaynerKurdi
 * 
 * Changes: 
 *      [08/09/2023] - Initial implementation (DaynerKurdi)
 */

using UnityEngine;
public class InventoryDirector : MonoBehaviour
{
    /// <summary>
    /// The main inventory script array
    /// </summary>
    [SerializeField]
    private InventorySlotManager[] MainInventory;

    /// <summary>
    /// The weapon or main hand slot script
    /// </summary>
    [SerializeField]
    private MainHandWearableSlot mainHandSlot;

    /// <summary>
    /// The off hand slot script
    /// </summary>
    [SerializeField]
    private OffHandWaerableSlot offHandSlot;

    /// <summary>
    /// The head armor slot script
    /// </summary>
    [SerializeField]
    private HeadArmorWearableSlot headArmorSlot;

    /// <summary>
    /// The body armor slot script
    /// </summary>
    [SerializeField]
    private BodyArmorWearableSlot bodyArmorSlot;

    /// <summary>
    /// The Misc Item Script Array
    /// </summary>
    [SerializeField]
    private MiscWearableSlot[] miscWearableSlotArray;

    public void Start()
    {
        //                            move up    -> MainInventory -> Background -> Grid
        Transform MainInventoryTran = transform.parent.GetChild(1).GetChild(0).GetChild(0);
        //                  move up -> Hero Unit Wearable -> Background -> Solid Color -> Wearable Slots
        Transform heroUnitTran = transform.parent.GetChild(2).GetChild(0).GetChild(0).GetChild(0);

        int childCount = MainInventoryTran.childCount;

        MainInventory = new InventorySlotManager[childCount];

        for (int i = 0; i < childCount; i++)
        {
            MainInventory[i] = MainInventoryTran.GetChild(i).GetComponent<InventorySlotManager>();
        }

        headArmorSlot = heroUnitTran.GetChild(0).GetComponent<HeadArmorWearableSlot>();
        bodyArmorSlot = heroUnitTran.GetChild(1).GetComponent<BodyArmorWearableSlot>();
        mainHandSlot = heroUnitTran.GetChild(2).GetComponent<MainHandWearableSlot>();
        offHandSlot = heroUnitTran.GetChild(3).GetComponent<OffHandWaerableSlot>();
       
        miscWearableSlotArray = new MiscWearableSlot[4];

        miscWearableSlotArray[0] = heroUnitTran.GetChild(4).GetComponent<MiscWearableSlot>();
        miscWearableSlotArray[1] = heroUnitTran.GetChild(5).GetComponent<MiscWearableSlot>();
        miscWearableSlotArray[2] = heroUnitTran.GetChild(6).GetComponent<MiscWearableSlot>();
        miscWearableSlotArray[3] = heroUnitTran.GetChild(7).GetComponent<MiscWearableSlot>();

    }
}
