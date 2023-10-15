using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HBBC/New Unit")]
public class UnitScOb : ScriptableObject
{
    public string UnitName;
    public string UnitDescription;
    public Sprite[] sprites;
    public int UnitClassType;   //infantry, ranged, spellcaster etc.
    public int UnitRaceType;    //human, beast, demon, summoned something etc.
    public int UnitExperience = 0;
    public int UnitSetupPoints;
    public int UnitArmyPoints;
    public int UnitCombat;
    public int UnitLife;
    public int UnitSpeed;
    public int UnitDamage;
    public int UnitDamageType;
    public int UnitDamageRange = 1;
    public int UnitArmor;
    public int UnitResistance;
    public int UnitStrengthType = 0;
    public int UnitWeaknessType = 0;
    public int UnitBuildingSkill = 0;
    public int UnitProductionTime;
    public int UnitCost1 = 0;
    public int UnitCost2 = 0;
    public int UnitCost3 = 0;
    public int UnitCost4 = 0;
    public int UnitCost5 = 0;
    public string UnitSpecialAbilityName;
    // spells, converting, poison/fear (effects), etc to different script

    public int UnitSideAllegiance; // 0-passive, 1-player1, 2-player2, ...
}
