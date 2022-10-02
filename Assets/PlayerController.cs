using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Camera cam;
    public float camYOffset;
    public float acceleration;
    public float angularAccelerationDegrees;
    public float frictionFactor;
    public float maxAngularOffsetDegrees;
    public HealthBarManager healthManager;

    private Rigidbody2D rb;

    void Start()
    {
        // Do not save this as a field so we can pick update to new base rate as it changes
        float baseRate = cam.GetComponent<Mover>().ratePerSecond;

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0.0f, baseRate);
    }

    void FixedUpdate()
    {
        float accX = 0.0f;
        float accY = 0.0f;
        float angAcc = 0.0f;

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

        // Apply friction if no player controls are active
        float baseRate = cam.GetComponent<Mover>().ratePerSecond;
        if (accX == 0.0f && Mathf.Abs(rb.velocity.x) > 0.000001f) { // Use offset so this adjustment doesn't continue running
            rb.velocity = new Vector2(rb.velocity.x / frictionFactor, rb.velocity.y);
        }
        if (accY == 0.0f && Mathf.Abs(rb.velocity.y - baseRate) > 0.000001f)
        {
            float tempVel = rb.velocity.y - baseRate;
            tempVel /= frictionFactor;
            rb.velocity = new Vector2(rb.velocity.x, tempVel + baseRate);
        }
        float adjRotZ = getAdjRotZ();
        if (angAcc == 0.0f && Mathf.Abs(adjRotZ) > 0.000001f)
        {
            rb.angularVelocity = 0.0f; // Gotta do this to avoid Unity screwing up my manual calculations
            rb.rotation = adjRotZ / frictionFactor;
        }

        // Apply player inputs to vehicle velocity and angular velocity
        rb.velocity += new Vector2(accX * Time.fixedDeltaTime, accY * Time.fixedDeltaTime);
        rb.angularVelocity += angAcc * Time.fixedDeltaTime;
    }

    void LateUpdate()
    {
        // Keep the player in cam bounds!
        float baseRate = cam.GetComponent<Mover>().ratePerSecond;
        if (transform.position.y > cam.transform.position.y + camYOffset / cam.aspect)
        {
            transform.position = new Vector3(transform.position.x, cam.transform.position.y + camYOffset / cam.aspect, transform.position.z);
            rb.velocity = new Vector2(rb.velocity.x, baseRate);
        } else if (transform.position.y < cam.transform.position.y - camYOffset / cam.aspect)
        {
            transform.position = new Vector3(transform.position.x, cam.transform.position.y - camYOffset / cam.aspect, transform.position.z);
            rb.velocity = new Vector2(rb.velocity.x, baseRate);
        }

        // Clamp the player car's rotation
        float adjRotZ = getAdjRotZ();
        if (adjRotZ > maxAngularOffsetDegrees + 0.01) // Offset to avoid getting stuck rotated due to rounding errors
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, maxAngularOffsetDegrees);
            rb.angularVelocity = 0.0f;
        }
        else if (adjRotZ < -(maxAngularOffsetDegrees + 0.01)) // Offset to avoid getting stuck rotated due to rounding errors
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, -maxAngularOffsetDegrees);
            rb.angularVelocity = 0.0f;
        }
    }

    private float getAdjRotZ()
    {
        float adjRotZ = transform.rotation.eulerAngles.z;
        if (adjRotZ > 180.0)
        {
            adjRotZ -= 360.0f;
        }
        return adjRotZ;
    }

}
