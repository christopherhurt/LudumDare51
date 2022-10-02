using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthBarManager : MonoBehaviour
{

    public static double milesTravelled = 0.0;

    public double milesPerMinute;
    public RectTransform bar;
    public float xOffset;

    private double health;
    private float fullBarWidth;

    void Start()
    {
        health = 1.0;
        milesTravelled = 0.0;
        fullBarWidth = bar.rect.width;
    }

    void Update()
    {
        milesTravelled += (milesPerMinute / 60.0) * Time.deltaTime;

        // TODO: remove this and apply damage from player script
        inflictDamage(0.05f * Time.deltaTime);
    }

    public void inflictDamage(double amount)
    {
        health -= amount;

        if (health <= 0.0)
        {
            SceneManager.LoadScene("GameOver");
        }

        // Update health bar visual
        int newWidth = (int)(health * fullBarWidth);
        bar.sizeDelta = new Vector2(newWidth, bar.rect.height);
        bar.position = new Vector3(xOffset - (fullBarWidth - newWidth) / 2, bar.position.y, bar.position.z);
    }

}
