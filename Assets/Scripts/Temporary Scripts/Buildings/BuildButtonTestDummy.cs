using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildButtonTestDummy : MonoBehaviour
{
    public GameObject building;

    public void BuildTimeBaby()
    {
        BuildingManager.singleton.SelectBuilding(building);
    }
}
