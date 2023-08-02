/* PlayerResources.cs - Highborne Universe
 * 
 * Creation Date: 29/07/2023
 * Authors: C137
 * Original : C137
 * 
 * Changes: 
 *      [29/07/2023] - Initial implementation (C137)
 */
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Base code for each resource
/// </summary>
public struct Resource
{
    public string name;
    public string description;

    public float amount;
    public float? workerCount;

    public TextMeshProUGUI amountShower;
#nullable enable
    public TextMeshProUGUI? workerShower;
#nullable disable

    public void UpdateShowers()
    {
        amountShower.text = amount.ToString();

        if (workerCount != null && workerCount != null)
            workerShower.text = workerCount.ToString();
    }
}

public class PlayerResources : MonoBehaviour
{
    /// <summary>
    /// Singleton reference 
    /// </summary>
    public static PlayerResources singleton;

    /// <summary>
    /// Food resource
    /// </summary>
    public Resource food;

    /// <summary>
    /// Wood resource
    /// </summary>
    public Resource wood;

    /// <summary>
    /// Metal resource
    /// </summary>
    public Resource metal;

    /// <summary>
    /// Crystal resource
    /// </summary>
    public Resource crystal;

    /// <summary>
    /// Stone resource
    /// </summary>
    public Resource stone;

    /// <summary>
    /// Gold resource
    /// </summary>
    public Resource gold;

    /// <summary>
    /// Population "resource"
    /// </summary>
    public Resource population;

    private void Awake()
    {
        if (singleton != null)
        {
            Destroy(this);
            return;
        }

        singleton = this;
    }

    void UpdateValues()
    {
        food.UpdateShowers();
        wood.UpdateShowers(); 
        metal.UpdateShowers();
        crystal.UpdateShowers();

        population.UpdateShowers();
        gold.UpdateShowers();
    }
}
