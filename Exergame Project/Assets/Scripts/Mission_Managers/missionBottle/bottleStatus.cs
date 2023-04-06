using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottleStatus : MonoBehaviour
{
    public string waterColor;

    public GameObject waterObject1;
    public GameObject waterObject2;
    [HideInInspector] public Vector3 initPos;

     private void Start() {
        initPos = transform.position;
     }

    public bool isMoveable;
    public bool isFinished;

    public GameObject handPivot;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("HandObjects"))
        {            

            string handType = other.transform.parent.tag == "RightHand" ? "right" : "left";
            bool isGrabbing = EventManager.isGrabbing.Invoke(handType);

            // hand is touched and grabbing
            if (isGrabbing && isMoveable)
            {
                transform.position = handPivot.transform.position;
                waterObject1.GetComponent<bottleStatus>().isMoveable = false;
                waterObject2.GetComponent<bottleStatus>().isMoveable = false;     
            }
            else
            {
                waterObject1.GetComponent<bottleStatus>().isMoveable = !waterObject1.GetComponent<bottleStatus>().isFinished ? true : false;
                waterObject2.GetComponent<bottleStatus>().isMoveable = !waterObject2.GetComponent<bottleStatus>().isFinished ? true : false;
            }
        }
    }
}
