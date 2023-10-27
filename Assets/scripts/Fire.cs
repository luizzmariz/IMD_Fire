using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float maxRadius = 12f;
    public float load_spread = 2f;

    private ParticleSystem.ShapeModule shape;

    private float radius = 1f;
    private float r_spread = 0f;
    private bool spreading = true;

    // Start is called before the first frame update
    void Start()
    {
        shape = GetComponent<ParticleSystem>().shape;
        radius = shape.radius;
    }

    // Update is called once per frame
    void Update()
    {
        if (spreading)
        {
            spreading = (radius < maxRadius);

            if (r_spread >= load_spread)
            {
                SetRadius(radius + 1f);
                r_spread = 0;
            }
            r_spread += Time.deltaTime;
        }
    }

    void SetRadius(float r)
    {
        shape.radius = r;
        radius = r;
    }

    public float GetRadius() {return radius;}

    /*
    void SpreadTo(GameObject victim)
    {
        if (victim.GetComponent<Fire>() == null)
        {
            victim.AddComponent<Fire>();
            Instantiate(FireObj, victim.transform);
        }

        //CopyComponent(GetComponent<ParticleSystem>(), victim);
    }
    */

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
