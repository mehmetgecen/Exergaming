using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public enum FoodType
    {
        Fruit,
        Vegetable,
        Dairy
    }

    public FoodType foodType;
    private string foodName;
}
