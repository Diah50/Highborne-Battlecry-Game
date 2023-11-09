using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HBBC/New Building")]
public class BuildingScOb : ScriptableObject
{
    public string BuiildingName;
    public string BuildingDescription;
    public Sprite[] sprites;
    public int BuildingRaceType;    //race that can use this building
    public int BuildingArmyPoints;
    public int BuildingCombat;
    public int BuildingLife;
    public int BuildingSpeed;
    public int BuildingDamage;
    public int BuildingDamageType;
    public int BuildingDamageRange = 0;
    public int BuildingWeaknessType = 0;  //fire i think
    public int BuildingCost1 = 0;
    public int BuildingCost2 = 0;
    public int BuildingCost3 = 0;
    public int BuildingCost4 = 0;
    public int BuildingCost5 = 0;
    public string BuildingSpecialAbilityName;
    // spells, converting, researches, spawning units

    public int BuildingSideAllegiance; // 0-passive, 1-player1, 2-player2, ...
}
