using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Ingredient", menuName = "Ingredient")]
public class IngredientObject : ScriptableObject
{
    public new string name;
    public Sprite image;
    public DamageType type;

}
