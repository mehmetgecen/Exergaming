using DG.Tweening;
using UnityEngine;

public class Mission2HoldDetector : MonoBehaviour
{
    #region variables for mission control
    public LevelManager levelManager;
    public Transform pivotHandObject, apron, apronTarget;

    private bool isMoveReady, isMissionDone;
    private bool leftHold, rightHold;
    #endregion

    #region variales for hand control
    public bool isLeft;
    #endregion

    public GameObject currentMission;

    private void Update()
    {
        HoldAndMoveListener();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("HandObjects"))
        {
            string handType = other.transform.parent.tag == "RightHand" ? "right" : "left";
            bool isGrabbing = EventManager.isGrabbing.Invoke(handType);

            leftHold = EventManager.isGrabbing.Invoke("left");
            rightHold = EventManager.isGrabbing.Invoke("right");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Head") && isLeft)
        {
            isMissionDone = true;
            GetComponent<Collider>().enabled = false;
            apron.DOMove(apronTarget.position, 0.5f).OnComplete(() => { levelManager.SkipMission(); currentMission.SetActive(false);});
        }
    }

    private void HoldAndMoveListener()
    {
        if (isMoveReady && !isMissionDone && isLeft)
        {
            apron.position = new Vector3(pivotHandObject.position.x, pivotHandObject.position.y, apron.position.z);
        }

        isMoveReady = (leftHold && rightHold) ? true : false;
    }
}