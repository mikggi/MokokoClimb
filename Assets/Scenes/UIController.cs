using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Button beginning;
    
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        beginning = root.Q<Button>("StartB");

        beginning.clicked += startingButtonPressed;
    }

    void startingButtonPressed()
    {
        SceneManager.LoadScene("Game");
    }
}
