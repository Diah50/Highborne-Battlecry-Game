/* Inventory.cs - Highborne Universe
 * 
 * Creation Date: 30/07/2023
 * Authors: C137
 * Original : C137
 * 
 * Changes: 
 *      [30/07/2023] - Initial implementation (C137)
 *      
 *  TODO:
 *      Optimize Tuple<bool, int> AddItem(Item item, int amount) function
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Contains information of the inventory slot
/// </summary>
public struct Slot
{
    /// <summary>
    /// The item contained in the slot
    /// </summary>
    public Item item;

    /// <summary>
    /// The amount of "item" contained in the slot
    /// </summary>
    public int stack;

    public Slot(Item item, int stack = 1)
    {
        this.item = item;
        this.stack = stack;
    }
}

public class Inventory
{
    /// <summary>
    /// Refers to the slots of the inventory
    /// </summary>
    public List<Slot> slots;

    /// <summary>
    /// Initiate the inventory
    /// </summary>
    /// <param name="slots">The value of each slot</param>
    public Inventory(Slot[] slots)
    {
        this.slots = slots.ToList();
    }

    /// <summary>
    /// Initiate the inventory
    /// </summary>
    /// <param name="slots">The amount of slot the inventory has</param>
    public Inventory(int slots)
    {
        this.slots = new List<Slot>(slots);
    }

    /// <summary>
    /// Whether the inventory has any empty slots 
    /// </summary>
    public bool HasEmptySlot()
    {
        return slots.Any(slot => slot.item == null);
    }

    /// <summary>
    /// Searches if the inventory contains the item
    /// </summary>
    /// <param name="item">The item to search for</param>
    /// <returns></returns>
    public bool HasItem(Item item)
    {
        return GetFirstSlot(item).HasValue;
    }

    /// <summary>
    /// The first empty slot in the inventory
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception">Inventory doesn't have any empty slots</exception>
    public int GetEmptySlot()
    {
        if (!HasEmptySlot())
            throw new Exception("Inventory does not have empty slots");

        return slots.FindIndex(slot => slot.item == null);
    }

    /// <summary>
    /// Gets the first slot in which the item is found
    /// </summary>
    /// <param name="item">Item to search for</param>
    /// <returns>Slot of the item, otherwise -1</returns>
    public int? GetFirstSlot(Item item) 
    { 
        var correspondingSlot = slots.FindIndex(slot => slot.item == item);

        return correspondingSlot == -1 ? null : correspondingSlot;
    }

    /// <summary>
    /// Add an item with amount 1 to the inventory
    /// </summary>
    /// <param name="item">The item to add</param>
    /// <returns></returns>
    public bool AddItem(Item item)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            Slot currentSlot = slots[i];

            //If slot is null add the item
            if (currentSlot.item == null)
            {
                slots[i] = new(item);
                return true;
            }

            if (currentSlot.item != item)
                continue;

            //Increase the stack count if it's not maxed
            if (currentSlot.stack == currentSlot.item.maxStack)
                continue;

            slots[i] = new(item, currentSlot.stack + 1);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Adds a certain number of an item to the inventory(unoptimised)
    /// </summary>
    /// <param name="item">The item to be added</param>
    /// <param name="amount">The amount to add</param>
    /// <returns>If all the items could be added, if not, how many were leftover</returns>
    public Tuple<bool, int> AddItem(Item item, int amount)
    {
        for(int i = 0;i < amount; i++)
        {
            if(AddItem(item))
                amount--;
            else 
                return new(false, amount);
        }

        return new(true, 0);
    }

    /// <summary>
    /// Removes the item at the given index
    /// </summary>
    /// <param name="index">Index to remove the item</param>
    /// <param name="amount">Amount to remove, removes entire stack if set to 0</param>
    public void RemoveAt(int index, int amount = 0)
    {
        if (slots[index].item == null)
            return;

        if (amount == 0)
            slots[index] = new(null, 0);
        else
            slots[index] = new(slots[index].item, Mathf.Clamp(slots[index].stack - amount, 0, int.MaxValue));
    }

    /// <summary>
    /// Forcefully add an item to a specific slot
    /// </summary>
    /// <param name="item">The item to add</param>
    /// <param name="slot">The slot to add the item</param>
    /// <param name="amount">The amount to add</param>
    public void SetItem(Item item, int slot, int amount = 1)
    {
        slots[slot] = new(item, amount);
    }

    /// <summary>
    /// Swap the places of two items
    /// </summary>
    /// <param name="slotA">Slot of the first item</param>
    /// <param name="slotB">Slot of the second item</param>
    public void Swap(int slotA, int slotB)
    {
        (slots[slotA], slots[slotB]) = (slots[slotB], slots[slotA]);
    }
}
