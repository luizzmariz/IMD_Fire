using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 NormalSpawn()
    {
        return transform.GetChild(0).transform.position;
    }

    public Vector3 Spawn(int i)
    {
        return transform.GetChild(i).transform.position;
    }

    public Vector3 Spawn(string str)
    {
        return transform.Find(str).transform.position;
    }

    public Vector3 RandomSpawn()
    {
        int rnd = Random.Range(0, transform.childCount);
        return transform.GetChild(rnd).transform.position;
    }
}
