using UnityEngine;

public class RenderLine : MonoBehaviour
{
    #region variables for rendering line
    LineRenderer lineRenderer;
    public Transform origin;
    public Transform destination;
    #endregion

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }

    void Update()
    {
        lineRenderer.SetPosition(0, origin.position);
        lineRenderer.SetPosition(1, destination.position);
    }
}