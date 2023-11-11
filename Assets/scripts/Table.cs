using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour, I_Interactable
{
    [SerializeField] float interactDis = 2.7f;

    private Transform table;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        table = transform;
        player = GameObject.FindWithTag("Player").gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Applies a force to the object
    public void Kick(Vector3 dir, float strength = 1f)
    {
        float mag = 20f * strength;
        this.gameObject.GetComponent<Rigidbody>().AddForce(dir * mag);
    }

    public bool hasTag(string tag)
    {
        List<string> tags = new List<string>{"", "Table"};
        return tags.Contains(tag);
    }
}
