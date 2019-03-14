using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveScript : MonoBehaviour
{
 
    public Vector3 speed = new Vector3(3, 3,1);


    public Vector3 direction = new Vector3(-1, 0,0);
    private Vector3 movement;


    public Rigidbody rb;
    void FixedUpdate()
    {
        // Application du mouvement
        rb = GetComponent<Rigidbody>();
        rb.velocity = movement;
    }

    void Update()
    {
        while (true)
        {

            movement = new Vector2(
            speed.x * direction.x,
            speed.y * direction.y);
            new WaitForSecondsRealtime(2);

            direction = new Vector2(1, 0);
            new WaitForSecondsRealtime(2);
        }


    }



}