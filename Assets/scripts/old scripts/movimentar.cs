using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimentar : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 12f;
    private float gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float velocity;

    // Update is called once per frame
    void Update()
    {
        ApplyGravity();
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }

    private void ApplyGravity(){
        velocity += gravity * gravityMultiplier * Time.deltaTime;
        //direction.y = velocity;
    }
}
