/* TileGenerator.cs - Highborne Universe
 * 
 * Creation Date: 30/07/2023
 * Authors: DaynerKurdi, C137
 * Original: DaynerKurdi
 * 
 * Edited By: DaynerKurdi
 * 
 * Changes: 
 *      [30/07/2023] - Initial implementation (DaynerKurdi)
 *      [01/08/2023] - Fixed spelling mistakes + Made "Grid" serializable (C137)
 *      [02/08/2023] - Use of new singleton system (C137)
 *      [04/08/2023] - Added Perlin Noise functionality (DaynerKurdi)
 *      [07/08/2023] - Added Perlin Noise functionality for Resource (DaynerKurdi)
 *      [20/08/2023] - Code Review (C137)
 */
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class Grid
{
    #region Variables 
    /// <summary>
    /// The total number of cells on the positive X axis
    /// </summary>
    private int width;

    /// <summary>
    /// The total number of cells on the positive Y axis
    /// </summary>
    private int height;

    /// <summary>
    /// The size of each cell on the grid
    /// </summary>
    private float cellSize;

    /// <summary>
    /// Total cell count in the grid
    /// </summary>
    private int cellCount;

    /// <summary>
    /// For storing the cell index
    /// </summary>
    private Tile[,] cellArray;

    /// <summary>
    /// Used for moving the grid  
    /// </summary>
    private Vector3 gridOffset;

    /// <summary>
    /// Getter for Width
    /// </summary>
    public int Width { get { return width; } }

    /// <summary>
    /// Getter for Height
    /// </summary>
    public int Height { get { return height; } }

    /// <summary>
    /// Getter for total number of cells
    /// </summary>
    public int TotalCellCount { get { return cellCount; } }
    #endregion
    public Grid(Transform parentForCell, int width, int height, float cellSize, Vector3 offset)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.gridOffset = offset;

        this.cellArray = new Tile[width, height];

        int cellCount = 0;

        // y first
        for (int y = 0; y < this.cellArray.GetLength(1); y++) 
        {
            // x second
            for (int x = 0; x < this.cellArray.GetLength(0); x++)
            {
                GameObject cell = new GameObject(cellCount.ToString());
                cell.transform.position = GetCellCenter(x,y);

                //Scaling the object so the sprite match the cell size. roughly 
                cell.transform.localScale = new Vector3(4, 4, 1);
                cell.transform.parent = parentForCell;
            
                Tile tile = cell.AddComponent<Tile>();

                InitializeTile(tile, new Vector2Int(x, y));

                cellArray[x, y] = tile;
               
                cellCount++;
            }

        }

        this.cellCount = cellCount;
    }

    private Vector3 GetCellBorders(int x, int y)
    {
        Vector3 worldPos = new Vector3(x, y, 0) * cellSize;

        return worldPos + gridOffset;
    }

    public Vector3 GetCellCenter(int x, int y)
    {
        return GetCellBorders(x,y) + new Vector3(cellSize, cellSize,0) * 0.5f;
    }

    public Tile[,] GetCellArray()
    {
        return cellArray;
    }

    private void InitializeTile(Tile tile, Vector2Int cellIndex)
    {

        int PerlinNoiseResult = PerlinNoiseGenerator.CalculatNoise(cellIndex.x, cellIndex.y, width, height);


        switch (PerlinNoiseResult)
        {
            //water
            case 0:
            case 1:
            case 2:
                {
                    tile.SetupTile(BiomeType.Water, 0, cellIndex);
                    //Assigning the sprite to the current tile
                    tile.AssignSprite(SpriteLoader.singleton.tileWaterSpriteArray[5]);
                }
                break;
           
           //grass
           case 3:
           case 4:
           case 5:
           case 6:
                {
                    tile.SetupTile(BiomeType.Grass, 0, cellIndex);
                    //Assigning the sprite to the current tile
                    tile.AssignSprite(SpriteLoader.singleton.tileGrassSpriteArray[5]);
                }
                break;


            //dirt
            case 7:
            case 8:
            case 9:
                {
                    tile.SetupTile(BiomeType.Dirt, 0, cellIndex);
                    //Assigning the sprite to the current tile
                    tile.AssignSprite(SpriteLoader.singleton.tileDritSpriteArray[5]);

                }
                break;

            default:
                break;
        }
    }
}

public class TileGenerator : MonoBehaviour
{
    #region Grid & Tile
    [Header("Grid Setting")]
    /// <summary>
    /// The initial gird size
    /// </summary>
    [SerializeField]
    private Vector2Int gridSize = new Vector2Int();

    /// <summary>
    /// The size of each cell
    /// </summary>
    [SerializeField]
    private float cellSize = 5f;

    /// <summary>
    /// Used to move the grid
    /// </summary>
    [SerializeField]
    private Vector3 gridOffSet = Vector3.zero;

    /// <summary>
    /// The grid object
    /// </summary>
    public Grid grid;

    /// <summary>
    /// The Grid's Tile Array
    /// </summary>
    public Tile[,] tileArray;

    /// <summary>
    /// The List used to store the Nature Resources
    /// </summary>
    public List<TiledObject> NatureResourceList;
    #endregion

    #region Perlin Noise 
    [Header("-------------------")]
    [Header("Tile's Perlin Noise Setting")]

    /// <summary>
    /// The Seed for the Perlin Noise 
    /// </summary>
    [SerializeField]
    private int PerlinNoiseSeedForTile = 0;

    /// <summary>
    /// The scale of the Perlin Noise image, higher value = more randomness 
    /// </summary>
    [SerializeField]
    private float PerlinNoiseScaleForTile = 1;
    
    [Header("-------------------")]
    [Header("Resource's Perlin Noise Setting")]

    /// <summary>
    /// The Seed for the Perlin Noise 
    /// </summary>
    [SerializeField]
    private int PerlinNoiseSeedForResource = 0;

    /// <summary>
    /// The scale of the Perlin Noise image, higher value = more randomness 
    /// </summary>
    [SerializeField]
    private int PerlinNoiseScaleForResource = 1;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        TitleGeneration();
        ResourceGeneration();
    }

    private void TitleGeneration()
    {
        //Setup Perlin Noise "does not reset"
        PerlinNoiseGenerator.Seed = PerlinNoiseSeedForTile;
        PerlinNoiseGenerator.Scale = PerlinNoiseScaleForTile;

        grid = new Grid(this.transform, gridSize.x, gridSize.y, cellSize, gridOffSet);

        tileArray = new Tile[grid.Width, grid.Height];

        tileArray = grid.GetCellArray();
    }

    private void ResourceGeneration()
    {
        PerlinNoiseGenerator.Seed = PerlinNoiseSeedForResource;
        PerlinNoiseGenerator.Scale = PerlinNoiseScaleForResource;

        NatureResourceList = new List<TiledObject>();

        for (int y = 0; y < grid.Height; y++)
        {
            for (int x = 0; x < grid.Width; x++)
            {
                int noiseResult = PerlinNoiseGenerator.CalculatNoise(x, y, grid.Width, grid.Height);
                switch (noiseResult)
                {
                    case 1:
                            ResourceSelector(tileArray[x, y]);
                        break;

                    case 9:
                            ResourceSelector(tileArray[x,y]);
                        break;
                    default: 
                        break;
                }
            }
        }
    }

    private void ResourceSelector(Tile tile)
    {
        GameObject resource = new GameObject();
        resource.transform.position = new Vector3(tile.transform.position.x , tile.transform.position.y , tile.transform.position.z - 1) ;
        resource.transform.parent = tile.transform;
        resource.transform.localScale = new Vector3(1, 1, 1);

        //Reference to the tiled object of the resource
        TiledObject resourceTiledObject = resource.AddComponent<TiledObject>();

        resourceTiledObject.SetupStationery(tile, TiledObjectType.Resource);

        switch (resourceTiledObject.BiomeType)
        {
            case BiomeType.Grass:
                {
                    resourceTiledObject.gameObject.name = "Resource: Fertile Land";
                    resourceTiledObject.AssignSprite(SpriteLoader.singleton.ResourceSpriteArray[1]);
                }
                break;
            case BiomeType.Dirt:
                {
                    resourceTiledObject.gameObject.name = "Resource: Iron Deposit";
                    resourceTiledObject.AssignSprite(SpriteLoader.singleton.ResourceSpriteArray[4]);
                }
                break;
            case BiomeType.Water:
                {
                    resourceTiledObject.gameObject.name = "Resource: Fish Pond";
                    resourceTiledObject.AssignSprite(SpriteLoader.singleton.ResourceSpriteArray[2]);
                }
                break;

            default:
                break;
        }
    }
}
