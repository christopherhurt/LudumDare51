using TMPro;
using UnityEngine;

public class InitScoreText : MonoBehaviour
{

    public TextMeshProUGUI scoreText;

    void Start()
    {
        string formattedMiles = HealthBarManager.milesTravelled.ToString("F2");
        scoreText.SetText("Miles Travelled: " + formattedMiles);
    }

}
