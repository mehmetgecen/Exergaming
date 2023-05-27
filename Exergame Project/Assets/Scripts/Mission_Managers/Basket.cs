using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Basket : MonoBehaviour
{
   public Food.FoodType acceptedFoodType;
   public Basket nextBasket;
   
   public int maxBasketCapacity = 4;
   public int currentItemsCount = 0;
   public int placeCounter;

   public bool CanAcceptFood(Basket basket, Food food)
   {
      return basket.acceptedFoodType == food.foodType;
   }
   
   public bool IsFull()
   {
      return currentItemsCount >= maxBasketCapacity;
   }
   
   public void SwitchToNextBasket()
   {
      gameObject.SetActive(false);
      nextBasket.gameObject.SetActive(true);

      // Reset currentItemsCount
      currentItemsCount = 0;
      placeCounter = 0;

   }
   
   public void AddItemToBasket(GameObject food)
   {
      currentItemsCount++;

      if (IsFull())
      {
         SwitchToNextBasket();
      }
      
   }

   public int GetPlaceCounter()
   {
      return placeCounter;
   }

   public void IncrementPlaceCounter()
   {
      placeCounter++;
   }

   
   
   
}
