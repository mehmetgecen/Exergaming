using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSorter : MonoBehaviour
{
    private void Start()
    {
        // Find all food objects in the scene
        Food[] foods = FindObjectsOfType<Food>();

        // Iterate through each food object and sort them into the proper baskets
        foreach (Food food in foods)
        {
            // Find the appropriate basket based on the food type
            GameObject basketObject = GameObject.Find(food.foodType.ToString() + "Basket");

            if (basketObject != null)
            {
                Basket basket = basketObject.GetComponent<Basket>();

                // Check if the basket accepts this type of food
                if (basket.acceptedFoodType == food.foodType)
                {
                    
                    //TODO Slot Replacer will be used.
                    food.transform.parent = basketObject.transform;
                }
                else
                {
                    Debug.LogWarning("The food object cannot be sorted into this basket.");
                }
            }
            else
            {
                Debug.LogError("Basket object not found for food type: " + food.foodType.ToString());
            }
        }
    }
}
