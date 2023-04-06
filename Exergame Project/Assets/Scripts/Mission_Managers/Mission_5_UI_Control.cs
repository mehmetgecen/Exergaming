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

    /*private void Awake()
    {
        Debug.Log("Mission Counter :" + levelmanager.missionCounter);
            
        leftRecipeButton.gameObject.SetActive(true);
        rightRecipeButton.gameObject.SetActive(true);
        
    }*/
    
    public void LeftRecipeOperations()
    {
        isLeftRecipeSelected = true;
        
        leftRecipeButton.gameObject.SetActive(false);
        rightRecipeButton.gameObject.SetActive(false);
        
        leftRecipeList.gameObject.SetActive(true);

        StartCoroutine(DisplayRecipe());

    }

    public void RightRecipeOperations()
    {
        isRightRecipeSelected = true;
        
        leftRecipeButton.gameObject.SetActive(false);
        rightRecipeButton.gameObject.SetActive(false);
        
        rightRecipeList.gameObject.SetActive(true);
        
        StartCoroutine(DisplayRecipe());
    }

    public IEnumerator DisplayRecipe()
    {
        yield return new WaitForSeconds(3);

        if (leftRecipeList.IsActive())
        {
            leftRecipeList.gameObject.SetActive(false);
        }

        if (rightRecipeButton.IsActive())
        {
            rightRecipeList.gameObject.SetActive(false);
        }
        
    }
    
    
    
}
