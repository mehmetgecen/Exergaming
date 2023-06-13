using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class FoodSorter : MonoBehaviour
{
    #region Food and Basket Part
    [Header("Baskets")]
    [SerializeField] private Basket fruitBasket;
    [SerializeField] private Basket vegetableBasket;
    [SerializeField] private Basket dairyBasket;
    [SerializeField] private List<GameObject> foodObjects;
    [SerializeField] private List<Basket> basketList;
    private Basket currentBasket;
    #endregion
    
    
    #region variables for replace mechanic
    public HandTracking handTracking;
    public LevelManager levelManager;
    public GameObject nextMission;
    private GameObject currentMission;
    private bool isMissionDone;
    #endregion
    
    private float disableCooldown = 0.5f;
    private AudioSource correctIngredientSound;

    private void Start()
    {
        correctIngredientSound = GetComponent<AudioSource>();
        
        fruitBasket.GetComponent<MeshCollider>().enabled = false;
        dairyBasket.GetComponent<MeshCollider>().enabled = false;
        
        // Set the initial current basket
        currentBasket = basketList[0];
        currentBasket.isCurrentBasket = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // Find the appropriate basket based on the food type
        
        if (other.gameObject.GetComponent<Food>()) // Recipe Control will be added;
        {
            Debug.Log("trigger");
            
            Interactable interactable = other.gameObject.GetComponent<Interactable>();
            
            if (interactable.handType.Equals("right"))
                handTracking.ResetHand("right");
            else
                handTracking.ResetHand("left");
            
            
            GameObject basketObject = currentBasket.gameObject;
            Food.FoodType foodType = other.gameObject.GetComponent<Food>().foodType;
                
            if (basketObject != null)
            {
                Basket basket = basketObject.GetComponent<Basket>();

                // Check if the basket accepts this type of food
                if (basket.CanAcceptFood(basket,foodType))
                {
                    Debug.Log(basket.CanAcceptFood(basket,foodType));
                    other.gameObject.name = "sss";
                    other.GetComponent<Interactable>().enabled = false ;
                    other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    other.gameObject.GetComponent<Collider>().enabled = false;
                        
                    other.gameObject.transform.DOMove(basket.transform.GetChild(basket.GetPlaceCounter()).position, 0.5f);
                    other.gameObject.transform.DORotate(basket.transform.GetChild(basket.GetPlaceCounter()).eulerAngles, 0.5f);
                        
                    foodObjects.Add(other.gameObject);
                    
                    correctIngredientSound.Play();
                        
                    basket.placeCounter++;
                    basket.currentItemsCount++;

                    if (basket.IsFull())
                    {
                        StartCoroutine(DisableFoodObjects());
                        basket.gameObject.SetActive(false);
                        Basket nextBasket = GetNextBasket();
                            
                        if (nextBasket != null)
                        {
                            // Set the next basket as the current basket
                            SetCurrentBasket(nextBasket);
                            nextBasket.gameObject.SetActive(true);
                        }
                        else
                        {
                            // No more baskets available, end the level or perform other actions
                            EndLevel();
                        }
                    }
                }
                else
                {
                        
                    interactable.ReturnObjectToInitialPosition();
                    Debug.LogWarning("The food object cannot be sorted into this basket.");
                }
            }
            else
            {
                Debug.LogError("Basket object not found for food type: " + foodType);
            }
        }

        CheckEndLevel();

    }
    
    private Basket GetNextBasket()
    {
        int currentIndex = basketList.IndexOf(currentBasket);
        int nextIndex = currentIndex + 1;

        if (nextIndex < basketList.Count)
        {
            
            return basketList[nextIndex];
            
        }

        return null;
    }

    private void SetCurrentBasket(Basket basket)
    {
        currentBasket.isCurrentBasket = false;
        currentBasket = basket;
        currentBasket.isCurrentBasket = true;
    }
    
    private bool CheckEndLevel()
    {
        return (fruitBasket.IsFull() && vegetableBasket.IsFull() && dairyBasket.IsFull());
    }

    private void EndLevel()
    {
        levelManager.SkipMission();
        isMissionDone = true;
        nextMission.SetActive(true);
    }

    public IEnumerator DisableFoodObjects()
    {
        yield return new WaitForSeconds(disableCooldown);
        
        foreach (var foodObject in foodObjects)
        {
            foodObject.SetActive(false);
        }
    }
}
