using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportMovement : MonoBehaviour
{
    GameObject TeleportPoint;
    float walking_dis = 1.5f;

    private Menu settings;

    // Start is called before the first frame update
    void Start()
    {
        TeleportPoint = transform.Find("TeleportPoint").gameObject;
        settings =  GameObject.FindWithTag("Settings").gameObject.GetComponent<Menu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!settings.paused)
        {
            float y = transform.Find("Camera").transform.eulerAngles.y;
            Vector3 dirDown = Quaternion.Euler(45, y, 0) * (Vector3.forward);
            Vector3 dir = Quaternion.Euler(-45, y, 0) * (Vector3.forward);

            Vector3 newPos = transform.position + dir * walking_dis;

            // Changing teleport point's position
            TeleportPoint.transform.position = new Vector3 (
                newPos.x,
                TeleportPoint.transform.position.y,
                newPos.z
            );

            // Getting a newPos based on the teleport point position
            newPos = new Vector3 (
                TeleportPoint.transform.position.x, 
                transform.position.y - TeleportPoint.GetComponent<TeleportPoint>().dif/2f,
                TeleportPoint.transform.position.z
            );

            // Teleporting to new position
            if (Input.GetKeyDown(KeyCode.W))
            {
                bool collidingInteractable = Physics.Raycast(transform.position, dirDown.normalized,
                 walking_dis*1.1f, 1 << LayerMask.NameToLayer("Interactable"))
                 || TeleportPoint.GetComponent<TeleportPoint>().collidingWithInteractable;

                if (!collidingInteractable && !Physics.Raycast(transform.position, dir.normalized , walking_dis))
                {
                    transform.position = newPos;
                    GetComponent<Player>().updateStamina(-0.25f);
                }
            }
        }
    }
}
