/* BuildingManager.cs - Highborne Universe
 * 
 * Creation Date: 03/08/2023
 * Authors: Archetype
 * Original: Archetype
 * 
 * Edited By: Archetype
 * 
 * Changes: 
 *      [03/08/2023] - Initial implementation (Archetype)
 *      [08/08/2023] - Bug fixing (Archetype)
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingManager : Singleton<BuildingManager>
{
    /// <summary>
    /// Fake building to show where to build
    /// </summary>
    GameObject building;

    /// <summary>
    /// Previous building for chain building purpuses
    /// </summary>
    GameObject lastBuild;

    /// <summary>
    /// Temporary tilemap
    /// </summary>
    public Tilemap tilemapTemp;

    /// <summary>
    /// Permanant tilemap
    /// </summary>
    public Tilemap tilemapPerm;

    /// <summary>
    /// List of chain built buildings
    /// </summary>
    List<GameObject> buildBluePrints = new List<GameObject>();

    /// <summary>
    /// Basic tiles to occupy grid
    /// </summary>
    public TileBase redTile, greenTile, whiteTile;

    /// <summary>
    /// Script from building that is being selected
    /// </summary>
    BuildingBase buildingScript;

    /// <summary>
    /// Whether or not the building blueprint is touching another exisiting building 
    /// </summary>
    public bool touchingAnotherBuilding;

    /// <summary>
    /// Relavant variables if the building is a resource collector and touching the correct node
    /// </summary>
    public bool touchingCorrectResource, resourceBuilding;
    public Transform resourcePoint;

    private void Update()
    {
        //If no buildings are selected to be built, return
        if (building == null) return;
            
        //Selected buildings follow mouse around
        MoveWithMouse();
        
        //Check if current building can be placed in current spot
        if (buildingScript != null)
            if ((!touchingAnotherBuilding) && (!resourceBuilding || 
                (touchingCorrectResource && resourceBuilding && resourcePoint != null)))
                PlaceBuilds();

        //When Shift is released the chain of buildings is solidified
        if (Input.GetKeyUp(KeyCode.LeftShift) && buildBluePrints.Count > 0)
        {
            if (building != null) Destroy(building);
            tilemapTemp.ClearAllTiles();
            building = null;
            buildBluePrints.Clear();
        }

        //Right mouse button cancels the selected build
        if (Input.GetMouseButtonDown(1))
        {
            if (building != null) Destroy(building);
            tilemapTemp.ClearAllTiles();
            building = null;
        }
    }

    void PlaceBuilds()
    {
        //Start build chain
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift))
        {
            TakeAreaPerm(GetColliderVertexPositionsLocal().min, buildingScript.size);
            buildBluePrints.Add(building);
            building.SendMessage("BecomeSolid", SendMessageOptions.DontRequireReceiver);
            building = null;
            SelectBuilding(lastBuild);
        }//Build single building
        else if (Input.GetMouseButtonDown(0))
        {
            TakeAreaPerm(GetColliderVertexPositionsLocal().min, buildingScript.size);
            building.SendMessage("BecomeSolid", SendMessageOptions.DontRequireReceiver);
            building = null;
        }
    }

    //Generate building blueprint that follows mouse
    public void SelectBuilding(GameObject build)
    {
        if (building != null) Destroy(building);
        building = Instantiate(build);
        lastBuild = build;
        buildingScript = build.GetComponent<BuildingBase>();
    }

    //Drag blueprint around with mouse
    void MoveWithMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
        if (resourcePoint != null && resourceBuilding) worldPoint = resourcePoint.position;
        Vector3Int position = tilemapTemp.WorldToCell(worldPoint);

        building.transform.position = position;

        tilemapTemp.ClearAllTiles();
        TakeArea(GetColliderVertexPositionsLocal().min, buildingScript.size);
    }
    
    //Fetch Size of blueprint in cells
    BoundsInt GetColliderVertexPositionsLocal()
    {
        return new BoundsInt(new Vector3Int((int)building.transform.position.x,
            (int)building.transform.position.y), buildingScript.size * tilemapTemp.size);
    }

    //Mark area under blueprint
    void TakeArea(Vector3Int start, Vector3Int size)
    {
        var x = new Vector3Int(start.x - (size.x/2), start.y - (size.y/2));
        tilemapTemp.BoxFill(x, (((!touchingAnotherBuilding) && (!resourceBuilding ||
                (touchingCorrectResource && resourceBuilding && resourcePoint != null))) ? greenTile : redTile), 
            x.x, x.y, x.x + size.x, x.y + size.y);
    }

    //Mark perm area under blueprint as occupied
    void TakeAreaPerm(Vector3Int start, Vector3Int size)
    {
        tilemapTemp.ClearAllTiles();
        var x = new Vector3Int(start.x - (size.x / 2), start.y - (size.y / 2));
        tilemapPerm.BoxFill(x, (whiteTile), 
            x.x, x.y, x.x + size.x, x.y + size.y);
    }
}