using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sliceObjectBehaviour : MonoBehaviour
{
    public int knifeState = 0;// for different knife phases.
    public bool isNext = true;
    public GameObject nextObject;
    public GameObject sideObject;
    public GameObject[] ghostKnifeOBJs; // 0 - 3
    public GameObject knife;
    public void nextState(){
        if(knifeState<ghostKnifeOBJs.Length-1){
            ghostKnifeOBJs[knifeState].SetActive(false);
            knifeState++;
            ghostKnifeOBJs[knifeState].SetActive(true);
        }
        else if(isNext){
            gameObject.SetActive(false);
            nextObject.SetActive(true);
            if(sideObject!=null) sideObject.SetActive(true);
        }
        else{            
            if(sideObject!=null) sideObject.SetActive(true);
            MeshRenderer childMesh = gameObject.GetComponentInChildren<MeshRenderer>();
            childMesh.enabled = false;
            StartCoroutine(waitAndEnd());
        }
        
        IEnumerator waitAndEnd(){
            yield return new WaitForSecondsRealtime(2);
            gameObject.SetActive(false);
            knife.GetComponent<knifeHandler>().endScenerio();
        }
        
    }
}
