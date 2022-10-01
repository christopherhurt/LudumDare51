using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private static readonly float MIN_VEL = 0.01f;

    public Camera cam;
    public float camXOffset;
    public float camYOffset;
    public float acceleration;
    public float frictionFactor;

    private float baseRate;
    private float velX;
    private float velY;

    void Start()
    {
        baseRate = cam.GetComponent<Mover>().initRatePerSecond;
        velX = 0.0f;
        velY = baseRate;
    }

    void Update()
    {
        float accX = 0.0f;
        float accY = 0.0f;

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
        }
        if (Input.GetKey(KeyCode.D))
        {
            accX += acceleration;
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

        // Apply player inputs to vehicle velocity/position
        velX += accX * Time.deltaTime;
        if (Mathf.Abs(velX) <= MIN_VEL)
        {
            velX = 0.0f;
        }
        velY += accY * Time.deltaTime;
        if (Mathf.Abs(Mathf.Abs(velY) - baseRate) <= MIN_VEL)
        {
            velY = baseRate;
        }

        if (velX != 0.0f)
        {
            transform.position = new Vector3(transform.position.x + velX * Time.deltaTime, transform.position.y, transform.position.z);
        }
        if (velY != 0.0f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + velY * Time.deltaTime, transform.position.z);
        }
    }

    void LateUpdate()
    {
        // Keep the player in cam bounds!
        if (transform.position.x > cam.transform.position.x + camXOffset)
        {
            transform.position = new Vector3(cam.transform.position.x + camXOffset, transform.position.y, transform.position.z);
            velX = 0.0f;
        } else if (transform.position.x < cam.transform.position.x - camXOffset)
        {
            transform.position = new Vector3(cam.transform.position.x - camXOffset, transform.position.y, transform.position.z);
            velX = 0.0f;
        }

        if (transform.position.y > cam.transform.position.y + camYOffset)
        {
            transform.position = new Vector3(transform.position.x, cam.transform.position.y + camYOffset, transform.position.z);
            velY = baseRate;
        } else if (transform.position.y < cam.transform.position.y - camYOffset)
        {
            transform.position = new Vector3(transform.position.x, cam.transform.position.y - camYOffset, transform.position.z);
            velY = baseRate;
        }
    }
}
