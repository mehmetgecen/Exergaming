using UnityEngine;

public class OpenDoorScript : MonoBehaviour
{
    private Animator anim;
    public GameObject p1;
    public GameObject p2;
    public bool grasp = false;
    public bool in_grasp = false;
    public int grasp_counter = 0;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        var angle = Mathf.Atan2(p2.transform.position.y - p1.transform.position.y, p2.transform.position.x - p1.transform.position.x) * 180 / Mathf.PI;

        if (angle < -80f)
        {
            grasp = true;
        }
        else
        {
            grasp = false;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            rotationFunc();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "HandObjects") && grasp == true)
        {
            rotationFunc();
        }
    }

    private void rotationFunc()
    {
        if (transform.rotation.eulerAngles.y == 270) transform.Rotate(0, 90, 0);
        else anim.SetTrigger("OpenTrigger");
    }
}