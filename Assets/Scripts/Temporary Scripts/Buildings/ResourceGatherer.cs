/* ResourceGatherer.cs - Highborne Universe
 * 
 * Creation Date: 08/08/2023
 * Authors: Archetype
 * Original: Archetype
 * 
 * Edited By: Archetype
 * 
 * Changes: 
 *      [08/08/2023] - Initial implementation (Archetype)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGatherer : BuildingBase
{
    /// <summary>
    /// Variables for amount of workers currently woriking on this building and max capacity
    /// </summary>
    public int numOfWorkers, maxNumOfWorkers;

    [Space]
    [Header("Can be: food, wood, metal, crystal, stone or gold")]
    public string whatIsProduced;

    public override void Start()
    {
        base.Start();
        BuildingManager.singleton.resourceBuilding = true;
    }

    //Do things when the blueprint is placed
    public override void BecomeSolid()
    {
        base.BecomeSolid();
        BuildingManager.singleton.touchingCorrectResource = false;
        BuildingManager.singleton.resourcePoint = null;
    }

    //Do things when the building finishes construction
    public override void BuildDone()
    {
        base.BuildDone();
        BuildingManager.singleton.resourceBuilding = false;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.layer == LayerMask.NameToLayer("ResourceNode") 
            && collision.gameObject.tag == whatIsProduced)
            BuildingManager.singleton.touchingCorrectResource = true;
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        if (collision.gameObject.layer == LayerMask.NameToLayer("ResourceNode")
            && collision.gameObject.tag == whatIsProduced)
            BuildingManager.singleton.touchingCorrectResource = false;
    }

    //Should be triggered when a worker starts working here
    public void GainAWorker()
    {
        numOfWorkers++;
        switch(whatIsProduced)
        {
            case "food":
                ResourceManager.singleton.foodWorkers++;
            break;

            case "wood":
                ResourceManager.singleton.woodWorkers++;
            break;

            case "metal":
                ResourceManager.singleton.metalWorkers++;
            break;

            case "crystal":
                ResourceManager.singleton.crystalWorkers++;
            break;

            case "stone":
                ResourceManager.singleton.stoneWorkers++;
            break;

            case "gold":
                ResourceManager.singleton.goldWorkers++;
            break;

            default:
                Debug.Log("Resource not recognized");
            break;
        }
    }

    //Should be triggered when a worker stops working here
    public void LooseAWorker()
    {
        numOfWorkers--;
        switch (whatIsProduced)
        {
            case "food":
                ResourceManager.singleton.foodWorkers--;
                break;

            case "wood":
                ResourceManager.singleton.woodWorkers--;
                break;

            case "metal":
                ResourceManager.singleton.metalWorkers--;
                break;

            case "crystal":
                ResourceManager.singleton.crystalWorkers--;
                break;

            case "stone":
                ResourceManager.singleton.stoneWorkers--;
                break;

            case "gold":
                ResourceManager.singleton.goldWorkers--;
                break;

            default:
                Debug.Log("Resource not recognized");
                break;
        }
    }
}