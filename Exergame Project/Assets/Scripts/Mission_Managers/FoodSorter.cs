using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FoodSorter : MonoBehaviour
{
    #region variables for replace mechanic
    public LevelManager levelManager;
    public GameObject nextMission;

    private bool isMissionDone;
    [SerializeField] private int capacity;
    [SerializeField] private int placeCounter = 0;

    #endregion

    [SerializeField] Basket[] baskets;
    
    
}
