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
 *      [10/08/2023] - Aded script paramaters for a neutral resource building that can be captured, also custom Editor (Archetype)
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class BuildingBase : MonoBehaviour
{
    /// <summary>
    /// Custom Editor for convenience
    /// </summary>
    #region Editor
#if UNITY_EDITOR
    [CustomEditor(typeof(BuildingBase))]
    public class BuildingBaseEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            BuildingBase buildingBase = (BuildingBase)target;

            buildingBase.playerBuild = EditorGUILayout.Toggle("Is this a player building?", buildingBase.playerBuild);
            if (buildingBase.playerBuild)
            {
                EditorGUILayout.LabelField("Sprites");

                var spriteProperty = serializedObject.FindProperty("buildPhase1");
                EditorGUILayout.PropertyField(spriteProperty);

                spriteProperty = serializedObject.FindProperty("buildPhase2");
                EditorGUILayout.PropertyField(spriteProperty);

                spriteProperty = serializedObject.FindProperty("buildPhase3");
                EditorGUILayout.PropertyField(spriteProperty);

                spriteProperty = serializedObject.FindProperty("buildDone");
                EditorGUILayout.PropertyField(spriteProperty);

                serializedObject.ApplyModifiedProperties();
            }

            base.OnInspectorGUI();
        }
    }
#endif
    #endregion

    /// <summary>
    /// How many tile the building occupies, z can be left at 1
    /// </summary>
    [Header("How many tiles the building occupies +1\n(so 2, 2, 1 will mean 3x3), z can be left at 1")]
    public Vector3Int size;

    /// <summary>
    /// The building's sprite renderer
    /// </summary>
    SpriteRenderer sprite;

    /// <summary>
    /// Mark as true if this is a building that a player can build
    /// </summary>
    [HideInInspector]public bool playerBuild;

    /// <summary>
    /// The different sprite's representing how finished the building is, buildDone should be placed as the default sprite
    /// </summary>
    [HideInInspector] public Sprite buildPhase1, buildPhase2, buildPhase3, buildDone;

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
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        
        if (!playerBuild) BuildingManager.singleton.TakeAreaPerm
                (BuildingManager.singleton.GetColliderVertexPositionsLocal(transform.GetChild(1).gameObject, this).min, size);
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
        if (!playerBuild) return;
        Destroy(rb2D);
        sprite.color = new Color(1, 1, 1, 1);
        sprite.sprite = buildPhase1;
        gameObject.layer = LayerMask.NameToLayer("Building");
    }

    //Any builder working on this building should periodically trigger this
    public void ConstructBuild(float buildAmount)
    {
        if (!playerBuild) return;
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
