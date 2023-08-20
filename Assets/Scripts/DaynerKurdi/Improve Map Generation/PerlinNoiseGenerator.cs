/* PerlinNoiseGenerator.cs - Highborne Universe
 * 
 * Creation Date: 04/08/2023
 * Authors: DaynerKurdi, C137
 * Original: DaynerKurdi
 * 
 * Edited By: C137
 * 
 * Changes: 
 *      [04/08/2023] - Initial implementation (DaynerKurdi)
 *      [18/08/2023] - Code review (C137)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public static class PerlinNoiseGenerator
{
    private static int seed = 0;
    private static float scale = 1.0f;

    public static int Seed { get { return seed; } set { seed = value; } }
    public static float Scale { get { return scale; } set { scale = value; } }

    public static int CalculatNoise(int cellX, int cellY, int gridWidth, int gridHeight)
    {
        float x = (float)cellX / gridWidth * scale; 
        float y = (float)cellY / gridHeight * scale;

        float resultNoise = Mathf.PerlinNoise(x + seed, y + seed);

        return (int)(resultNoise * 10);
    }
}
