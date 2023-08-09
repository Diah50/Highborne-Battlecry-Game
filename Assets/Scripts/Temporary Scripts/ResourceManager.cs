/* ResourceManager.cs - Highborne Universe
 * 
 * Creation Date: 08/08/2023
 * Authors: Archetype
 * Original: Archetype
 * 
 * Edited By: Archetype
 * 
 * Changes: 
 *      [08/08/2023] - Initial implementation (Archetype)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : Singleton<ResourceManager>
{
    /// <summary>
    /// Variables for food stockpile UI and amount kept
    /// </summary>
    public TextMeshProUGUI TMPFoodStockpile, TMPFoodWorkers;
    public int foodStockpile, foodWorkers;
    /// <summary>
    /// Variables for wood stockpile UI and amount kept
    /// </summary>
    public TextMeshProUGUI TMPWoodStockpile, TMPWoodWorkers;
    public int woodStockpile, woodWorkers;
    /// <summary>
    /// Variables for metal stockpile UI and amount kept
    /// </summary>
    public TextMeshProUGUI TMPMetalStockpile, TMPMetalWorkers;
    public int metalStockpile, metalWorkers;
    /// <summary>
    /// Variables for crystal stockpile UI and amount kept
    /// </summary>
    public TextMeshProUGUI TMPCrystalStockpile, TMPCrystalWorkers;
    public int crystalStockpile, crystalWorkers;
    /// <summary>
    /// Variables for stone stockpile UI and amount kept
    /// </summary>
    public TextMeshProUGUI TMPStoneStockpile, TMPStoneWorkers;
    public int stoneStockpile, stoneWorkers;
    /// <summary>
    /// Variables for gold stockpile UI and amount kept
    /// </summary>
    public TextMeshProUGUI TMPGoldStockpile, TMPGoldWorkers;
    public int goldStockpile, goldWorkers;
    [Space]
    /// <summary>
    /// Variables for population stockpile UI and amount kept
    /// </summary>
    public TextMeshProUGUI TMPPopulation;
    public int populationTotal, populationMax;
    [Space]
    /// <summary>
    /// How frequently resources are gained
    /// </summary>
    public float tickSpeed = 1;
    /// <summary>
    /// How much is gained for every worker per tick
    /// </summary>
    int resourceMultiplyer = 1;

    private void Start()
    {
        InvokeRepeating("UpdateResources", tickSpeed, tickSpeed);
    }

    void UpdateResources()
    {
        TMPFoodStockpile.text = "" + (foodStockpile + foodWorkers * resourceMultiplyer);
        TMPFoodWorkers.text = "" + foodWorkers;

        TMPWoodStockpile.text = "" + (woodStockpile + woodWorkers * resourceMultiplyer);
        TMPWoodWorkers.text = "" + woodWorkers;

        TMPMetalStockpile.text = "" + (metalStockpile + metalWorkers * resourceMultiplyer);
        TMPMetalWorkers.text = "" + metalWorkers;

        TMPCrystalStockpile.text = "" + (crystalStockpile + crystalWorkers * resourceMultiplyer);
        TMPCrystalWorkers.text = "" + crystalWorkers;

        TMPStoneStockpile.text = "" + (stoneStockpile + stoneWorkers * resourceMultiplyer);
        TMPStoneWorkers.text = "" + stoneWorkers;

        TMPGoldStockpile.text = "" + (goldStockpile + goldWorkers * resourceMultiplyer);
        TMPGoldWorkers.text = "" + goldWorkers;

        TMPPopulation.text = populationTotal + "/" + populationMax;
    }
}