/* WorldCanvasManager.cs - Highborne Universe
 * 
 * Creation Date: 12/08/2023
 * Authors: Archetype
 * Original: Archetype
 * 
 * Edited By: Archetype
 * 
 * Changes: 
 *      [12/08/2023] - Initial implementation (Archetype)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldCanvasManager : Singleton<WorldCanvasManager>
{
    /// <summary>
    /// Healthbar prefab that can be placed into the scene
    /// </summary>
    public GameObject healthBar;

    /// <summary>
    /// WorldSpace Canvas for UI that can be instantiated in specific spots on the map instead of HUD
    /// </summary>
    public GameObject worldCanvas;

    //Instantiate health bar for building
    public Slider AskForHealthBar(GameObject parent)
    {
        GameObject x = Instantiate(healthBar, parent.transform.GetChild(2).position, 
            parent.transform.GetChild(2).rotation, worldCanvas.transform);

        return x.GetComponent<Slider>();
    }
}
