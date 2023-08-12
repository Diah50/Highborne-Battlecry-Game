using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectIfMouseIsTouchingNode : MonoBehaviour
{
    private void OnMouseEnter()
    {
        Debug.Log("oh the pain the pain");
    }

    private void OnMouseExit()
    {
        BuildingManager.singleton.resourcePoint = null;
    }
}
