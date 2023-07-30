/* Tile.cs - Highborne Universe
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
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{
    /// <summary>
    /// Tile Biome Type (0 is Unknown)
    /// </summary>
    private BiomeType type;

    /// <summary>
    /// ref to the sprite image
    /// </summary>
    private Sprite sprite;

    /// <summary>
    /// the cell index on the grid
    /// </summary>
    private Vector2Int cellIndex;

    /// <summary>
    /// to be used with A*
    /// </summary>
    private int movmentCost = 0;

    /// <summary>
    /// getter for type
    /// </summary>
    public BiomeType Type {  get { return type; } }

    /// <summary>
    /// getter for sprite
    /// </summary>
    public Sprite Sprite { get { return sprite; } }

    /// <summary>
    /// getter for movmentCost
    /// </summary>
    public int MovmentCost {get {return movmentCost;} }

    public void SetupTile (BiomeType biome, int movmentCost, Vector2Int index) 
    {
        this.type = biome;
        this.movmentCost = movmentCost;
        this.cellIndex = index;
    }

    public void AssignSprite (Sprite sprite)
    {
        this.sprite = sprite;
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
