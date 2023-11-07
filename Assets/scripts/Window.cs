using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    public bool closed = true;
    [SerializeField] float interactDis = 2.7f;

    private GameObject windowDoor;
    private Transform player;
    private DoorText textPrompt;

    // Start is called before the first frame update
    void Start()
    {
        windowDoor = transform.Find("Door").gameObject;
        textPrompt = transform.Find("TextPrompt").gameObject.GetComponent<DoorText>();
        player = GameObject.FindWithTag("Player").gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // distance from the player to the knob
        float dis = Vector3.Distance(player.position, transform.position);
        bool closeToWindow = dis <= interactDis;

        // Getting in wich side of the door the player is
        Vector3 toPlayer = transform.position - player.position;

        float windowSide = Vector3.Dot(Vector3.Cross(toPlayer, Vector3.forward).normalized, Vector3.up);

        textPrompt.Show(closeToWindow, windowSide > 0); 

        if (Input.GetKeyDown(KeyCode.E))
        {
            windowDoor.SetActive(!closed);
            Interact(!closed);
        }
    }

    // Opens and closes the window
    void Interact(bool closed_)
    {
        closed = closed_;
        //anim.SetBool("Open", open);
        textPrompt.ChangePrompt(!closed);

        /*
        if (open)
        {
            anim.Play("DoorOpen");
        }
        */
    }
}
