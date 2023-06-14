using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostSpongeBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Sponge")){
            other.GetComponent<Cleaning>().nextState();
        }
    }
}