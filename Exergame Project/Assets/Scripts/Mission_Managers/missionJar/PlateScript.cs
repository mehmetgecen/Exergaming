using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateScript : MonoBehaviour
{
    public GameObject[] spiceChunks;

    public int state = -1;
     private void OnTriggerEnter(Collider other) {
        if(other.name == "Spice")
        {
            other.GetComponent<JarLidScript>().isMoveable = false;
            other.transform.position = other.GetComponent<JarLidScript>().initPos;
            IncrementState();        
        }
    }

    public void IncrementState()
    {
        state++;
        for (int i = 0; i < spiceChunks.Length; i++)
        {
            spiceChunks[i].SetActive(false);
        }
        spiceChunks[state].SetActive(true);
    }


}
