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
    /// Tilemap
    /// </summary>
    public Tilemap tilemap;

    /// <summary>
    /// List of chain built buildings
    /// </summary>
    List<GameObject> buildBluePrints = new List<GameObject>();

    /// <summary>
    /// Vertices of building
    /// </summary>
    Vector2[] vertices;

    /// <summary>
    /// Basic white tile to occupy grid
    /// </summary>
    public TileBase whiteTile;

    /// <summary>
    /// Script from building that is being selected
    /// </summary>
    BuildingBase buildingScript;

    /// <summary>
    /// Size of buildings in tiles
    /// </summary>
    public Vector3Int size { get; private set; }

    private void Update()
    {
        //If no buildings are selected to be built, return
        if (building == null) return;
            
        //Selected buildings follow mouse around
        MoveWithMouse();
        
        //Check if current building can be placed in current spot
        if (buildingScript != null)
            if (CanBePlaced(buildingScript)) PlaceBuilds();

        //When Shift is released the chain of buildings is solidified
        if (Input.GetKeyUp(KeyCode.LeftShift) && buildBluePrints.Count > 0)
        {
            foreach (GameObject gO in buildBluePrints)
            {
                gO.SendMessage("BecomeSolid", SendMessageOptions.DontRequireReceiver);
            }
            if (building != null) Destroy(building);
            building = null;
            buildBluePrints.Clear();
        }

        //Right mouse button cancels the selected build
        if (Input.GetMouseButtonDown(1))
        {
            if (building != null) Destroy(building);
            building = null;
        }
    }

    void PlaceBuilds()
    {
        //Start build chain
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift))
        {
            Vector3Int start = tilemap.WorldToCell(GetStartPosition());
            TakeArea(start, buildingScript.size);
            buildBluePrints.Add(building);
            building = null;
            SelectBuilding(lastBuild);
        }//Build single building
        else if (Input.GetMouseButtonDown(0))
        {
            Vector3Int start = tilemap.WorldToCell(GetStartPosition());
            TakeArea(start, buildingScript.size);
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

        GetColliderVertexPositionsLocal();
        CalculateSizeInCells();
    }

    //Drag blueprint around with mouse
    void MoveWithMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
        Vector3Int position = tilemap.WorldToCell(worldPoint);

        //TileBase tile = tilemap.GetTile(position);
        building.transform.position = position;
    }
    
    //Fetch verticies of blueprint
    void GetColliderVertexPositionsLocal()
    {
        BoxCollider2D b = building.GetComponent<BoxCollider2D>();
        vertices = new Vector2[4];
        vertices[0] = b.bounds.center + new Vector3(-b.size.x, -b.size.y) * .5f;
        vertices[1] = b.bounds.center + new Vector3(b.size.x, -b.size.y) * .5f;
        vertices[2] = b.bounds.center + new Vector3(b.size.x, b.size.y) * .5f;
        vertices[3] = b.bounds.center + new Vector3(-b.size.x, b.size.y) * .5f;
    }

    //Calculate size using cells
    void CalculateSizeInCells()
    {
        Vector3Int[] vert = new Vector3Int[vertices.Length];

        for (int i = 0; i < vert.Length; i++)
        {
            Vector3 worldPos = transform.TransformPoint(vertices[i]);
            vert[i] = tilemap.WorldToCell(worldPos);
        }

        size = new Vector3Int(Math.Abs((vert[0] - vert[1]).x), Math.Abs((vert[0] - vert[3]).y), 1);
    }

    Vector3 GetStartPosition()
    {
        return building.transform.TransformPoint(vertices[0]);
    }

    //Get all tiles under blueprint
    static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach (var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }

        return array;
    }

    //Checks if current blueprint can be placed on current square
    bool CanBePlaced(BuildingBase buildingBase)
    {
        BoundsInt area = new BoundsInt();
        area.position = tilemap.WorldToCell(GetStartPosition());
        area.size = buildingBase.size;

        TileBase[] baseArray = GetTilesBlock(area, tilemap);

        foreach (var b in baseArray)
        {
            if (b == whiteTile)
            {
                return false;
            }
        }

        return true;
    }

    //Mark area under blueprint as occupied
    void TakeArea(Vector3Int start, Vector3Int size)
    {
        tilemap.BoxFill(start, whiteTile, start.x, start.y, start.x + size.x, start.y + size.y);
    }
}