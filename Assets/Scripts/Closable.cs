using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Closable : MonoBehaviour
{
    public Button CloseButton;

    public GameManager gm;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        CloseButton.onClick.AddListener(() => { Destroy(gameObject); gm.NumActiveAds -= 1; });

        
    }
}
