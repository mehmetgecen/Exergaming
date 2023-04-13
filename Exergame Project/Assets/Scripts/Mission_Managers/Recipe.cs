using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Recipe : MonoBehaviour
{
     #region variables for replace mechanic
    public HandTracking handTracking;
    public ObjectType[] objectTypes;
    public LevelManager levelManager;

    public GameObject nextMission;
    
    private bool isMissionDone;
    [SerializeField] private int capacity;
    [SerializeField] private int placeCounter = 0;
    #endregion

    public Mission_5_UI_Control UIControl;


    public GameObject textParentObject;
    public List<GameObject> requiredIngredients; // List of required ingredients for this recipe
    public List<GameObject> selectedIngredients; // List of selected ingredients by the player
    public AudioSource correctIngredientSound; // Sound to play when a correct ingredient is selected
    
    void Start()
    {
        capacity = transform.childCount;
        UIControl = GetComponent<Mission_5_UI_Control>();

        /*if (UIControl.isLeftRecipeSelected)
        {
            // use left recipe ingredients;
            // requiredText = leftlisttext
        }

        if (UIControl.isRightRecipeSelected)
        {
            // use right recipe selected
            // requiredText = rightListText
        }*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.GetComponent<Interactable>())
        {
            Debug.Log("Triggered");
            
            AddIngredient(collision.gameObject);
            
            Interactable interactable = collision.gameObject.GetComponent<Interactable>();

            // hand check
            if (interactable.handType.Equals("right"))
                handTracking.ResetHand("right");
            else
                handTracking.ResetHand("left");

            // if not accepted
            if (!ControlObjectType(interactable.objectType))
            {
                interactable.ReturnObjectToInitialPosition();
                return;
            }

            collision.gameObject.GetComponent<Collider>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            collision.gameObject.transform.DOMove(transform.GetChild(placeCounter).position, 0.5f);

            placeCounter++;
        }
        
        if (placeCounter >= capacity && !isMissionDone)
        {
            isMissionDone = true;
            nextMission.SetActive(true);
            levelManager.SkipMission();
            
        }
    }
    

    private bool ControlObjectType(ObjectType type)
    {
        bool isAccepted = false;

        for (int i = 0; i < objectTypes.Length; i++)
        {
            if (objectTypes[i].Equals(type))
            {
                isAccepted = true;
                break;
            }
        }

        return isAccepted;
    }
    
    public void AddIngredient(GameObject ingredient)
    {
        selectedIngredients.Add(ingredient);

        // Remove the ingredient from the required ingredients list if it matches a required ingredient
        if (requiredIngredients.Contains(ingredient))
        {
            requiredIngredients.Remove(ingredient);
            //UpdateRequiredIngredientsText();

            // Play the correct ingredient sound
            //correctIngredientSound.Play();

            print(ingredient.name);

            CheckUIText(textParentObject,ingredient);
            
        }
        else
        {
            Debug.Log("Incorrect ingredient selected!");
        }

        // Check if all required ingredients have been selected
        if (requiredIngredients.Count == 0)
        {
            // Move on to the next step in the recipe
            Debug.Log("All required ingredients selected!");
        }
    }

    // Update the required ingredients text object with the list of required ingredients
    private void UpdateRequiredIngredientsText()
    {
        string requiredIngredientsString = "";
        foreach (GameObject ingredient in requiredIngredients)
        {
            requiredIngredientsString = ingredient.name + "\n";
        }
        //requiredIngredientsText.text = requiredIngredientsString;
    }

    private void CheckUIText(GameObject textParent,GameObject ingredient)
    {
        for (int i = 0; i < textParent.transform.childCount; i++)
        {
            if (textParent.transform.GetChild(i).name.Equals(ingredient.name))
            {
                textParent.transform.GetChild(i).GetComponent<TextMeshProUGUI>().color = Color.green;
            }
        }
    }
}
