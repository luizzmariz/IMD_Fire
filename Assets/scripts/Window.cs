using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    public bool closed = true;
    [SerializeField] float interactDis = 2.7f;

    private GameObject windowDoor;
    private GameObject playerObj;
    private Transform player;
    private DoorText textPrompt;

    // Start is called before the first frame update
    void Start()
    {
        windowDoor = transform.Find("Door").gameObject;
        textPrompt = transform.Find("TextPrompt").gameObject.GetComponent<DoorText>();
        playerObj = GameObject.FindWithTag("Player").gameObject;
        player = playerObj.transform;
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

        if (closeToWindow && Input.GetKeyDown(KeyCode.E))
        {
            Interact(!closed);
        }
    }

    // Opens and closes the window
    void Interact(bool closed_)
    {
        closed = closed_;
        windowDoor.SetActive(closed_);
        textPrompt.ChangePrompt(!closed_);

        // Backdraft
        if (!closed)
        {
            if (closeToAFire())
            {
                // Game Over
                // pls add an explosion so it looks cool

                playerObj.GetComponent<Player>().GameOver("Não abra janelas em um incêndio !!");
            }
        }
    }

    private bool closeToAFire()
    {
        GameObject[] lista = GameObject.FindGameObjectsWithTag("FirePoint");

        foreach(GameObject g in lista)
        {
            float distanceToFire = Vector3.Distance(g.transform.position, transform.position);
            float fireRadius = g.GetComponent<ParticleSpread>().GetRadius();

            if (distanceToFire <= fireRadius*0.75f)
            {
                return true;
            }
        }

        return false;
    }
}
