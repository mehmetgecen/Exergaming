using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class tajineBehaviour : MonoBehaviour
{
    public GameObject side1;
    public GameObject side2;

    public bool ismoved = false;

    private void Update() {
        if(!ismoved){
            if(side1.activeSelf && side2.activeSelf){
                transform.DOLocalMoveZ(4f,1f);
                ismoved = true;
            }
        }
    }

}
