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
 *      [10/08/2023] - Aded script paramaters for a neutral resource building that can be captured, also custom Editor (Archetype)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ResourceGatherer : BuildingBase
{
    /// <summary>
    /// Inherited Editor from BuildingBase
    /// </summary>
    #region Editor
#if UNITY_EDITOR
    [CustomEditor(typeof(ResourceGatherer))]
    public class ResourceGathererEditor : BuildingBaseEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
#endif
    #endregion

    /// <summary>
    /// Variables for amount of workers currently woriking on this building and max capacity
    /// </summary>
    public int numOfWorkers, maxNumOfWorkers;

    /// <summary>
    /// White flag next to building that can be recoloured
    /// </summary>
    SpriteRenderer flag;

    [Space]
    [Header("Can be: food, wood, metal, crystal, stone or gold")]
    public string whatIsProduced;

    public override void Start()
    {
        base.Start();
        if (playerBuild) BuildingManager.singleton.resourceBuilding = true;
        else flag = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
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

    //when the building is captured by the player
    public void BecomeFriendly()
    {
        flag.color = GameManager.singleton.teamColor;
        switch (whatIsProduced)
        {
            case "food":
                ResourceManager.singleton.foodNodes++;
                break;

            case "wood":
                ResourceManager.singleton.woodNodes++;
                break;

            case "metal":
                ResourceManager.singleton.metalNodes++;
                break;

            case "crystal":
                ResourceManager.singleton.crystalNodes++;
                break;

            case "stone":
                ResourceManager.singleton.stoneNodes++;
                break;

            case "gold":
                ResourceManager.singleton.goldNodes++;
                break;

            default:
                Debug.Log("Resource not recognized");
                break;
        }
    }

    //when the building is no longer captured by the player
    public void BecomeUnFriendly()
    {
        flag.color = Color.gray;
        switch (whatIsProduced)
        {
            case "food":
                ResourceManager.singleton.foodNodes--;
                break;

            case "wood":
                ResourceManager.singleton.woodNodes--;
                break;

            case "metal":
                ResourceManager.singleton.metalNodes--;
                break;

            case "crystal":
                ResourceManager.singleton.crystalNodes--;
                break;

            case "stone":
                ResourceManager.singleton.stoneNodes--;
                break;

            case "gold":
                ResourceManager.singleton.goldNodes--;
                break;

            default:
                Debug.Log("Resource not recognized");
                break;
        }
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