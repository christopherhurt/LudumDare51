using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{

    private readonly float DELETION_Y_OFFSET = 30.0f;

    public Camera cam;
    public float initSpeed;

    public bool isOriginal = true;
    
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0.0f, initSpeed);
    }

    void Update()
    {
        // TODO: destroy when too far above screen as well?
        // Destroy when off bottom of screen
        if (!isOriginal && transform.position.y < cam.transform.position.y - DELETION_Y_OFFSET)
        {
            Destroy(this.gameObject);
        }
    }
}
