/* PerlinNoiseGenerator.cs - Highborne Universe
 * 
 * Creation Date: 04/08/2023
 * Authors: DaynerKurdi
 * Original : DaynerKurdi
 * 
 * Changes: 
 *      [04/08/2023] - Initial implementation (DaynerKurdi)
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

    public static int calculatNoise(int cellX, int cellY, int gridWidth, int gridHeight)
    {
        int result = 0;

        float x = (float)cellX / gridWidth * scale; 
        float y = (float)cellY / gridHeight * scale;

        float resultNoise = Mathf.PerlinNoise(x + seed, y + seed);

        result = (int)(resultNoise * 10);

      //  Debug.Log(result + "    " + x + "   " + y);

        return result;
    }
}
