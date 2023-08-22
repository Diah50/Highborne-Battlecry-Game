using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetClickPosition : MonoBehaviour
{
    public GameObject target = null;
    public Vector3 position = Vector3.zero;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position = mousePosition;
            //Debug.Log("Clicked at position: " + position.ToString());
        }
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position = mousePosition;
            //Debug.Log("Clicked at position: " + position.ToString());
        }
    }
}

