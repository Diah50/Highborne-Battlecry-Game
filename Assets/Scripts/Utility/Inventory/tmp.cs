using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tmp : MonoBehaviour
{
    public Item item;

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.K))
            PlayerInventory.singleton.movingItem = item;

        if (Input.GetKeyUp(KeyCode.L))
            Debug.Log(PlayerInventory.singleton.movingItem);
    }
}
