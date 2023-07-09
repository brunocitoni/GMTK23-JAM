using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    public string recipeName;
    public ItemSO result;
    public List<ItemSO> requiredIngredients;
    public RecipeSO thisRecipe;
    void Start()
    {
        recipeName = thisRecipe.recipeName;
        result = thisRecipe.recipeResult;
        requiredIngredients = thisRecipe.requiredIngredients;

        
    }
    void Update()
    {
        
    }

}



