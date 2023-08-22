/* ItemSlot.cs - Highborne Universe
 * 
 * Creation Date: 12/08/2023
 * Authors: C137
 * Original: C137
 * 
 * Changes: 
 *      [12/08/2023] - Initial implementation (C137)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : Button
{
    /// <summary>
    /// The item stored in the slot
    /// </summary>
    Item storedItem;

    /// <summary>
    /// The image to show the icon of the stored item
    /// </summary>
    public Image itemIcon;

    protected override void Awake()
    {
        base.Awake();

        itemIcon = itemIcon == null ?  transform.GetChild(0).GetComponent<Image>() : itemIcon;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        AcceptItem(PlayerInventory.singleton.movingItem);
    }

    public virtual void AcceptItem(Item item)
    {
        (PlayerInventory.singleton.movingItem, storedItem) = (storedItem, item);

        itemIcon.sprite = storedItem?.icon;

        if (itemIcon.sprite == null)
            itemIcon.color = new Color(1, 1, 1, 0);
        else
            itemIcon.color = new(1, 1, 1, 1);
    }
}
