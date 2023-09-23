/* UnitBase.cs - Highborne Universe
 * 
 * Creation Date: 19/09/2023
 * Authors: Archetype
 * Original: Archetype
 * 
 * Edited By: Archetype
 * 
 * Changes: 
 *      [19/09/2023] - Initial implementation (Archetype)
 */

using UnityEngine;

public class DetectUnits : MonoBehaviour
{
    public SelectionManager manager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "unit")
        {
            manager.AddToSelection(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "unit")
        {
            manager.RemoveFromSelection(collision.gameObject);
        }
    }
}
