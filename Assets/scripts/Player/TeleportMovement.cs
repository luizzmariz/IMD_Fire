using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportMovement : MonoBehaviour
{
    Transform TeleportPoint;
    float walking_dis = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        TeleportPoint = transform.Find("TeleportPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float y = transform.Find("Camera").transform.eulerAngles.y;
        Vector3 dir = Quaternion.Euler(0, y, 0) * (Vector3.forward);

        Vector3 newPos = transform.position + dir * walking_dis;
        newPos = new Vector3 (newPos.x, transform.position.y, newPos.z);

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (!Physics.Raycast(transform.position, dir.normalized , walking_dis))
            {
                transform.position = newPos;
            }
        }

        TeleportPoint.position = new Vector3 (newPos.x, TeleportPoint.position.y, newPos.z);
    }
}
