/* TileGenerator.cs - Highborne Universe
 * 
 * Creation Date: 30/07/2023
 * Authors: DaynerKurdi
 * Original : DaynerKurdi
 * 
 * Changes: 
 *      [30/07/2023] - Initial implementation (DaynerKurdi)
 */
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grid
{
    /// <summary>
    /// the total number of cells on the postive X axie
    /// </summary>
    private int width;

    /// <summary>
    /// the total number of cells on the postive Y axie
    /// </summary>
    private int height;

    /// <summary>
    /// the size of each cell on the grid
    /// </summary>
    private float cellSize;

    /// <summary>
    /// storing the cell index
    /// </summary>
    private int[,] cellArray;

    /// <summary>
    /// used for moving the grid  
    /// </summary>
    private Vector3 gridOffset;

    public Grid(int width, int height, float cellSize, Vector3 offset)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.gridOffset = offset;

        this.cellArray = new int[width, height];

        string output = "";

        int cellCount = 0;

        // y first
        for (int y = 0; y < this.cellArray.GetLength(1); y++) 
        {
            // x secend
            for (int x = 0; x < this.cellArray.GetLength(1); x++)
            {
                if (x == 0)
                {
                    output = y.ToString();
                }

                output = output + "  " + x.ToString();

                Debug.DrawLine(this.GetCellBorders(x, y), this.GetCellBorders(x, y + 1), Color.white, 100f);
                Debug.DrawLine(this.GetCellBorders(x, y), this.GetCellBorders(x + 1, y), Color.white, 100f);

                //for testing
                GameObject cell = new GameObject(cellCount.ToString());
                cell.transform.position = GetCellCenter(x,y);
                
                cellCount++;
            }

            Debug.Log(output);
        }

        Debug.DrawLine(this.GetCellBorders(0, this.width), this.GetCellBorders(this.width, this.height), Color.white, 100f);
        Debug.DrawLine(this.GetCellBorders(this.width, 0), this.GetCellBorders(this.width, this.height), Color.white, 100f);
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
}

public class TileGenerator : MonoBehaviour
{

    /// <summary>
    /// the initial gird size
    /// </summary>
    [SerializeField]
    private Vector2Int gridSize = new Vector2Int();

    /// <summary>
    /// the size of each cell
    /// </summary>
    [SerializeField]
    private float cellSize = 5f;

    /// <summary>
    /// used to move the grid
    /// </summary>
    [SerializeField]
    private Vector3 GridOffSet = Vector3.zero;

    /// <summary>
    /// the grid object
    /// </summary>
    public Grid grid;

    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid(gridSize.x, gridSize.y, cellSize, GridOffSet );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}