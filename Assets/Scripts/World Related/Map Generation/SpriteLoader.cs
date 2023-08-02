/* SpriteLoader.cs - Highborne Universe
 * 
 * Creation Date: 31/07/2023
 * Authors: DaynerKurdi
 * Original: DaynerKurdi
 * 
 * Edited By: C137
 * 
 * Changes: 
 *      [31/07/2023] - Initial implementation (DaynerKurdi)
 *      [01/08/2023] - Variables renaming (C137)
 *      [02/08/2023] - Use of new singleton system (C137)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLoader : Singleton<SpriteLoader>
{
    /// <summary>
    /// To store all the grass sprite
    /// </summary>
    [SerializeField]
    public Sprite[] tileGrassSpriteArray;

    /// <summary>
    /// To store all the dirt sprite
    /// </summary>
    [SerializeField]
    public Sprite[] tileDritSpriteArray;

    /// <summary>
    /// To store all the water sprite
    /// </summary>
    [SerializeField]
    public Sprite[] tileWaterSpriteArray;

    // Start is called before the first frame update
    void Start()
    {
        tileGrassSpriteArray = Resources.LoadAll<Sprite>("Map Generation/Tile/Grass");

        tileDritSpriteArray = Resources.LoadAll<Sprite>("Map Generation/Tile/Dirt");

        tileWaterSpriteArray = Resources.LoadAll<Sprite>("Map Generation/Tile/Water");
    }
}
