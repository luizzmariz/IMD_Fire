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

    private float calc_freq = 3f;
    private float r_freq = 0f;

    // Start is called before the first frame update
    void Start()
    {
        r_freq = Random.Range(0f, calc_freq);
    }

    // Update is called once per frame
    void Update()
    {
        if (!burned && !burning)
        {

            if (r_freq >= calc_freq)
            {
                // Checking if the object is in range to be burned;

                GameObject[] lista = GameObject.FindGameObjectsWithTag("FirePoint");

                foreach(GameObject g in lista)
                {
                    float distanceToFire = Vector3.Distance(g.transform.position, transform.position);
                    float fireRadius = g.GetComponent<ParticleSpread>().GetRadius();

                    if (distanceToFire <= fireRadius*0.5f)
                    {
                        burning = true;
                        CreateFlame();
                        break;
                    }
                }
                
                r_freq = 0f;
            }

            r_freq += Time.deltaTime;
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

        // Changing the color of the whole object (and its children)
        void turnBurntColor(GameObject obj) {

            Color burnedColor = new Color(0, 0, 0);

            Renderer ren = obj.GetComponent<Renderer>();
            
            if (ren != null) {
                ren.material.color = burnedColor;
            }

            for (int i = 0;i < obj.transform.childCount;i ++)
            {
                turnBurntColor(obj.transform.GetChild(i).gameObject);
            }

        }

        turnBurntColor(this.gameObject);
    }
}
