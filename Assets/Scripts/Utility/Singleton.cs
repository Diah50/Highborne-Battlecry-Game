/* Singleton.cs - Highborne Universe
 * 
 * Creation Date: 28/07/2023
 * Authors: Archetype, C137
 * Original: Archetype
 * 
 * Edited By: C137
 * 
 * Changes: 
 *      [29/07/2023] - Initial implementation (Archetype)
 *      [01/08/2023] - Code cleanup + Error throwing (C137)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T:Component
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<T>();

                //If no instance is found, should cause an exception rather than making a new instance
                throw new System.Exception("No instance of the singleton was found");

                //if(instance == null)
                //{
                //    GameObject gameObject = new GameObject("Controller");
                //    instance = gameObject.AddComponent<T>();
                //}
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this as T;
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}