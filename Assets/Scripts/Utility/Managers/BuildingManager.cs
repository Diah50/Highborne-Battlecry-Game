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
 *      [10/08/2023] - Made TakeAreaPerm() public so neutral buildings can acess it (Archetype)
 *      [12/08/2023] - TakeAreaPerm() is currently mostly deactivated but may be used if needed (Archetype)
 *      [21/08/2023] - Updates to make script work with new grid, also changed the blueprint marker to hide and show from a pool rather than instantiate/destroy (Archetype)
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
    /// Previous building for chain building purposes
    /// </summary>
    GameObject lastBuild;

    /// <summary>
    /// Temporary tilemap
    /// </summary>
    public Tilemap tilemapTemp;

    /// <summary>
    /// Permanent tilemap
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
    /// Whether or not the building blueprint is touching another existing building 
    /// </summary>
    [HideInInspector] public bool touchingAnotherBuilding;

    /// <summary>
    /// Sprites for the tiles that show if a space is available, will turn red or green depending
    /// </summary>
    List<SpriteRenderer> occupationTiles = new List<SpriteRenderer>();

    /// <summary>
    /// List of tiles used to show if a space is available to build
    /// </summary>
    List<GameObject> listOfOccupationTilesInScene = new List<GameObject>();

    /// <summary>
    /// Prefab for tile used to show if a space is available
    /// </summary>
    public GameObject tilePrefab;

    /// <summary>
    /// Relavant variables if the building is a resource collector and touching the correct node
    /// </summary>
    public bool resourceBuilding;
    [HideInInspector] public bool touchingCorrectResource;
    [HideInInspector] public Transform resourcePoint;

    private void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            var x = Instantiate(tilePrefab, transform);
            listOfOccupationTilesInScene.Add(x);
            x.SetActive(false);
        }
    }

    private void Update()
    {
        //If no buildings are selected to be built, return
        if (building == null) return;
            
        //Selected buildings follow mouse around
        MoveWithMouse();

        //Check if current building can be placed in current spot
        CheckIfTouching();

        CheckInput();
    }

    void CheckInput()
    {
        //When Shift is released the chain of buildings is solidified
        if (Input.GetKeyUp(KeyCode.LeftShift) && buildBluePrints.Count > 0)
        {
            EmptyOccupySprites();
            if (building != null) Destroy(building);
            //tilemapTemp.ClearAllTiles();
            building = null;
            buildBluePrints.Clear();
        }

        //Right mouse button cancels the selected build
        if (Input.GetMouseButtonDown(1))
        {
            EmptyOccupySprites();
            if (building != null) Destroy(building);
            //tilemapTemp.ClearAllTiles();
            building = null;
        }
    }

    void CheckIfTouching()
    {
        if (buildingScript != null)
            if ((!touchingAnotherBuilding) && (!resourceBuilding ||
                (touchingCorrectResource && resourceBuilding && resourcePoint != null)))
                PlaceBuilds();
    }

    void PlaceBuilds()
    {
        //Start build chain
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift))
        {
            TakeAreaPerm(GetColliderVertexPositionsLocal(building, buildingScript).min, buildingScript.size);
            buildBluePrints.Add(building);
            building.SendMessage("BecomeSolid", SendMessageOptions.DontRequireReceiver);
            building = null;

            EmptyOccupySprites();
            SelectBuilding(lastBuild);
        }//Build single building
        else if (Input.GetMouseButtonDown(0))
        {
            TakeAreaPerm(GetColliderVertexPositionsLocal(building, buildingScript).min, buildingScript.size);
            building.SendMessage("BecomeSolid", SendMessageOptions.DontRequireReceiver);
            building = null;

            EmptyOccupySprites();
        }
    }

    //Generate building blueprint that follows mouse
    public void SelectBuilding(GameObject build)
    {
        if (building != null) Destroy(building);
        building = Instantiate(build);
        lastBuild = build;
        buildingScript = build.GetComponent<BuildingBase>();

        int number = 0;
        for (int i = 0; i < buildingScript.size.y; i ++)
        {
            for (int ii = 0; ii < buildingScript.size.x; ii ++)
            {
                var y = listOfOccupationTilesInScene[number];
                number++;
                y.SetActive(true);
                y.transform.position = TileGenerator.singleton.grid.GetCellCenter(GetColliderVertexPositionsLocal(building,
                    buildingScript).min.x - 1 + i, GetColliderVertexPositionsLocal(building,
                    buildingScript).min.y - 1 + ii);
                y.transform.parent = building.transform.GetChild(0);
                occupationTiles.Add(y.GetComponent<SpriteRenderer>());
            }
        }
    }

    //Drag blueprint around with mouse
    void MoveWithMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
        if (resourcePoint != null && resourceBuilding) worldPoint = resourcePoint.position;
        Vector3Int position = tilemapTemp.WorldToCell(worldPoint);

        var x = TileGenerator.singleton.grid.GetCellCenter(position.x, position.y);
        building.transform.position = new Vector3(x.x + 2.5f, x.y + 2.5f, -1);

        //tilemapTemp.ClearAllTiles();
        if (occupationTiles.Count > 0 && buildingScript != null) TakeArea(GetColliderVertexPositionsLocal(building, buildingScript).min, buildingScript.size);
    }
    
    //Fetch Size of blueprint in cells
    public BoundsInt GetColliderVertexPositionsLocal(GameObject obj, BuildingBase script)
    {
        return new BoundsInt(new Vector3Int((int)obj.transform.position.x,
            (int)obj.transform.position.y), script.size * tilemapTemp.size);
    }

    //Hide tiles used to show if a space is available
    void EmptyOccupySprites()
    {
        foreach (SpriteRenderer spRend in occupationTiles)
        {
            spRend.transform.parent = transform;
            spRend.gameObject.SetActive(false);
        }

        occupationTiles = new List<SpriteRenderer>();
    }

    //Mark area under blueprint
    void TakeArea(Vector3Int start, Vector3Int size)
    {
        if (buildingScript != null)
        {
            if ((!touchingAnotherBuilding) && (!resourceBuilding ||
                (touchingCorrectResource && resourceBuilding && resourcePoint != null)))
            {
                foreach (SpriteRenderer spRend in occupationTiles)
                {
                    spRend.color = Color.green;
                }
            }
            else
            {
                foreach (SpriteRenderer spRend in occupationTiles)
                {
                    spRend.color = Color.red;
                }
            }
        }
    }

    //Mark perm area under blueprint as occupied
    public void TakeAreaPerm(Vector3Int start, Vector3Int size)
    {
        //tilemapTemp.ClearAllTiles();
        /*
        var x = new Vector3Int(start.x - (size.x / 2), start.y - (size.y / 2));
        tilemapPerm.BoxFill(x, (whiteTile), 
            x.x, x.y, x.x + size.x, x.y + size.y);
        */
    }
}