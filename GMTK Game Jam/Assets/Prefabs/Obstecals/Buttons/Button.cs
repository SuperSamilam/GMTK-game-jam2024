using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public enum ButtonType { Normal, Timed};
    public ButtonType type;

    public enum ButtonAction { Door, Spawn};
    public ButtonAction action;
    public Animator animator;

    bool pressed = false;

    float time;
    public float ReactivetionTime;

    public GameObject door;
    public Transform spawnPos;
    public GameObject spawnItem;

    public void Press()
    {
        animator.SetBool("Clicked", true);
        if (action == ButtonAction.Door)
        {
            door.active = false;
        }
        else if (action == ButtonAction.Spawn)
        {
            GameObject obj = Instantiate(spawnItem);
            obj.transform.position = spawnPos.position;
        }
        time = 0;
        pressed = true;
    }

    public void Unpress()
    {
        animator.SetBool("Clicked", false);
        if (action == ButtonAction.Door)
        {
            door.active = true;
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
