using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeDefault", menuName = "ScriptableObjects/Recipes")]
public class RecipeSO : ScriptableObject
{
    public string recipeName;
    public ItemSO recipeResult;
    public List<ItemSO> requiredIngredients;
}








