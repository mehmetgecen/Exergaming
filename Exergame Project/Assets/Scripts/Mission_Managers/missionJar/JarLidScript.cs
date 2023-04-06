using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarLidScript : MonoBehaviour
{

    public bool isMoveable;
    public GameObject handPivot;
    [HideInInspector] public Vector3 initPos;

    private void Start() {
        initPos = transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("HandObjects"))
        {            

            string handType = other.transform.parent.tag == "RightHand" ? "right" : "left";
            bool isGrabbing = EventManager.isGrabbing.Invoke(handType);

            Debug.Log(isGrabbing);
            // hand is touched and grabbing
            if (isGrabbing && isMoveable)
            {                
                transform.position = handPivot.transform.position;
            }
        }
    }
}
