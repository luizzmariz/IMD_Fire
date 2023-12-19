using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, I_Interactable
{
    [SerializeField] float doorSpd = 0.5f;
    [SerializeField] float interactDis = 2.7f;
    public bool open = false;
    public bool broken = false;
    public bool stuck = false;

    public AudioClip doorOpen_audio;
    public AudioClip doorClose_audio;

    private Transform knob;
    private Transform player;
    private Animator anim;
    private DoorText textPrompt;
    private Rigidbody rb;
    private AudioSource audioSource;
    private ActionsPrompt actionsPrompt;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        anim = GetComponent<Animator>();
        anim.speed = doorSpd;

        knob = transform.Find("Knob").gameObject.transform;
        player = GameObject.FindWithTag("Player").gameObject.transform;
        textPrompt = transform.Find("TextPrompt").gameObject.GetComponent<DoorText>();
        actionsPrompt = GameObject.FindWithTag("ActionsPrompt").gameObject.GetComponent<ActionsPrompt>();
    }

    // Update is called once per frame
    void Update()
    {
        // Interactions
        if (!broken)
        {
            // distance from the player to the knob
            float dis = Vector3.Distance(player.position, knob.position);

            // Getting in wich side of the door the player is
            Vector3 toPlayer = transform.position - player.position;
            Vector3 toKnob = transform.position - knob.position;

            float doorSide = Vector3.Dot(Vector3.Cross(toPlayer, toKnob).normalized, Vector3.up);

            bool closeToKnob = selfCollisionLine(player.position, 
            player.Find("Camera").forward, 
            interactDis);

            //if (closeToKnob) Debug.Log(player.position +  Quaternion.Euler(-30 * Mathf.Sign(doorSide), 0, 0) * player.Find("Camera").forward);

            textPrompt.Show(closeToKnob, doorSide < 0);
            
            // Only interacting with the door when close to the knob
            if (closeToKnob)
            {
                // opening door
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (!stuck) Interact(!open);
                    else actionsPrompt.Show("A porta está emperrada!");
                }

                /*
                // Checking handle temperature
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    actionsPrompt.Show("A porta está quente!");
                }
                */
            }
        }
    }

    // Opens and closes the door
    void Interact(bool opened)
    {
        open = opened;
        anim.SetBool("Open", open);
        textPrompt.ChangePrompt(open);

        if (open)
        {
            audioSource.PlayOneShot(doorOpen_audio);
            anim.Play("DoorOpen");
        }
        else {
            audioSource.PlayOneShot(doorClose_audio);
        }
    }

    // Applies a force to the object
    public void Kick(Vector3 dir, float strength = 1f)
    {
        // Making the door fall on the ground as a physics object
        if (!broken)
        {
            broken = true;
            anim.enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<Rigidbody>().isKinematic = false;
            textPrompt.HidePrompt();
            this.gameObject.layer = 2;
        }

        // Getting the side of the player in relation to the door
        Vector3 toKnob = transform.position - knob.position;
        float doorSide = Vector3.Dot(Vector3.Cross(dir, toKnob).normalized, Vector3.up);

        // Applying the force
        float mag = 200f * strength * Mathf.Sign(doorSide);
        rb.AddForce(Quaternion.Euler(0, transform.eulerAngles.y, 0) * Vector3.forward * mag);
    }

    public bool hasTag(string tag)
    {
        List<string> tags = new List<string>{"", "Door"};
        return tags.Contains(tag);
    }

    // Collision line with yourself from 'pos', at 'dir' direction and 'dis' distance
    private bool selfCollisionLine(Vector3 pos, Vector3 dir, float dis) {

        RaycastHit ray;

        if (Physics.Raycast(pos, dir, out ray, dis, 1<<6)) {
            return ray.collider.gameObject == gameObject;
        }

        return false;
    }
}
