using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    [SerializeField] public GameObject fire;
    public string spawn = "spawn_Cantina";
    public float startDelay = 1f;
    
    private SpawnController spawnControl;
    private float r = 0;
    private bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnControl = GameObject.FindWithTag("SpawnController").GetComponent<SpawnController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
        {
            if (r < startDelay)
            {
                r += Time.deltaTime;
            }
            else {
                started = true;
                starFire();
            }
        }
    }
    
    public void starFire()
    {
        Instantiate(fire, spawnControl.Spawn(spawn), Quaternion.identity);
    }
}
