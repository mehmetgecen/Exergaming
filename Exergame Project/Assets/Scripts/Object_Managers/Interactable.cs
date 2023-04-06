using System.Collections;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    #region variables for object properties
    public InteractableType interactableType;
    public ObjectType objectType;
    [HideInInspector] public string handType;
    private Vector3 firstPosition;
    private Quaternion firstRotation;
    private Rigidbody rigidbody;
    #endregion

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        firstPosition = transform.position;
        firstRotation = transform.rotation;
    }

    public void DropObject()
    {
        transform.SetParent(null);
        rigidbody.isKinematic = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("OutBorder"))
        {
            ReturnObjectToInitialPosition();
        }
    }

    public void ReturnObjectToInitialPosition()
    {
        rigidbody.isKinematic = true;
        transform.SetPositionAndRotation(firstPosition, firstRotation);
        StartCoroutine(setKinematic());
    }

    IEnumerator setKinematic()
    {
        yield return new WaitForSeconds(0.1f);
        rigidbody.isKinematic = false;
    }
}