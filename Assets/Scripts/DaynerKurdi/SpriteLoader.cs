/* SpriteLoader.cs - Highborne Universe
 * 
 * Creation Date: 31/07/2023
 * Authors: DaynerKurdi
 * Original : DaynerKurdi
 * 
 * Changes: 
 *      [31/07/2023] - Initial implementation (DaynerKurdi)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLoader : MonoBehaviour
{
    /// <summary>
    /// the sprite loader instance
    /// </summary>
    public static SpriteLoader Instance;

    /// <summary>
    /// to store all the grass sprite
    /// </summary>
    [SerializeField]
    public Sprite[] tileGrassSpriteArray;

    /// <summary>
    /// to store all the drit sprite
    /// </summary>
    [SerializeField]
    public Sprite[] tileDritSpriteArray;

    /// <summary>
    /// to store all the water sprite
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

        tileDritSpriteArray = Resources.LoadAll<Sprite>("Tile/Drit");

        tileWaterSpriteArray = Resources.LoadAll<Sprite>("Tile/Water");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
