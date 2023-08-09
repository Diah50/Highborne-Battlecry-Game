/* StationeryEnity.cs - Highborne Universe
 * 
 * Creation Date: 07/08/2023
 * Authors: DaynerKurdi
 * Original : DaynerKurdi
 * 
 * Changes: 
 *      [07/08/2023] - Initial implementation (DaynerKurdi) *      
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StationeryType
{
    Unknown = 0,
    Resource = 1,
    Building = 2,
}

[RequireComponent(typeof(SpriteRenderer))]
public class StationeryEnity : MonoBehaviour
{
    /// <summary>
    /// What kind Stationery Object
    /// </summary>
    private StationeryType type;

    /// <summary>
    /// Used to store the tile info it placed upon
    /// </summary>
    private Tile TileBelongTo;

    /// <summary>
    /// Reference to the sprite image
    /// </summary>
    private Sprite sprite;

    /// <summary>
    /// Getter for Stationery Object
    /// </summary>
    public StationeryType Type { get { return type; } }

    /// <summary>
    /// Getting for which Tile Index is belong to 
    /// </summary>
    public Vector2Int CellIndex { get { return TileBelongTo.CellIndex; } }

    public BiomeType BiomeType { get { return TileBelongTo.Type; } }

    /// <summary>
    /// Getter for world position
    /// </summary>
    public Vector3 Position { get { return transform.position; } }

    /// <summary>
    /// Getter for sprite
    /// </summary>
    public Sprite Sprite { get { return sprite; } }

    public void SetupStationery(Tile tile, StationeryType type)
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
