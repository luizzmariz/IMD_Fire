using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    private float startY;
    public float dif;

    // Start is called before the first frame update
    void Start()
    {
        startY = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        float d = 1.5f;

        if (Physics.Raycast(transform.position + new Vector3(0, d, 0), Vector3.down, out hit, d + 3f))
        {
            if (hit.collider.gameObject.tag == "Stairs")
            {
                transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
            }
            else {
                transform.localPosition = new Vector3(transform.localPosition.x, startY, transform.localPosition.z);
            }
        }

        dif = startY - transform.localPosition.y;
    }
    
}
