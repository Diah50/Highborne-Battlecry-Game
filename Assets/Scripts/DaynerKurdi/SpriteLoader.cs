/* SpriteLoader.cs - Highborne Universe
 * 
 * Creation Date: 31/07/2023
 * Authors: DaynerKurdi
 * Original : DaynerKurdi, C137
 * 
 * Changes: 
 *      [31/07/2023] - Initial implementation (DaynerKurdi)
 *      [01/08/2023] - Variables renaming (C137)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLoader : MonoBehaviour
{
    /// <summary>
    /// The sprite loader instance
    /// </summary>
    public static SpriteLoader Instance;

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
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        tileGrassSpriteArray = Resources.LoadAll<Sprite>("Tile/Grass");

        tileDritSpriteArray = Resources.LoadAll<Sprite>("Tile/Dirt");

        tileWaterSpriteArray = Resources.LoadAll<Sprite>("Tile/Water");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
