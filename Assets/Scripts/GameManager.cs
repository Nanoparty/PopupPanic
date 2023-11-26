using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum WindowType { Email, Form, Victory, Defeat, Menu, Popup };

    public WindowType ActiveWindow;

    public float MinDelay;
    public float MaxDelay;
    public int MinCount;
    public int MaxCount;

    public Button MailIcon;
    public Button InternetIcon;
    public Button MenuIcon;

    public Button StartButton;
    public Button ClaimPrize;
    public Button ResumeButton;
    public Button RestartMenuButton;
    public Button QuitButton;
    public Button LoseRestart;
    public Button LoseAdRestart;
    public Button WinRestart;

    public GameObject MailNotificationIcon;
    public GameObject BlockingWindow;

    public GameObject StartWindow;
    public GameObject PrizeWindow;
    public GameObject EmailWindow;
    public GameObject TimerWindow;
    public GameObject VictoryWindow;
    public GameObject DefeatTimeoutWindow;
    public GameObject DefeatAdWindow;
    public GameObject MenuWindow;

    public GameObject PopupContainer;

    public List<GameObject> PopupAds;
    public List<GameObject> ActiveAds;

    public int NumActiveAds;

    public TMP_Text CountdownTimer;

    [Header("Form Fields")]
    public TMP_InputField FirstName;
    public TMP_InputField LastName;
    public TMP_InputField MiddleInitial;
    public TMP_InputField Gender;
    public TMP_InputField Age;
    public TMP_InputField FavoriteColor;
    public TMP_Dropdown StarWars;
    public TMP_InputField Planet;
    public TMP_InputField System;
    public TMP_InputField Galaxy;
    public TMP_InputField Addition;
    public TMP_InputField Subtraction;
    public TMP_InputField Power;
    public TMP_InputField Universe;
    public Toggle Soul;
    public TMP_Text ErrorMessage;
    public Button SubmitForm;

    private bool GameStart;

    public int TotalTime = 120;
    private float Clock;
    public bool TimerRunning;

    private bool MenuOpen;

    private Coroutine ActiveCoroutine;

    private void Start()
    {
        StartButton.onClick.AddListener(() => {
            GameStart = true;
            StartWindow.SetActive(false);
            MailNotificationIcon.SetActive(true);
        });

        MailIcon.onClick.AddListener(() => {
            if (!GameStart) return;

            EmailWindow.SetActive(true);
            MailNotificationIcon.SetActive(false);
        });

        ClaimPrize.onClick.AddListener(() => {
            PrizeWindow.SetActive(true);
            TimerWindow.SetActive(true);
            TimerRunning = true;

            ActiveCoroutine = StartCoroutine(SpawnAds());

        });

        SubmitForm.onClick.AddListener(() => {
            CheckForm();
        });

        MenuIcon.onClick.AddListener(() => {
            if (MenuOpen)
            {
                
                MenuOpen = false;
                MenuWindow.SetActive(false);
                TimerRunning = true;
            }
            else
            {
                
                MenuOpen = true;
                MenuWindow.SetActive(true);
                TimerRunning = false;
            }
        });

        ResumeButton.onClick.AddListener(() => {
            MenuWindow.SetActive(false);
            MenuOpen = false;
            if (PrizeWindow.activeSelf && (TotalTime - (int)Clock) > 0){
                TimerRunning = true;
            }
        });

        RestartMenuButton.onClick.AddListener(() => {
            SceneManager.LoadScene("Desktop", LoadSceneMode.Single);
        });

        LoseRestart.onClick.AddListener(() => {
            SceneManager.LoadScene("Desktop", LoadSceneMode.Single);
        });

        LoseAdRestart.onClick.AddListener(() => {
            SceneManager.LoadScene("Desktop", LoadSceneMode.Single);
        });

        WinRestart.onClick.AddListener(() => {
            SceneManager.LoadScene("Desktop", LoadSceneMode.Single);
        });

        QuitButton.onClick.AddListener(() => {
            Application.Quit();
        });

    }

    private void Update()
    {
        if (TimerRunning)
        {
            Clock += Time.deltaTime;
        }
        if (TimerWindow.activeSelf)
        {
            CountdownTimer.text = (TotalTime - (int)Clock).ToString();
        }

        if ((TotalTime - (int)Clock) <= 0)
        {
            TimerRunning = false;
            CountdownTimer.text = "0";
            DefeatTimeoutWindow.SetActive(true);
            ActiveWindow = WindowType.Defeat;
        }

        if (NumActiveAds > 0)
        {
            BlockingWindow.SetActive(true);
        }
        else
        {
            BlockingWindow.SetActive(false);
        }

    }

    IEnumerator SpawnAds()
    {
        yield return new WaitForSeconds(1f);

        yield return new WaitUntil(() => TimerRunning);

        int numAds = Random.Range(MinCount, MaxCount);
        float delay = Random.Range(MinDelay, MaxDelay);

        EventSystem.current.SetSelectedGameObject(null);

        for (int i = 0; i < numAds; i++)
        {
            Vector2 pos = new Vector2(Random.Range(-400, 400), Random.Range(-90, 140));
            GameObject ad = Instantiate(PopupAds[Random.Range(0, PopupAds.Count)], Vector2.zero, Quaternion.identity);
            ad.transform.parent = PopupContainer.transform;
            ad.transform.localPosition = pos;
            ActiveAds.Add(ad);
            NumActiveAds += 1;
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(delay);

        yield return new WaitUntil(() => TimerRunning);

        ActiveCoroutine = StartCoroutine(SpawnAds());
    }

    private void CheckForm()
    {
        bool complete = true;
        string error_message;

        if (FirstName.text == "")
        {
            complete = false;
            error_message = "ERROR: First Name is Missing.";
            ErrorMessage.gameObject.SetActive(true);
            ErrorMessage.text = error_message;
            return;
        }

        if (LastName.text == "")
        {
            complete = false;
            error_message = "ERROR: Last Name is Missing.";
            ErrorMessage.gameObject.SetActive(true);
            ErrorMessage.text = error_message;
            return;
        }

        if (MiddleInitial.text == "")
        {
            complete = false;
            error_message = "ERROR: Middle Initial is Missing.";
            ErrorMessage.gameObject.SetActive(true);
            ErrorMessage.text = error_message;
            return;
        }

        if (Gender.text == "")
        {
            complete = false;
            error_message = "ERROR: Gender is Missing.";
            ErrorMessage.gameObject.SetActive(true);
            ErrorMessage.text = error_message;
            return;
        }

        if (Age.text == "")
        {
            complete = false;
            error_message = "ERROR: Age is Missing.";
            ErrorMessage.gameObject.SetActive(true);
            ErrorMessage.text = error_message;
            return;
        }

        if (FavoriteColor.text == "")
        {
            complete = false;
            error_message = "ERROR: Favorite Color is Missing.";
            ErrorMessage.gameObject.SetActive(true);
            ErrorMessage.text = error_message;
            return;
        }

        if (StarWars.value == 0)
        {
            complete = false;
            error_message = "ERROR: Star Wars Answer is Missing.";
            ErrorMessage.gameObject.SetActive(true);
            ErrorMessage.text = error_message;
            return;
        }

        if (StarWars.value == 2)
        {
            complete = false;
            error_message = "ERROR: Star Wars Answer is Incorrect.";
            ErrorMessage.gameObject.SetActive(true);
            ErrorMessage.text = error_message;
            return;
        }

        if (Planet.text == "")
        {
            complete = false;
            error_message = "ERROR: Planet is Missing.";
            ErrorMessage.gameObject.SetActive(true);
            ErrorMessage.text = error_message;
            return;
        }

        if (System.text == "")
        {
            complete = false;
            error_message = "ERROR: System is Missing.";
            ErrorMessage.gameObject.SetActive(true);
            ErrorMessage.text = error_message;
            return;
        }

        if (Galaxy.text == "")
        {
            complete = false;
            error_message = "ERROR: Galaxy is Missing.";
            ErrorMessage.gameObject.SetActive(true);
            ErrorMessage.text = error_message;
            return;
        }

        if (Addition.text == "")
        {
            complete = false;
            error_message = "ERROR: Addition Answer is Missing.";
            ErrorMessage.gameObject.SetActive(true);
            ErrorMessage.text = error_message;
            return;
        }

        if (Addition.text.Trim() != "4")
        {
            complete = false;
            error_message = "ERROR: Addition Answer is Incorrect.";
            ErrorMessage.gameObject.SetActive(true);
            ErrorMessage.text = error_message;
            return;
        }

        if (Subtraction.text == "")
        {
            complete = false;
            error_message = "ERROR: Subtraction Answer is Missing.";
            ErrorMessage.gameObject.SetActive(true);
            ErrorMessage.text = error_message;
            return;
        }

        if (Subtraction.text.Trim() != "3")
        {
            complete = false;
            error_message = "ERROR: Subtraction Answer is Incorrect.";
            ErrorMessage.gameObject.SetActive(true);
            ErrorMessage.text = error_message;
            return;
        }

        if (Power.text == "")
        {
            complete = false;
            error_message = "ERROR: Power Answer is Missing.";
            ErrorMessage.gameObject.SetActive(true);
            ErrorMessage.text = error_message;
            return;
        }

        if (Power.text.Trim() != "8")
        {
            complete = false;
            error_message = "ERROR: Power Answer is Incorrect.";
            ErrorMessage.gameObject.SetActive(true);
            ErrorMessage.text = error_message;
            return;
        }

        if (Universe.text == "")
        {
            complete = false;
            error_message = "ERROR: Answer to the Universe is Missing.";
            ErrorMessage.gameObject.SetActive(true);
            ErrorMessage.text = error_message;
            return;
        }

        if (Universe.text.Trim() != "42")
        {
            complete = false;
            error_message = "ERROR: Answer to the Universe is Incorrect.";
            ErrorMessage.gameObject.SetActive(true);
            ErrorMessage.text = error_message;
            return;
        }

        if (!Soul.isOn)
        {
            complete = false;
            error_message = "ERROR: Terms of Service Has Not Been Ageed To.";
            ErrorMessage.gameObject.SetActive(true);
            ErrorMessage.text = error_message;
            return;
        }

        ErrorMessage.gameObject.SetActive(false);
        TimerRunning = false;
        VictoryWindow.SetActive(true);
    }
}
