using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Levels : MonoBehaviour
{
    public UnityEngine.UI.Button b1;
    public UnityEngine.UI.Button b2;
    public UnityEngine.UI.Button b3;

    private void Start()
    {
        if (PlayerPrefs.GetInt("3", -1) != 1)
        {
            b1.interactable = false;
        }
        if (PlayerPrefs.GetInt("4", -1) != 1)
        {
            b2.interactable = false;
        }
    }
}
