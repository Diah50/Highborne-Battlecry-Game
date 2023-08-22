/*
 * PlayerInventory.cs - Highborne Universe
 * 
 * Creation Date: 10/08/2023
 * Authors: C137
 * Original: C137
 * 
 * Changes: 
 *  [10/08/2023] - Initial Implementation (C137)
 *  [15/08/2023] - Removed unnecessary code + Implemented moving UI (C137)
 *  
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : Singleton<PlayerInventory>
{
    /// <summary>
    /// The actual inventory for handling the items
    /// </summary>
    public Inventory inventory;

    /// <summary>
    /// The current item which is being moved if any. 
    /// i.e the item the player is currently moving in between slots
    /// </summary>
    public Item movingItem { get => _movingItem; set { MovingItem(value); _movingItem = value; } }

    /// <summary>
    /// The image used to show the current item being moved
    /// </summary>
    public Image moverUI;

    /// <summary>
    /// The canvas in which the "moverUI" is found
    /// Used to position the "moveUI" to the mouse position
    /// </summary>
    public Canvas moverUICanvas;

    /// <summary>
    /// Actual holder for the current item being moved
    /// Used to avoid stack overflow
    /// </summary>
    Item _movingItem;

    public void MovingItem(Item moving)
    {
        if(moving == null)
        {
            moverUI.gameObject.SetActive(false);
            return;
        }

        moverUI.gameObject.SetActive(true);
        moverUI.sprite = moving.icon;
    }

    public void Update()
    {
        if (movingItem != null)
            UpdateMoverPosition();
    }

    void UpdateMoverPosition()
    {

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            moverUICanvas.transform as RectTransform,
            Input.mousePosition, moverUICanvas.worldCamera,
            out Vector2 movePos);

        moverUI.transform.position = moverUICanvas.transform.TransformPoint(movePos);
    }
}
