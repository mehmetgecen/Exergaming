using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PuzzleController : MonoBehaviour
{
    public GameObject[] puzzleItems;
    public GameObject trueItem;
    
    // Start is called before the first frame update
    void Start()
    {
        int index = Random.Range(0, 3);
        Debug.Log(index);
        
        for (int i = 0; i < puzzleItems.Length; i++)
        {
            puzzleItems[i].SetActive(true);
            puzzleItems[i].GetComponent<MeshRenderer>().material.color = Color.red;

            if (i == index)
            {
                puzzleItems[i].GetComponent<MeshRenderer>().material.color = Color.green;
                trueItem = puzzleItems[index];
                trueItem.name = "trueCube";

            }
            
        }

        StartCoroutine(WaitPuzzleItems());
        
    }
    
    IEnumerator WaitPuzzleItems()
    {
        yield return new WaitForSeconds(2f);
        
        for (int i = 0; i < puzzleItems.Length; i++)
        {
            puzzleItems[i].SetActive(false);
        }
        
        yield return new WaitForSeconds(2f);
        
        for (int i = 0; i < puzzleItems.Length; i++)
        {
            puzzleItems[i].SetActive(true);
            puzzleItems[i].GetComponent<MeshRenderer>().material.color = Color.grey;
        }
    }

    
}
