using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempUnitDummy : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) transform.position += (Vector3.up);
    }

    int numOfClicks;
    private void OnMouseDown()
    {
        numOfClicks++;
        if (numOfClicks >= 2) CameraCenterMovement.Instance.TakeFocus(transform);
        Invoke("giveUpOnDoubleClick", .2f);
    }
    void giveUpOnDoubleClick()
    {
        numOfClicks = 0;
    }
}