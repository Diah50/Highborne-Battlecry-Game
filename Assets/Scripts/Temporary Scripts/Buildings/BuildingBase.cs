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
    /// <summary>
    /// How many tile the building occupies, z can be left at 1
    /// </summary>
    public Vector3Int size;

    /// <summary>
    /// The building's sprite renderer
    /// </summary>
    public SpriteRenderer sprite;

    /// <summary>
    /// The different sprite's representing how finished the building is, buildDone should be placed as the default sprite
    /// </summary>
    public Sprite buildPhase1, buildPhase2, buildPhase3, buildDone;

    /// <summary>
    /// This game object's rigid body 2D, will be removed after the building is placed for performance
    /// </summary>
    Rigidbody2D rb2D;

    /// <summary>
    /// How far along the building is from being finished out of 100
    /// </summary>
    float buildPercent = 0;

    /// <summary>
    /// Whether or not the building has finished construction
    /// </summary>
    public bool built;

    public virtual void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Building")) 
            BuildingManager.singleton.touchingAnotherBuilding = true;
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Building")) 
            BuildingManager.singleton.touchingAnotherBuilding = false;
    }

    //Do things when the blueprint is placed
    public virtual void BecomeSolid()
    {
        Destroy(rb2D);
        sprite.color = new Color(1, 1, 1, 1);
        sprite.sprite = buildPhase1;
        gameObject.layer = LayerMask.NameToLayer("Building");
    }

    //Any builder working on this building should periodically trigger this
    public void ConstructBuild(float buildAmount)
    {
        buildPercent += buildAmount;
        if (buildPercent >= 100)
        {
            if (!built) BuildDone();
        }
        else if (buildPercent >= 67)
        {
            sprite.sprite = buildPhase3;
        }
        else if (buildPercent >= 33)
        {
            sprite.sprite = buildPhase2;
        }
    }

    //Do things when the building finishes construction
    public virtual void BuildDone()
    {
        sprite.sprite = buildDone;
        built = true;
    }
}
