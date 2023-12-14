using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI altitudeText;

    public TextMeshProUGUI winText;

    public static GameUI instance;

    void Awake() { instance = this; }

    void Update()
    {
        altitudeText.text = "Highest Player: " + GameManager.instance.highestPlayerPosition;
    }
    void UpdateAltitudeTxt()
    {

    }


}
