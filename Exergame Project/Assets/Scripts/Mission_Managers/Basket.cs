using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Basket : MonoBehaviour
{
   #region variables for replace mechanic
   public HandTracking handTracking;
   public LevelManager levelManager;
   public GameObject nextMission;
   public GameObject prevMission;
   private GameObject currentMission;
   private bool isMissionDone;
   #endregion
   
   public Food.FoodType acceptedFoodType;
   public Basket nextBasket;
   private Food[] foods;
   
   public int maxCapacity = 4;
   public int currentItemsCount = 0;
   private int placeCounter;
   
   
   
   private void Awake()
   {
      Food[] foods = FindObjectsOfType<Food>();
   }
   
   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.GetComponent<Interactable>()) // Recipe Control will be added;
      {
         Interactable interactable = other.gameObject.GetComponent<Interactable>();
         
         if (interactable.handType.Equals("right"))
            handTracking.ResetHand("right");
         else
            handTracking.ResetHand("left");
         
         foreach (Food food in foods)
         {
            // Find the appropriate basket based on the food type
            GameObject basketObject = GameObject.Find(food.foodType + "Basket"); // will be changed as start basket

            if (basketObject != null)
            {
               Basket basket = basketObject.GetComponent<Basket>();

               // Check if the basket accepts this type of food
               if (CanAcceptFood(basket,food))
               {
                  AddItemToBasket(food);
                  other.gameObject.name = "sss";
                  other.GetComponent<Interactable>().enabled = false ;
                  other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                  other.gameObject.GetComponent<Collider>().enabled = false;
                  other.gameObject.transform.DOMove(transform.GetChild(placeCounter).position, 0.5f);
                  other.gameObject.transform.DORotate(transform.GetChild(placeCounter).eulerAngles, 0.5f);
                  placeCounter++;
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
   

   private static bool CanAcceptFood(Basket basket, Food food)
   {
      return basket.acceptedFoodType == food.foodType;
   }

   
   public bool IsFull()
   {
      return currentItemsCount >= maxCapacity;
   }
   
   public void SwitchToNextBasket()
   {
      gameObject.SetActive(false);
      nextBasket.gameObject.SetActive(true);

      // Reset currentItemsCount
      currentItemsCount = 0;

      if (IsLevelEnded())
      {
         levelManager.SkipMission();
         
         isMissionDone = true;
         currentMission.SetActive(false);
         prevMission.SetActive(false);
      }
   }

   private bool IsLevelEnded()
   {
      return nextBasket == null && currentItemsCount !=0;
   }

   public void AddItemToBasket(Food food)
   {
      currentItemsCount++;

      if (IsFull())
      {
         SwitchToNextBasket();
      }
      
   }

   
   
   
}
