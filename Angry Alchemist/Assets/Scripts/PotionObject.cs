using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DamageType { Normal, Acid, Fire, Electric, Water }

[CreateAssetMenu(fileName = "New Potion", menuName = "Potion")]
public class PotionObject : ScriptableObject
{
    public Sprite image;
    public Sprite[] splash;
    public int plantMushCost;
    public int mineralMushCost;
    public int creatureMushCost;
    public int demonMushCost;

    public DamageType type;
    public Vector2 initDamage;
    public Vector2 damageOverTime;
    public float duration;
    [Tooltip("The amount of time (in seconds) it takes to craft this potion")]
    public float craftTime;
    public Collider2D areaOfEffect;
}
