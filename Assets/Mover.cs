using Unity.VisualScripting;
using UnityEngine;

public class Mover : MonoBehaviour
{

    public Object initialBackground;
    public float initialSizeFactor;
    public float initRatePerSecond;
    public float speedUpAmount;

    public float ratePerSecond; // Value provided in UI not used

    private float ar;

    void Start()
    {
        ratePerSecond = initRatePerSecond;

        ar = GetComponent<Camera>().aspect;
        updateCamViewSize();

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
    }

    public void speedup()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        ratePerSecond = rb.velocity.y + speedUpAmount;
        rb.velocity = new Vector2(rb.velocity.x, ratePerSecond);

        // TODO: play sound
    }

    private void updateCamViewSize()
    {
        // Lock cam size to match background dimensions and aspect ratio
        Camera cam = GetComponent<Camera>();
        cam.orthographicSize = initialBackground.GetComponent<Transform>().transform.localScale.x * initialSizeFactor / cam.aspect;
    }

}
