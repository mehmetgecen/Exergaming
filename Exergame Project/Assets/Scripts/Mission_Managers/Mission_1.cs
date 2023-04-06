using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements.Experimental;
using System.Collections;

public class Mission_1 : MonoBehaviour
{
    #region variables for mission control
    public LevelManager levelManager;
    private bool isMissionDone;
    public Vector3 openDoorRotation;
    #endregion

    #region variables for hand depth control
    public float minZ, maxZ;
    #endregion

    #region variables for end level control
    public GameObject[] openObjects;
    public float delayTime;
    #endregion

    private void Start()
    {
        EventManager.setMaxAndMinZPoint?.Invoke(minZ, maxZ); // set hand depth limits
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("HandObjects"))
        {
            Debug.Log("Triggered");

            string handType = other.transform.parent.tag == "RightHand" ? "right" : "left";
            bool isGrabbing = EventManager.isGrabbing.Invoke(handType);

            // hand is touched and grabbing
            if ((isGrabbing && !isMissionDone))
            {
                isMissionDone = true;
                transform.DOLocalRotate(openDoorRotation, 1.5f).OnComplete(() => { levelManager.SkipMission(); });
                StartCoroutine(RunLevelEndActions());
            }
        }
    }

    private IEnumerator RunLevelEndActions()
    {
        yield return new WaitForSeconds(delayTime);

        for (int i = 0; i < openObjects.Length; i++)
        {
            openObjects[i].SetActive(true);
        }
    }
}