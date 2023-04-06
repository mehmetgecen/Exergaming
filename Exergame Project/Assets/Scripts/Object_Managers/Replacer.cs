using UnityEngine;
using DG.Tweening;

public class Replacer : MonoBehaviour
{
    #region variables for replace mechanic
    public HandTracking handTracking;
    public ObjectType[] objectTypes;
    public LevelManager levelManager;

    public GameObject nextMission;
    
    private bool isMissionDone;
    [SerializeField] private int capacity;
    [SerializeField] private int placeCounter = 0;
    #endregion

    void Start()
    {
        capacity = transform.childCount;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Interactable>())
        {
            Debug.Log("Triggered");
            
            Interactable interactable = collision.gameObject.GetComponent<Interactable>();

            // hand check
            if (interactable.handType.Equals("right"))
                handTracking.ResetHand("right");
            else
                handTracking.ResetHand("left");

            // if not accepted
            if (!ControlObjectType(interactable.objectType))
            {
                interactable.ReturnObjectToInitialPosition();
                return;
            }

            collision.gameObject.GetComponent<Collider>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            collision.gameObject.transform.DOMove(transform.GetChild(placeCounter).position, 0.5f);

            placeCounter++;
        }
        
        if (placeCounter >= capacity && !isMissionDone)
        {
            isMissionDone = true;
            nextMission.SetActive(true);
            levelManager.SkipMission();
            
        }
    }
    

    private bool ControlObjectType(ObjectType type)
    {
        bool isAccepted = false;

        for (int i = 0; i < objectTypes.Length; i++)
        {
            if (objectTypes[i].Equals(type))
            {
                isAccepted = true;
                break;
            }
        }

        return isAccepted;
    }
}