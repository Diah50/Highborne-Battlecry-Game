/* HeroUnitScOb.cs - Highborne Universe
 * 
 * Creation Date: 06/09/2023
 * Authors: DaynerKurdi
 * Original: DaynerKurdi
 * 
 * Changes: 
 *      [06/09/2023] - Initial implementation (DaynerKurdi)
 */
using UnityEngine;

/// <summary>
/// The Unit Class List
/// </summary>
public enum UnitClass
{
    Infantry,
    Ranged, 
    Spellcaster,
    Calvary,
}

/// <summary>
/// The Unit Race List
/// </summary>
public enum UnitRace
{
    Human,
    Elypions,
    Beast,
    Demon,
    SummonedBest,
}

/// <summary>
/// The Faction list
/// </summary>
public enum UnitAllegiance
{
    Natural, 
    Kinhnami,
    LongHourn,
}



public abstract class UnitBaseScOb : ScriptableObject
{
    /// <summary>
    /// The unit name
    /// </summary>
    public string unitName;

    /// <summary>
    /// The unit description
    /// </summary>
    public string unitDescription;

    /// <summary>
    /// The unit Sprite
    /// </summary>
    public Sprite sprites;

    /// <summary>
    /// The unit class assigned to 
    /// </summary>
    public UnitClass unitClass; 

    /// <summary>
    /// Which race this unit born as
    /// </summary>
    public UnitRace unitRace;

    /// <summary>
    /// Which faction this unit swae allegiance to
    /// </summary>
    public UnitAllegiance unitAllegiance;

    /// <summary>
    /// The current level of the unit
    /// </summary>
    public int unitLevel;

    /// <summary>
    /// The experience needed for the reaching the next level
    /// </summary>
    public int maxExperience = 10;

    /// <summary>
    /// The current experience gained thus far 
    /// </summary>
    public int currentExperience = 0;

    /// <summary>
    /// The time needed for the unit to spawn on the map from the building
    /// </summary>
    public float UnitTraningTime;

    /// <summary>
    /// The cost to train the unit //todo update the unit cost once decided 
    /// </summary>
    public int traningCost = 0;

    /// <summary>
    /// The max amount of HP the unit has
    /// </summary>
    public int maxHealth;

    /// <summary>
    /// The current amount of HP the unit has
    /// </summary>
    public int currentHealth;

    /// <summary>
    /// The unit attack speed
    /// </summary>
    public float attackSpeed;

    /// <summary>
    /// The unit movement speed
    /// </summary>
    public float movmentSpeed;

    /// <summary>
    /// The unit attack power
    /// </summary>
    public int attackPower;

    /// <summary>
    /// The unit attack range
    /// </summary>
    public int attackRange = 1;

    /// <summary>
    /// The unit defence power
    /// </summary>
    public int defencePower;

    /// <summary>
    /// The unit resistance power
    /// </summary>
    public int resistancePower;
}
