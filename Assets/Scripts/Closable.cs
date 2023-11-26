using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Closable : MonoBehaviour
{
    public Button CloseButton;

    public Button ScamButton;

    public GameManager gm;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        CloseButton.onClick.AddListener(() => {
            SoundManager.sm.PlayWindowCloseSound();
            Destroy(gameObject); 
            gm.NumActiveAds -= 1; 
        });

        ScamButton.onClick.AddListener(() =>
        {
            gm.gameover = true;
            SoundManager.sm.PlayFailureSound();
            gm.TimerRunning = false;
            gm.DefeatAdWindow.SetActive(true);
        });

        
    }
}
