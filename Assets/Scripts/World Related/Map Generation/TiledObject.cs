/* StationeryEnity.cs - Highborne Universe
 * 
 * Creation Date: 07/08/2023
 * Authors: DaynerKurdi, C137
 * Original: DaynerKurdi
 * 
 * Edited By: C137
 * 
 * Changes: 
 *      [07/08/2023] - Initial implementation (DaynerKurdi)
 *      [18/08/2023] - Code review (C137)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TiledObjectType
{
    Unknown = 0,
    Resource = 1,
    Building = 2,
}

[RequireComponent(typeof(SpriteRenderer))]
public class TiledObject : MonoBehaviour
{
    /// <summary>
    /// What kind tiled object this is
    /// </summary>
    private TiledObjectType type;

    /// <summary>
    /// Used to store the tile info it placed upon
    /// </summary>
    private Tile TileBelongTo;

    /// <summary>
    /// Reference to the sprite image
    /// </summary>
    private Sprite sprite;

    /// <summary>
    /// Getter for tiled object
    /// </summary>
    public TiledObjectType Type { get { return type; } }

    /// <summary>
    /// Getting for which Tile Index is belong to 
    /// </summary>
    public Vector2Int CellIndex { get { return TileBelongTo.CellIndex; } }

    /// <summary>
    /// To what biome type does this tiled object belong to
    /// </summary>
    public BiomeType BiomeType { get { return TileBelongTo.Type; } }

    /// <summary>
    /// Getter for world position
    /// </summary>
    public Vector3 Position { get { return transform.position; } }

    /// <summary>
    /// Getter for sprite
    /// </summary>
    public Sprite Sprite { get { return sprite; } }

    public void SetupStationery(Tile tile, TiledObjectType type)
    {
        this.TileBelongTo = tile;
        this.type = type;   
    }

    public void AssignSprite(Sprite sprite)
    {
        this.sprite = sprite;
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
