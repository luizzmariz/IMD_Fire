using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousMovement : MonoBehaviour
{
    public float spd = 1f;
    public float walking_dis = 1.5f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Transform cam = transform.Find("Camera").transform;

        Vector3 front = (new Vector3 (cam.forward.x, 0, cam.forward.z)).normalized;
        Vector3 right = Quaternion.Euler(new Vector3(0,90,0)) * front;

        if (Input.GetKey(KeyCode.W))
        {
            float moveSpd = spd;

            /* //Sprinting
            if (Input.GetKey(KeyCode.LeftShift)) {
                moveSpd *= 2f;
            }
            */

            float y = cam.eulerAngles.y;
            Vector3 dirDown = Quaternion.Euler(45, y, 0) * (Vector3.forward);
            Vector3 dir = Quaternion.Euler(-45, y, 0) * (Vector3.forward);

            RaycastHit ray;

            bool collidingInteractable = Physics.Raycast(transform.position, dirDown.normalized, out ray,
                walking_dis*1.1f, 1 << LayerMask.NameToLayer("Interactable")) || ray.collider == null ? false : ray.collider.isTrigger;

            // Checking if the player is not colliding with interactable objects and walls
            if (!collidingInteractable && !Physics.Raycast(transform.position, dir.normalized, walking_dis, 1<<0))
            {
                rb.MovePosition(transform.position + (front * Input.GetAxis("Vertical") * moveSpd) 
                    + (right * Input.GetAxis("Horizontal") * moveSpd));
                
                GetComponent<Player>().updateStamina(-Time.deltaTime * (moveSpd/spd) * (moveSpd/spd));

                Debug.Log("TASUKETE");
            }
        }
    }
}
