/* InventoryDirector.cs - Highborne Universe
 * 
 * Creation Date: 08/09/2023
 * Authors: DaynerKurdi
 * Original: DaynerKurdi
 * 
 * Changes: 
 *      [08/09/2023] - Initial implementation (DaynerKurdi)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InventoryDirector : MonoBehaviour
{
    /// <summary>
    /// The main inventory script array
    /// </summary>
    [SerializeField]
    private InventorySlotManager[] MainInventory;  

    public void Start()
    {
        //                            move up    -> MainInventory -> Background -> Grid
        Transform MainInventoryTran = transform.parent.GetChild(1).GetChild(0).GetChild(0);

        int childCount = MainInventoryTran.childCount;

        MainInventory = new InventorySlotManager[childCount];

        for (int i = 0; i < childCount; i++)
        {
            MainInventory[i] = MainInventoryTran.GetChild(i).GetComponent<InventorySlotManager>();
        }
    }
}
