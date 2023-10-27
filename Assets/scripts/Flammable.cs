using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flammable : MonoBehaviour
{
    public bool burned = false;
    public float load_burn = 5f;
    
    private bool burning = false;
    private float r_burn = 0f;

    [SerializeField] private GameObject FireObj;
    private GameObject flame;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!burned && !burning)
        {
            // Checking if the object is in range to be burned;

            GameObject[] lista = GameObject.FindGameObjectsWithTag("FirePoint");

            foreach(GameObject g in lista)
            {
                float distanceToFire = Vector3.Distance(g.transform.position, transform.position);
                float fireRadius = g.GetComponent<Fire>().GetRadius();

                if (distanceToFire <= fireRadius*0.5f)
                {
                    burning = true;
                    CreateFlame();
                    break;
                }
            }
        }

        if (burning)
        {
            if (r_burn >= load_burn) //wating a little bit until its completly burned
            {
                Burn();
                burning = false;
                r_burn = 0;
            }

            r_burn += Time.deltaTime;
        }
    }

    void CreateFlame()
    {
        flame = Instantiate(FireObj, transform);
    }

    void Burn()
    {
        burned = true;

        // Putting out the flame
        Destroy(flame);

        // Changing color to black
        Color burnedColor = new Color(0, 0, 0);

        GetComponent<Renderer>().material.color = burnedColor;

        for (int i = 0;i < transform.childCount;i ++)
        {
            transform.GetChild(i).GetComponent<Renderer>().material.color = burnedColor;
        }
    }
}
