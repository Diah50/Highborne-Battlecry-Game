/* BuildingBase.cs - Highborne Universe
 * 
 * Creation Date: 03/08/2023
 * Authors: Archetype
 * Original: Archetype
 * 
 * Edited By: Archetype
 * 
 * Changes: 
 *      [03/08/2023] - Initial implementation (Archetype)
 *      [08/08/2023] - Bug fixing (Archetype)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBase : MonoBehaviour
{
    public Vector3Int size;
    public bool touchingAnotherBuilding;
    public SpriteRenderer sprite;
    public Sprite buildPhase1, buildPhase2, buildPhase3, buildDone;

    Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Building")) 
            BuildingManager.singleton.touchingAnotherBuilding = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Building")) 
            BuildingManager.singleton.touchingAnotherBuilding = false;
    }

    public void BecomeSolid()
    {
        Destroy(rb2D);
        sprite.color = new Color(1, 1, 1, 1);
        sprite.sprite = buildPhase1;
    }
}
