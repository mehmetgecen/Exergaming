using System;
using UnityEngine;
using System.Collections;

public class HandTracking : MonoBehaviour
{
    #region variables for get data from socket
    private UDPReceive udpReceive;
    #endregion

    #region varables for hand model
    public Transform rightHandPoints, leftHandPoints;
    #endregion

    #region variables for grabbed object information
    [Header("Pickup Settings")]
    [SerializeField]
    private Transform holdArea;
    public GameObject heldObj;
    private Rigidbody heldObjRB;
    #endregion

    #region variables for object grabbing
    public float grabbingRadius = 3.0f;
    private GameObject rightHandedObj = null, leftHandedObj = null;
    #endregion

    #region variables for distance control
    private float minZPoint = -1, maxZPoint = -1; // if value is -1, it is not setted. So there is no depth control
    #endregion

    #region variables for hand shapes
    private bool isRightGrab, isLeftGrab;
    #endregion

    private void OnEnable()
    {
        EventManager.updateHandPoints += UpdateHandPoints;
        EventManager.isGrabbing += IsGrabbing;
        EventManager.closeHandsInTravel += CloseHandsInTravel;
        EventManager.setMaxAndMinZPoint += SetMaxAndMinZPoint;
    }

    private void OnDisable()
    {
        EventManager.updateHandPoints -= UpdateHandPoints;
        EventManager.isGrabbing -= IsGrabbing;
        EventManager.closeHandsInTravel -= CloseHandsInTravel;
        EventManager.setMaxAndMinZPoint -= SetMaxAndMinZPoint;
    }

    void Start()
    {
        udpReceive = GetComponent<UDPReceive>();
    }

    void Update()
    {
        EventManager.updateHandPoints?.Invoke();
    }

    private void UpdateHandPoints()
    {
        string data = udpReceive.data;

        // if hands are not in camera view
        if (data.Length.Equals(0))
        {
            leftHandPoints.gameObject.SetActive(false);
            rightHandPoints.gameObject.SetActive(false);

            ResetHand("right");
            ResetHand("left");

            isRightGrab = false;
            isLeftGrab = false;

            data = "";
            return;
        }

        string[] dataArr = data.Split('?');

        // RIGHT POSES + ? + LET POSES + ? + RIGHT DIST + ? + LEFT DIST
        string rightPos = dataArr[0];
        string leftPos = dataArr[1];
        float rightDist = float.Parse(dataArr[2]);
        float leftDist = float.Parse(dataArr[3]);

        // left hand control
        if (!leftDist.Equals(-1))
        {
            leftHandPoints.gameObject.SetActive(true);


            // prepare left hand data
            string leftHandData = leftPos.Substring(1, leftPos.Length - 2);
            string[] leftPoints = leftHandData.Split(',');

            // grabbing control
            GrabbingControl(leftPoints, "left");

            for (int i = 0; i < 21; i++)
            {
                float x = 7f - float.Parse(leftPoints[i * 4]) / 100f;
                float y = float.Parse(leftPoints[i * 4 + 1]) / 100f;
                float z = float.Parse(leftPoints[i * 4 + 3]) / 20f - float.Parse(leftPoints[i * 4 + 2]) / 70f;

                // z depth control
                if (!minZPoint.Equals(-1))
                {
                    z = z > maxZPoint ? maxZPoint : z;
                    z = z < minZPoint ? minZPoint : z;
                }

                leftHandPoints.GetChild(i).localPosition = new Vector3(x, y, z);
            }
        }
        else
        {
            ResetHand("left");
            isLeftGrab = false;
            leftHandPoints.gameObject.SetActive(false);
        }

        // right hand control
        if (!rightDist.Equals(-1))
        {
            rightHandPoints.gameObject.SetActive(true);

            string rightHandData = rightPos.Substring(1, rightPos.Length - 2);
            string[] rightPoints = rightHandData.Split(',');

            // grabbing control
            GrabbingControl(rightPoints, "right");


            for (int i = 0; i < 21; i++)
            {
                float x = 7f - float.Parse(rightPoints[i * 4]) / 100f;
                float y = float.Parse(rightPoints[i * 4 + 1]) / 100f;
                float z = float.Parse(rightPoints[i * 4 + 3]) / 20f - float.Parse(rightPoints[i * 4 + 2]) / 70f;



                // z depth control
                if (!minZPoint.Equals(-1))
                {
                    z = z > maxZPoint ? maxZPoint : z;
                    z = z < minZPoint ? minZPoint : z;
                }
                rightHandPoints.GetChild(i).localPosition = new Vector3(x, y, z);
            }
        }
        else
        {
            ResetHand("right");
            isRightGrab = false;
            rightHandPoints.gameObject.SetActive(false);
        }
    }

    private void GrabbingControl(string[] handPoints, string handType)
    {
        Vector3 point0 = new Vector3(7f - float.Parse(handPoints[0 * 4]) / 100f, float.Parse(handPoints[0 * 4 + 1]) / 100f, float.Parse(handPoints[0 * 4 + 2]) / 70f + float.Parse(handPoints[0 * 4 + 3]) / 20f);
        Vector3 point8 = new Vector3(7f - float.Parse(handPoints[8 * 4]) / 100f, float.Parse(handPoints[8 * 4 + 1]) / 100f, float.Parse(handPoints[8 * 4 + 3]) / 20f - float.Parse(handPoints[8 * 4 + 2]) / 70f);
        Vector3 point12 = new Vector3(7f - float.Parse(handPoints[12 * 4]) / 100f, float.Parse(handPoints[12 * 4 + 1]) / 100f, float.Parse(handPoints[12 * 4 + 3]) / 20f - float.Parse(handPoints[12 * 4 + 2]) / 70f);
        Vector3 point16 = new Vector3(7f - float.Parse(handPoints[16 * 4]) / 100f, float.Parse(handPoints[16 * 4 + 1]) / 100f, float.Parse(handPoints[16 * 4 + 3]) / 20f - float.Parse(handPoints[16 * 4 + 2]) / 70f);
        Vector3 point20 = new Vector3(7f - float.Parse(handPoints[20 * 4]) / 100f, float.Parse(handPoints[20 * 4 + 1]) / 100f, float.Parse(handPoints[20 * 4 + 3]) / 20f - float.Parse(handPoints[20 * 4 + 2]) / 70f);

        // if grab detected 1.5f, 1.35f, 1.5f, 1f
        if (Vector3.Distance(point0, point12) < 2f &&
            Vector3.Distance(point0, point8) < 1.85f &&
            Vector3.Distance(point0, point16) < 2f &&
            Vector3.Distance(point0, point20) < 1.5f)
        {
            if (handType.Equals("right") && !isRightGrab)
            {
                isRightGrab = true;
                FirstGrabControl("right");
            }
            if (handType.Equals("left") && !isLeftGrab)
            {
                isLeftGrab = true;
                FirstGrabControl("left");
            }
        }
        else // if not
        {
            if (handType.Equals("right") && isRightGrab)
            {
                isRightGrab = false;

                if (rightHandedObj != null)
                {
                    rightHandedObj.GetComponent<Interactable>().DropObject();
                }
            }
            if (handType.Equals("left") && isLeftGrab)
            {
                isLeftGrab = false;

                if (leftHandedObj != null)
                {
                    leftHandedObj.GetComponent<Interactable>().DropObject();
                }
            }
        }
    }

    private bool IsGrabbing(string handType)
    {
        if (handType.Equals("right"))
            return isRightGrab;
        else
            return isLeftGrab;
    }

    private void CloseHandsInTravel()
    {
        StartCoroutine(RunCloseHandsInTravel());
    }

    private IEnumerator RunCloseHandsInTravel()
    {
        rightHandPoints.transform.parent.gameObject.SetActive(false); // close hand parent object
        yield return new WaitForSeconds(1.1f);
        rightHandPoints.transform.parent.gameObject.SetActive(true); // open hand parent object
    }

    private void SetMaxAndMinZPoint(float minZ, float maxZ)
    {
        minZPoint = minZ;
        maxZPoint = maxZ;
    }

    // functions for obejct grabbing

    private void FirstGrabControl(string handType)
    {
        if (handType.Equals("right"))
        {
            Vector3 center = rightHandPoints.GetChild(9).position;
            Collider[] hitColliders = Physics.OverlapSphere(center, grabbingRadius);
            if (hitColliders.Length.Equals(0)) return;

            float distance = int.MaxValue;

            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.GetComponent<Interactable>() && hitCollider.gameObject.name.Contains("holdable"))
                {
                    float objectDistance = Vector3.Distance(hitCollider.gameObject.transform.position, center);
                    if (objectDistance < distance)
                    {
                        distance = objectDistance;
                        rightHandedObj = hitCollider.gameObject;
                    }
                }
            }

            // to prevent collider list error
            if (rightHandedObj == null) return;

            // if there is object for grabbing
            rightHandedObj.GetComponent<Interactable>().handType = handType;
            rightHandedObj.GetComponent<Rigidbody>().isKinematic = true;
            rightHandedObj.transform.SetParent(rightHandPoints.GetChild(9).GetChild(0));
            rightHandedObj.transform.localPosition = Vector3.zero;
        }

        if (handType.Equals("left"))
        {
            Vector3 center = leftHandPoints.GetChild(9).position;
            Collider[] hitColliders = Physics.OverlapSphere(center, grabbingRadius);

            if (hitColliders.Length.Equals(0)) return;

            float distance = int.MaxValue;

            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.GetComponent<Interactable>())
                {
                    float objectDistance = Vector3.Distance(hitCollider.gameObject.transform.position, center);
                    if (objectDistance < distance)
                    {
                        distance = objectDistance;
                        leftHandedObj = hitCollider.gameObject;
                    }
                }
            }

            // to prevent collider list error
            if (leftHandedObj == null) return;

            // if there is object for grabbing
            leftHandedObj.GetComponent<Interactable>().handType = handType;
            leftHandedObj.GetComponent<Rigidbody>().isKinematic = true;
            leftHandedObj.transform.SetParent(leftHandPoints.GetChild(9));
            leftHandedObj.transform.localPosition = Vector3.zero;
        }
    }

    public void ResetHand(string handType)
    {
        if (handType.Equals("right"))
        {
            if (rightHandedObj != null) rightHandedObj.GetComponent<Interactable>().DropObject();
            rightHandedObj = null;
        }
        if (handType.Equals("left"))
        {
            if (leftHandedObj != null) leftHandedObj.GetComponent<Interactable>().DropObject();
            leftHandedObj = null;
        }
    }

    private void OnDrawGizmos()
    {
        if (isRightGrab)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(rightHandPoints.GetChild(9).position, grabbingRadius);
        }
        if (isLeftGrab)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(leftHandPoints.GetChild(9).position, grabbingRadius);
        }
    }
}