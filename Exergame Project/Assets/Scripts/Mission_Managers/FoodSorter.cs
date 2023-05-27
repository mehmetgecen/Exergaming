using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class FoodSorter : MonoBehaviour
{
    [SerializeField] private Basket fruitBasket;
    [SerializeField] private Basket vegetableBasket;
    [SerializeField] private Basket dairyBasket;
    [SerializeField] private Basket starterBasket;
    
    #region variables for replace mechanic
    public HandTracking handTracking;
    public LevelManager levelManager;
    public GameObject nextMission;
    private GameObject currentMission;
    private bool isMissionDone;
    #endregion
    
    private void Start()
    {
        Food[] foods = FindObjectsOfType<Food>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Food[] foods = FindObjectsOfType<Food>();
        
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
                GameObject basketObject = starterBasket.gameObject; // will be changed as start basket

                if (basketObject != null)
                {
                    Basket basket = basketObject.GetComponent<Basket>();

                    // Check if the basket accepts this type of food
                    if (basket.CanAcceptFood(basket,food))
                    {
                        basket.AddItemToBasket(other.gameObject);
                        other.gameObject.name = "sss";
                        other.GetComponent<Interactable>().enabled = false ;
                        other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                        other.gameObject.GetComponent<Collider>().enabled = false;
                        
                        other.gameObject.transform.DOMove(basket.transform.GetChild(basket.GetPlaceCounter()).position, 0.5f);
                        other.gameObject.transform.DORotate(basket.transform.GetChild(basket.GetPlaceCounter()).eulerAngles, 0.5f);
                        basket.IncrementPlaceCounter();
                    }
                    else
                    {
                        interactable.ReturnObjectToInitialPosition();
                        Debug.LogWarning("The food object cannot be sorted into this basket.");
                    }
                }
                else
                {
                    Debug.LogError("Basket object not found for food type: " + food.foodType.ToString());
                }
            }
         
        }
        
        CheckEndLevel();

    }
    private void CheckEndLevel()
    {
        if (fruitBasket.IsFull() && vegetableBasket.IsFull() && dairyBasket.IsFull())
        {
            levelManager.SkipMission();
            isMissionDone = true;
            nextMission.SetActive(true);
            
        }
    }
    
    
}
