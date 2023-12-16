using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    private float movAcel = 3f;
    private float maxSpd = 2f;
    [SerializeField] private float neckHSpd = 3f;
    [SerializeField] private float neckVSpd = 3f;
    [SerializeField] private float neckLen = 0.532f;

    private Vector2 neck;
    private Transform parentTransform;
    private Rigidbody rb;
    private Menu settings;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        parentTransform = transform.parent.transform;
        rb = transform.parent.gameObject.GetComponent<Rigidbody>();
        neck = new Vector2(transform.eulerAngles.x, transform.eulerAngles.y);
        settings =  GameObject.FindWithTag("Settings").gameObject.GetComponent<Menu>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKey(KeyCode.W)) {Move(Vector3.forward);}
        if (Input.GetKey(KeyCode.A)) {Move(Vector3.left);}
        if (Input.GetKey(KeyCode.S)) {Move(Vector3.back);}
        if (Input.GetKey(KeyCode.D)) {Move(Vector3.right);}
        */
        if (!settings.paused)
        {
            Rotate(new Vector2(-neckVSpd * Input.GetAxis("Mouse Y"), neckHSpd * Input.GetAxis("Mouse X")));
        }
    }

    void Rotate(Vector2 r)
    {
        neck += r;
        transform.eulerAngles = new Vector3(Mathf.Clamp(neck.x, -90f, 90f) , neck.y, 0);

        float ang = Mathf.Deg2Rad * (neck.y + 0);

        transform.position = new Vector3(
            parentTransform.position.x + neckLen * Mathf.Sin(ang),
            transform.position.y,
            parentTransform.position.z + neckLen * Mathf.Cos(ang)
        );

        /*
        parentTransform.eulerAngles = new Vector3 (
            parentTransform.eulerAngles.x,
            neck.y,
            parentTransform.eulerAngles.z
        );
        */
    }

    void Move(Vector3 d)
    {
        if (rb.velocity.magnitude < maxSpd)
        {
            d = Vector3.Normalize(Quaternion.Euler(0, neck.y, 0) * d); // adjusting movement angle to camera
            rb.velocity += (d * movAcel / 100f);
        }
    }
}
