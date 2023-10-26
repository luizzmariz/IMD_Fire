using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private Transform FireArea;
    private ParticleSystem.ShapeModule shape;

    // Start is called before the first frame update
    void Start()
    {
        FireArea = transform.Find("Area").transform;
        shape = GetComponent<ParticleSystem>().shape;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Spread();
        }
    }

    void Spread()
    {
        shape.radius = 12f;
        FireArea.localScale = new Vector3 (23f, FireArea.localScale.y, 23f);
    }

    void SpreadTo(GameObject victim)
    {
        /*
        if (victim.GetComponent<Fire>() == null)
        {
            victim.AddComponent<Fire>();
            Instantiate(FireObj, victim.transform);
        }
        */

        //CopyComponent(GetComponent<ParticleSystem>(), victim);
    }

    /*
    // by user: Shaffe   *adapted
    void CopyComponent(Component original, GameObject destination)
    {
        System.Type type = original.GetType();
        Component copy = destination.AddComponent(type);
        // Copied fields can be restricted with BindingFlags
        System.Reflection.FieldInfo[] fields = type.GetFields(); 
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
    }
    */
}
