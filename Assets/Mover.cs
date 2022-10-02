using Unity.VisualScripting;
using UnityEngine;

public class Mover : MonoBehaviour
{

    public UnityEngine.Object initialBackground;
    public float initialSizeFactor;
    public float initRatePerSecond;
    public float speedUpAcceleration;
    public float speedUpDurationSec;

    public float ratePerSecond; // Value provided in UI not used

    private float ar;
    private bool isSpeedingUp;
    private float speedUpTime;

    void Start()
    {
        ratePerSecond = initRatePerSecond;

        ar = GetComponent<Camera>().aspect;
        updateCamViewSize();

        isSpeedingUp = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0.0f, ratePerSecond);
    }

    void Update()
    {
        float currAr = GetComponent<Camera>().aspect;
        if (ar != currAr)
        {
            ar = currAr;
            updateCamViewSize();
        }

        // Apply speedup
        if (isSpeedingUp)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            ratePerSecond = rb.velocity.y + speedUpAcceleration * Time.deltaTime;
            rb.velocity = new Vector2(rb.velocity.x, ratePerSecond);
            speedUpTime -= Time.deltaTime;
        }
        if (speedUpTime <= 0.0f)
        {
            isSpeedingUp = false;
        }
    }

    public void speedup()
    {
        isSpeedingUp = true;
        speedUpTime = speedUpDurationSec;

        // TODO: play sound
    }

    private void updateCamViewSize()
    {
        // Lock cam size to match background dimensions and aspect ratio
        Camera cam = GetComponent<Camera>();
        cam.orthographicSize = initialBackground.GetComponent<Transform>().transform.localScale.x * initialSizeFactor / cam.aspect;
    }

}
