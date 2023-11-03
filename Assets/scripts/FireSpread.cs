using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpread : MonoBehaviour
{
    public GameObject FireCenter;
    private Transform IMD_area;
    public float isolationDisBuffer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        IMD_area = GameObject.FindWithTag("IMD_area").gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            TriangleSpread();
        }
    }

    // Creates a new fire point in 'pos'
    void createNewFireCenter(Vector3 pos)
    {
        if (isolated(pos) && insideIMD(pos))
        {
            Instantiate(FireCenter, pos, Quaternion.identity);
        }
    }

    // Created 3 new fire points in the shape of a triangle with the center as this object
    public void TriangleSpread(float radius = 12f, float rot = 0f)
    {
        for (int i = 0;i < 3;i ++)
        {
            float ang = Mathf.Deg2Rad *(rot + 120*i);
            Vector3 pos = transform.position + radius * (new Vector3(Mathf.Cos(ang), 0, Mathf.Sin(ang)));
            createNewFireCenter(pos);
        }
    }

    // Returns whether the fire point is far enough from others (aka isolated)
    private bool isolated(Vector3 pos)
    {
        GameObject[] lista = GameObject.FindGameObjectsWithTag("FirePoint");

        foreach(GameObject g in lista)
        {
            float dis = Vector3.Distance(g.transform.position, pos);
            if (dis < g.GetComponent<ParticleSpread>().GetRadius() + isolationDisBuffer)
            {
                return false;
            }
        }

        return true;
    }

    private bool insideIMD(Vector3 pos)
    {
        Vector3 topLeft = IMD_area.GetChild(0).transform.position;
        Vector3 bottomRight = IMD_area.GetChild(1).transform.position;

        return topLeft.x < pos.x && topLeft.z < pos.z && bottomRight.x > pos.x && bottomRight.z > pos.z;
    }
}
