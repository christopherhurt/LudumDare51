using TMPro;
using UnityEngine;

public class Every10Sec : MonoBehaviour
{

    public float timePerSpeedUpSec;
    public float textTimeBeforeSpeedUpSec;
    public float timeToShowTextSec;
    public Mover cam;
    public GameObject canvas;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI phoneText;
    public TextAsset json;
    public AudioSource textSound;

    private float counterSec;
    private bool textShown;
    private bool textIsShowing;
    private float textCounter;
    private MyText[] texts;
    private int currTextIndex;

    void Start()
    {
        counterSec = 0.0f;
        textShown = false;
        textIsShowing = false;
        textCounter = timeToShowTextSec;

        setMessageVisible(false);

        MyTexts theTexts = JsonUtility.FromJson<MyTexts>(json.text);
        texts = theTexts.texts;
        Shuffle(texts);
        currTextIndex = 0;
    }

    void Update()
    {
        // Update counters
        counterSec += Time.deltaTime;
        if (textIsShowing)
        {
            textCounter -= Time.deltaTime;
        }

        // Show text
        if (!textShown && counterSec >= timePerSpeedUpSec - textTimeBeforeSpeedUpSec)
        {
            updateText();
            setMessageVisible(true);

            textSound.Play();

            textShown = true;
            textIsShowing = true;
        }

        // Speed up camera/car
        if (counterSec >= timePerSpeedUpSec)
        {
            cam.speedup();

            counterSec = 0.0f;
            textShown = false;
        }
        
        // Hide the text after expiration
        if (textIsShowing && textCounter <= 0.0f)
        {
            setMessageVisible(false);

            textIsShowing = false;
            textCounter = timeToShowTextSec;
        }
    }

    private void setMessageVisible(bool visible)
    {
        canvas.SetActive(visible);
    }

    private void updateText()
    {
        // Re-shuffle once we've gone through all the texts
        if (currTextIndex >= texts.Length)
        {
            Shuffle(texts);
            currTextIndex = 0;
        }

        nameText.SetText(texts[currTextIndex].name);
        phoneText.SetText(texts[currTextIndex].text);
        currTextIndex++;
    }

    // https://stackoverflow.com/questions/36702548/shuffle-array-in-unity
    private void Shuffle(MyText[] arr)
    {
        int p = arr.Length;
        for (int n = p - 1; n > 0; n--)
        {
            int r = Random.Range(0, n);
            MyText t = arr[r];
            arr[r] = arr[n];
            arr[n] = t;
        }
    }

    [System.Serializable]
    public class MyTexts
    {
        public MyText[] texts;
    }

    [System.Serializable]
    public class MyText
    {
        public string name;
        public string text;
    }

}
