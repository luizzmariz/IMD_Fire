using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportMovement : MonoBehaviour
{
    float walking_dis = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float y = transform.Find("Camera").transform.eulerAngles.y;
        Vector3 dir = Quaternion.Euler(0, y, 0) * (Vector3.forward);

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (!Physics.Raycast(transform.position, dir.normalized , walking_dis))
            {
                transform.position += dir * walking_dis;
            }
        }
    }
}
