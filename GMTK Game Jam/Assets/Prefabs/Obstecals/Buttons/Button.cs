using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public enum ButtonType { Normal, Timed};
    public ButtonType type;

    public enum ButtonAction { Door, Spawn, Destory};
    public ButtonAction action;
    public Animator animator;

    bool pressed = false;

    float time;
    public float ReactivetionTime;

    public bool activeMode = true;
    public GameObject door;
    public GameObject spawnItem;

    public void Press()
    {
        animator.SetBool("Clicked", true);
        if (action == ButtonAction.Door)
        {
            door.active = activeMode;
        }
        else if (action == ButtonAction.Spawn)
        {
            GameObject obj = Instantiate(spawnItem);
        }
        else if (action == ButtonAction.Destory)
        {
            Destroy(door);
        }
        time = 0;
        pressed = true;
    }

    public void Unpress()
    {
        animator.SetBool("Clicked", false);
        if (action == ButtonAction.Door)
        {
            door.active = !activeMode;
        }
        pressed = false;
    }

    void Update(){
        if (pressed && type == ButtonType.Timed){
            time += Time.deltaTime;
            if (time >= ReactivetionTime){
                Unpress();
            }
        }
    }


}
