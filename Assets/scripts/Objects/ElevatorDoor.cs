using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoor : Door
{
    override public void Start()
    {
        base.Start();
        openAnimationName = "ElevatorOpen";
    }

    override public void Kick(Vector3 dir, float strength = 1f)
    {
        return;
    }

}
