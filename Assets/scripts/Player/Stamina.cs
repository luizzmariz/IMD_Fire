using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    private float maxEnergy = 100f;
    private float initialxSc;
    private float initialxPos;

    void Start()
    {
        initialxSc = transform.localScale.x;
        initialxPos = transform.localPosition.x;
    }

    public void setMaxEnergy(float energy)
    {
        maxEnergy = energy;
    }

    public void updateEnergy(float energy)
    {
        if (energy >= 0)
        {
            float xSc = (energy/maxEnergy)*(initialxSc);
            transform.localScale = new Vector2 (xSc, transform.localScale.y);
            transform.localPosition = new Vector2 (
                initialxPos - (initialxSc/2)*(1 - energy/maxEnergy), transform.localPosition.y);
        }
    }
}
