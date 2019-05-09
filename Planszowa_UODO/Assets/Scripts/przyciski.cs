using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class przyciski : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void StartGame()
    {
        // Debug.Log("Uruchomienie Gry");


       
        SceneManager.LoadScene(1);


    }

    public void KoniecGame()
    {
        // Debug.Log("Koniec Gry");
        Application.Quit();
    }

    

  
}
