using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CompleteScreen : MonoBehaviour

{
    public Text endTimer;

    public void Setup(string endtime)
    {
        gameObject.SetActive(true);
        endTimer.text = endtime;
    }
    public void RestartB()
    {
        SceneManager.LoadScene("Game");
    }
    public void MenuB()
    {
        SceneManager.LoadScene("Begin");
    }
}
                                                                                                                