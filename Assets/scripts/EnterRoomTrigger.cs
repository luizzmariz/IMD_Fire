using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterRoomTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {

        GameObject g = other.gameObject;

        if (g.tag == "Player") {
            transform.parent.transform.Find("Sala").gameObject.SetActive(true);
            Destroy(this.gameObject);
        }

    }
}
