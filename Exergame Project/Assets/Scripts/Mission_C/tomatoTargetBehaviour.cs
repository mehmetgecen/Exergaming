using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tomatoTargetBehaviour : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "knifeTip"){
            gameObject.GetComponentInParent<sliceObjectBehaviour>().nextState();
        }
    }
}
