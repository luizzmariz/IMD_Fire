using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorGameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other) {

        GameObject g = other.gameObject;

        if (g.tag == "Player" && (CheckForDanger("FirePoint") || CheckForDanger("Smoke")))
        {
            Player player = g.GetComponent<Player>();
            player.GameOver(player.elevator_death);
        }

    }

    bool CheckForDanger(string danger, float dis_modifier = 1f)
    {
        GameObject[] lista = GameObject.FindGameObjectsWithTag(danger);

        foreach(GameObject g in lista)
        {
            float distanceToDanger = Vector3.Distance(g.transform.position, transform.position);
            float dangerRadius = g.GetComponent<ParticleSpread>().GetRadius();

            if (distanceToDanger <= dangerRadius*g.transform.localScale.x*dis_modifier)
            {
                return true;
            }
        }

        return false;
    }
}
