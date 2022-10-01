using Unity.VisualScripting;
using UnityEngine;

public class Mover : MonoBehaviour
{

    public UnityEngine.Object initialBackground;
    public float initialSizeFactor;
    public float initRatePerSecond;

    private float ar;

    void Start()
    {
        ar = GetComponent<Camera>().aspect;
        updateCamViewSize();

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0.0f, initRatePerSecond);
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
        // TODO: implement
    }

    private void updateCamViewSize()
    {
        // Lock cam size to match background dimensions and aspect ratio
        Camera cam = GetComponent<Camera>();
        cam.orthographicSize = initialBackground.GetComponent<Transform>().transform.localScale.x * initialSizeFactor / cam.aspect;
    }

}
