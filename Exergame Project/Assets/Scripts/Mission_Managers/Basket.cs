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
   public int placeCounter = 0;
   
   public bool isCurrentBasket;

   public bool CanAcceptFood(Basket basket, Food.FoodType food)
   {
      return basket.acceptedFoodType.Equals(food);
   }
   
   public bool IsFull()
   {
      return currentItemsCount >= maxBasketCapacity;
   }
   
   public void SwitchToNextBasket()
   {
      gameObject.SetActive(false);
      nextBasket.gameObject.SetActive(true);

      ResetCounters();
   }

   private void ResetCounters()
   {
      currentItemsCount = 0;
      placeCounter = 0;
   }

   /*public void AddItemToBasket(GameObject food)
   {
      currentItemsCount++;
      placeCounter++;

      if (IsFull())
      {
         SwitchToNextBasket();
      }
      
   }*/

   public int GetPlaceCounter()
   {
      return placeCounter;
   }

   public int GetCurrentIndex()
   {
      return currentItemsCount;
   }

   public void IncrementCounters()
   {
      placeCounter++;
      currentItemsCount++;
   }
   
   
}
