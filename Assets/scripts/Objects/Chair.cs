using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour, I_Interactable
{
    [SerializeField] float interactDis = 2.7f;
    [SerializeField] string promptTextSit = "Sentar (F)";
    [SerializeField] string promptTextGetUp = "Levantar (F)";
    public bool IsSittable = true;
    [HideInInspector] public bool hasPlayer = false;

    private Transform chair;
    private Transform player;
    private Player playerScript;
    private ActionsPrompt actionsPrompt;

    // Start is called before the first frame update
    void Start()
    {
        chair = transform;
        actionsPrompt = GameObject.FindWithTag("ActionsPrompt").GetComponent<ActionsPrompt>();

        var player_ = GameObject.FindWithTag("Player");
        player = player_.transform;
        playerScript = player_.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        // Burned
        bool burned = GetComponent<Flammable>().burned;

        // 
        if (!burned)
        {
            // distance from the player to the chair
            float dis = Vector3.Distance(player.position, chair.position);

            // IsSittable
            float minSitAng = 45f;
            float xAng = chair.eulerAngles.x;
            float zAng = chair.eulerAngles.z;
            IsSittable = xAng > -minSitAng && xAng < minSitAng &&
                        zAng > -minSitAng && zAng < minSitAng;

            if (IsSittable && dis <= interactDis && (hasPlayer || !playerScript.isSatDown()))
            {
                actionsPrompt.Show(playerScript.isSatDown() ? promptTextGetUp : promptTextSit);
            }
        }
        else {
            IsSittable = false;
        }
    }

    // Applies a force to the object
    public void Kick(Vector3 dir, float strength = 1f)
    {
        float mag = 20f * strength;
        this.gameObject.GetComponent<Rigidbody>().AddForce(dir * mag);
    }

    public bool hasTag(string tag)
    {
        List<string> tags = new List<string>{"", "Chair"};
        return tags.Contains(tag);
    }
}
