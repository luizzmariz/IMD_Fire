using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpread : MonoBehaviour
{
    public GameObject FireCenter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            TriangleSpread();
        }
    }

    void createNewFireCenter(Vector3 pos)
    {
        Instantiate(FireCenter, pos, Quaternion.identity);
    }

    public void TriangleSpread(float radius = 12f, float rot = 0f)
    {
        for (int i = 0;i < 3;i ++)
        {
            float ang = Mathf.Deg2Rad *(rot + 120*i);
            Vector3 pos = transform.position + radius * (new Vector3(Mathf.Cos(ang), 0, Mathf.Sin(ang)));
            createNewFireCenter(pos);
        }
    }
}
