using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Strat_game : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UserSelectStartGame()
    {
        Debug.Log("Uruchomienie Gry");
        SceneManager.LoadScene(1);
    }
}
