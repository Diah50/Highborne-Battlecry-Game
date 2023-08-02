/* Tile.cs - Highborne Universe
 * 
 * Creation Date: 30/07/2023
 * Authors: DaynerKurdi, C137
 * Original : DaynerKurdi
 * 
 * Changes: 
 *      [30/07/2023] - Initial implementation (DaynerKurdi)
 *      [01/08/2023] - Moved BiomeType enum to this script (C137)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Global Enum for Biome
/// </summary>
public enum BiomeType
{
    Unknown = 0,
    Grass = 1,
    Dirt = 2,
    Water = 3,
}

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{
    /// <summary>
    /// Tile Biome Type (0 is Unknown)
    /// </summary>
    private BiomeType type;

    /// <summary>
    /// Reference to the sprite image
    /// </summary>
    private Sprite sprite;

    /// <summary>
    /// The cell index on the grid
    /// </summary>
    private Vector2Int cellIndex;

    /// <summary>
    /// To be used with A*
    /// </summary>
    private int movmentCost = 0;

    /// <summary>
    /// Getter for type
    /// </summary>
    public BiomeType Type {  get { return type; } }

    /// <summary>
    /// Getter for sprite
    /// </summary>
    public Sprite Sprite { get { return sprite; } }

    /// <summary>
    /// Getter for movmentCost
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
