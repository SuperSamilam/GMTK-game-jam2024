using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void LoadB(int sceneANumber)
    {
        SceneManager.LoadScene(sceneANumber);
    }
}
