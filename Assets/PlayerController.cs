using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Camera cam;
    public float camXOffset;
    public float camYOffset;
    public float acceleration;
    public float angularAccelerationDegrees;
    public float frictionFactor;
    public float maxAngularOffsetDegrees;

    private float baseRate;
    private Rigidbody2D rb;

    void Start()
    {
        baseRate = cam.GetComponent<Mover>().initRatePerSecond;

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float accX = 0.0f;
        float accY = 0.0f;
        float angAcc = 0.0f;

        // TODO: apply rotation on key input as well
        // Process player inputs
        if (Input.GetKey(KeyCode.W))
        {
            accY += acceleration;
        }
        if (Input.GetKey(KeyCode.S))
        {
            accY -= acceleration;
        }
        if (Input.GetKey(KeyCode.A))
        {
            accX -= acceleration;
            angAcc += angularAccelerationDegrees; // Rotate left
        }
        if (Input.GetKey(KeyCode.D))
        {
            accX += acceleration;
            angAcc -= angularAccelerationDegrees; // Rotate right
        }

        // TODO: fix this block
        // TODO: apply rotational friction
        // Apply friction if no player controls are active
        //if (accX == 0.0f && velX != 0.0f)
        //{
        //    accX = frictionFactor / -velX;
        //}
        //if (accY == 0.0f && velY != baseRate)
        //{
        //    accY = frictionFactor / -(velY - baseRate);
        //}

        // Apply player inputs to vehicle velocity
        rb.velocity += new Vector2(accX * Time.deltaTime, accY * Time.deltaTime);
    }

    void LateUpdate()
    {
        // Keep the player in cam bounds!
        if (transform.position.y > cam.transform.position.y + camYOffset)
        {
            transform.position = new Vector3(transform.position.x, cam.transform.position.y + camYOffset, transform.position.z);
            rb.velocity = new Vector2(rb.velocity.x, baseRate);
        } else if (transform.position.y < cam.transform.position.y - camYOffset)
        {
            transform.position = new Vector3(transform.position.x, cam.transform.position.y - camYOffset, transform.position.z);
            rb.velocity = new Vector2(rb.velocity.x, baseRate);
        }

        // Clamp the player car's rotation
        // TODO
    }
}
