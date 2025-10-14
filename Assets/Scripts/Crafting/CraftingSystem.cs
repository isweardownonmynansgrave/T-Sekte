using System.Collections.Generic;
using UnityEngine;
public class CraftingSystem : MonoBehaviour // WIP
{
    [Header("Verfügbare Rezepte")]
    public List<Rezept> knownRecipes = new List<Rezept>();

    public Produkt TryCraft(List<Zutat> inputItems)
    {
        foreach (var recipe in knownRecipes)
        {
            if (recipe.Matches(inputItems))
            {
                return recipe.ergebnis;
            }
        }

        return null;
    }
}