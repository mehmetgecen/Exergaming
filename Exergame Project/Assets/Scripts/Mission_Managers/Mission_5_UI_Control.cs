using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission_5_UI_Control : MonoBehaviour
{
    public Button leftRecipeButton;
    public Button rightRecipeButton;
    
    public Image leftRecipeList;
    public Image rightRecipeList;

    public LevelManager levelmanager;

    public bool isLeftRecipeSelected = false;
    public bool isRightRecipeSelected = false;
    public string selectedRecipe;
    public bool clicked;

    public RecipeSelect RecipeSelect;

    private void Awake()
    {
        rightRecipeButton.onClick.AddListener(RightRecipeOperations);
        leftRecipeButton.onClick.AddListener(LeftRecipeOperations);
    }
    
    public void LeftRecipeOperations()
    {
        isLeftRecipeSelected = true;
        clicked = true;
        selectedRecipe = "leftRecipe";
        
        leftRecipeButton.gameObject.SetActive(false);
        rightRecipeButton.gameObject.SetActive(false);
        
        leftRecipeList.gameObject.SetActive(true);
        
        //StartCoroutine(DisplayRecipe());
        
    }

    public void RightRecipeOperations()
    {
        isRightRecipeSelected = true;
        clicked = true;
        selectedRecipe = "rightRecipe";
        
        leftRecipeButton.gameObject.SetActive(false);
        rightRecipeButton.gameObject.SetActive(false);
        
        rightRecipeList.gameObject.SetActive(true);
        
        //StartCoroutine(DisplayRecipe());
    }

    public string GetRecipeName()
    {
        return selectedRecipe;
    }

    public IEnumerator DisplayRecipe()
    {
        yield return new WaitForSeconds(3);

        if (leftRecipeList.IsActive())
        {
            leftRecipeList.gameObject.SetActive(false);
        }

        if (rightRecipeList.IsActive())
        {
            rightRecipeList.gameObject.SetActive(false);
        }
        
    }
    
    
    
}
