using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeSelect : MonoBehaviour
{
    public bool isRightButtonTouched;
    public bool isLeftButtonTouched;
    
    public Button leftRecipeButton;
    public Button rightRecipeButton;

    public Mission_5_UI_Control UIControl;

    /*
    private void Start()
    {
        SimulateButtonClick(rightRecipeButton);
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Point Nine R")
        {
            SimulateButtonClick(rightRecipeButton);
            isRightButtonTouched = true;
            Debug.Log("Right Recipe Selected!");
            
        }
        
        if (other.gameObject.name == "Point Nine L")
        {
            SimulateButtonClick(leftRecipeButton);
            isLeftButtonTouched = true;
            Debug.Log("Left Recipe Selected!");
        }
        
    }
    
    private void SimulateButtonClick(Button button)
    {
        button.onClick.Invoke();
    }
    
    
}
