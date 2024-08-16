using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    bool pressed = false;
    [SerializeField] Transform ButtonRed;
    public float pressedStatePos = 0.05f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Press()
    {
        if (!pressed)
        {
            Vector3 pos = ButtonRed.localPosition;
            pos.x = pressedStatePos;
            Debug.Log(pos);
            ButtonRed.localPosition = pos;
            //Do Event
        }
    }
}
