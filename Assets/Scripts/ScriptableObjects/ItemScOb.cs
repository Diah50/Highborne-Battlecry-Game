using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HBBC/New Item")]
public class ItemScOb : ScriptableObject
{
    public string ItemName;
    public string ItemDescription;
    public Sprite sprite;
    public int CombatBonus;
    public int LifeBonus;
    public int SpeedBonus;
    public int DamageBonus;
    public int DamageRangeBonus;
    public int ArmorBonus;
    public int ResistanceBonus;
}