using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    class Button
    {
        public int hueOffsetIndex;
    }

    public Image backgroundImage;
    public Image webcamImage;
    public Text counterText;
    public Text victoireText;
    public Text defaiteText;

    public WebcamPhotographer webcamPhotographer;
    public GameObject webcamLive;
    public ArduinoCommunication arduinoCommunication;

    public int minCounter = 1;
    public int maxCounter = 6;
    public float challengeDuration = 60.0f;
    public float messageDuration = 5.0f;
    public float webcamDuration = 5.0f;
    public float gifDuration = 10.0f;
    public float gifFrameDuration = 0.5f;

    public float[] hueOffsets;
    private Button[] buttons = new Button[15];

    enum State
    {
        Challenge,
        Victoire,
        Defaite,
        Webcam,
        GIF,
    }

    State state;

    float mainHue;
    int buttonCount;
    int counter;
    int spaceCounter;
    float timer;

    List<Sprite> webcamSprites = new List<Sprite>();
    float gifTimer;
    int gifIndex;

    void Start()
    {
        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 5; ++j)
            {
                buttons[i * 5 + j] = new Button()
                {
                    hueOffsetIndex = i,
                };
            }
        }

        backgroundImage.material.SetFloat("_AspectRatio", Screen.width / Screen.height);

        ResetButtonCount();
        NewChallenge();
    }

    void SetAngle(float ratio)
    {
        backgroundImage.material.SetFloat("_Angle", Mathf.PI * (1 - 2 * ratio));
    }

    int GetButtonCount()
    {
        int sum = 0;
        for (int i = 0; i < 15; ++i)
        {
            if (buttons[i].hueOffsetIndex == 0 && arduinoCommunication.IsButtonPressed(i))
            {
                ++sum;
            }
        }
        return sum;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        switch (state)
        {
            case State.Challenge:
                SetAngle(timer / challengeDuration);
                if (timer <= 0)
                {
                    SetDefaite();
                }
                else
                {
                    if (Input.GetButtonDown("DecrementCounter"))
                    {
                        ++spaceCounter;
                    }

                    counter = buttonCount - spaceCounter - GetButtonCount();
                    UpdateCounterText();

                    if (counter <= 0)
                    {
                        SetVictoire();
                    }
                }
                break;

            case State.Victoire:
                if (timer <= 0)
                {
                    if (buttonCount >= maxCounter)
                        ShowGIF();
                    else
                        ShowWebcam();
                }
                break;

            case State.Defaite:
            case State.Webcam:
                if (timer <= 0)
                {
                    NewChallenge();
                }
                break;

            case State.GIF:
                if (timer <= 0)
                {
                    NewChallenge();
                }
                else
                {
                    gifTimer -= Time.deltaTime;
                    if (gifTimer <= 0)
                    {
                        gifTimer = gifFrameDuration;
                        gifIndex = (gifIndex + 1) % webcamSprites.Count;
                        webcamImage.sprite = webcamSprites[gifIndex];
                    }
                }
                break;
        }

        if (Input.GetButtonDown("NewChallenge"))
        {
            NewChallenge();
        }

        if (Input.GetButtonDown("ShowWebcam"))
        {
            webcamLive.SetActive(!webcamLive.activeSelf);
        }
    }

    void ResetButtonCount()
    {
        buttonCount = 0;
    }

    void NewChallenge()
    {
        state = State.Challenge;

        mainHue = Random.Range(0f, 1f);

        buttons.Shuffle();
        for (int i = 0; i < 15; ++i)
        {
            float hue = (mainHue + hueOffsets[buttons[i].hueOffsetIndex]) % 1f;
            var color = Color.HSVToRGB(hue, 1f, 1f);
            arduinoCommunication.SetButtonColor(i, color);
        }

        if (buttonCount <= 0)
        {
            buttonCount = minCounter;
            webcamSprites.Clear();
        }
        else
            ++buttonCount;
        counter = buttonCount;
        spaceCounter = 0;
        timer = challengeDuration;

        float firstHue = (mainHue + hueOffsets[0]) % 1f;
        var firstColor = Color.HSVToRGB(firstHue, 1f, 1f);
        backgroundImage.material.SetColor("_Color", firstColor);
        SetAngle(1.0f);
        webcamImage.enabled = false;
        counterText.enabled = true;
        victoireText.enabled = false;
        defaiteText.enabled = false;

        UpdateCounterText();
    }

    void UpdateCounterText()
    {
        counterText.text = counter.ToString();
    }

    void SetVictoire()
    {
        state = State.Victoire;
        timer = messageDuration;
        counterText.enabled = false;
        victoireText.enabled = true;

        Sprite webcam = webcamPhotographer.TakeSnapshot();
        webcamImage.sprite = webcam;
        if (webcam)
        {
            webcamSprites.Add(webcam);
        }
    }

    void SetDefaite()
    {
        ResetButtonCount();

        state = State.Defaite;
        timer = messageDuration;
        counterText.enabled = false;
        defaiteText.enabled = true;
    }

    void ShowWebcam()
    {
        if (webcamImage.sprite)
        {
            state = State.Webcam;
            timer = webcamDuration;
            webcamImage.enabled = true;
            counterText.enabled = false;
            victoireText.enabled = false;
            defaiteText.enabled = false;
        }
        else
            NewChallenge();
    }

    void ShowGIF()
    {
        ResetButtonCount();

        if (webcamSprites.Count > 0)
        {
            state = State.GIF;
            timer = gifDuration;
            gifTimer = gifFrameDuration;
            gifIndex = 0;
            webcamImage.enabled = true;
            webcamImage.sprite = webcamSprites[gifIndex];
            counterText.enabled = false;
            victoireText.enabled = false;
            defaiteText.enabled = false;
        }
        else
            NewChallenge();
    }
}
