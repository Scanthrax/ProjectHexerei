using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]
public class RecipeObject : ScriptableObject
{
    public List<IngredientObject> listOfIngredients;
    public Potion resultingPotion;
    public float researchTime;

}
