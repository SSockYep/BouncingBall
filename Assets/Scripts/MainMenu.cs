using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject message;
    public void PlayGame()
    {
        if (Random.value >= 0.5f)
        {
            message.GetComponent<MessageScript>().is2DFirst = false;
            SceneManager.LoadScene(1);
        }
        else
        {
            message.GetComponent<MessageScript>().is2DFirst = true;
            SceneManager.LoadScene(2);
        }
        
    }
    void Start()
    {
        DontDestroyOnLoad(message);
    }
  
}
