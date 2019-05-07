using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class GameControl : MonoBehaviour {

    private static GameObject whoWinsTextShadow, player1MoveText, pytanie1Shadow, TrueButtonShadow, FalseButtonShadow, player2MoveText, player1Icon, player2Icon;

    private static GameObject player1, player2;

    public static int diceSideThrown = 0;
    public static int player1StartWaypoint = 0;
    public static int player2StartWaypoint = 0;

    public static bool gameOver = false;
   

    public Question[] question;
    private static List<Question> unanswerQuestion;
    private Question currentQuestion;
    [SerializeField]
    private Text factText;
    bool MyFunctionCalled = false;
    private bool beingHandled;

    // Use this for initialization
    void Start () {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        whoWinsTextShadow = GameObject.Find("WhoWinsText");
        pytanie1Shadow = GameObject.Find("PytaniaPanel");
        TrueButtonShadow = GameObject.Find("TrueButton");
        FalseButtonShadow = GameObject.Find("FalseButton");
        player1MoveText = GameObject.Find("Player1MoveText");
        player1Icon = GameObject.Find("Player1Icon");
        player2MoveText = GameObject.Find("Player2MoveText");
        player2Icon = GameObject.Find("Player2Icon");
       

        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");

        player1.GetComponent<FollowThePath>().moveAllowed = false;
        player2.GetComponent<FollowThePath>().moveAllowed = false;

        whoWinsTextShadow.gameObject.SetActive(false);
        pytanie1Shadow.gameObject.SetActive(false);
        TrueButtonShadow.gameObject.SetActive(false);
        FalseButtonShadow.gameObject.SetActive(false);
        player1MoveText.gameObject.SetActive(true);
        player1MoveText.GetComponent<Text>().text = "Graczu 1 rzuć kostką ";
        player1Icon.gameObject.SetActive(true);
        player2MoveText.gameObject.SetActive(false);
        player2Icon.gameObject.SetActive(false);
       


        if (unanswerQuestion == null || unanswerQuestion.Count == 0)
        {
            unanswerQuestion = question.ToList<Question>();
        }
        SetCurenntQuestion();
        //Debug.Log(currentQuestion.fact + " is " + currentQuestion.isTrue);

    }

    // Update is called once per frame
    void Update()
    {
       

        if (player1.GetComponent<FollowThePath>().waypointIndex > 
            player1StartWaypoint + diceSideThrown)
        {
            player1.GetComponent<FollowThePath>().moveAllowed = false;
            player1MoveText.gameObject.SetActive(false);
            player1Icon.gameObject.SetActive(false);
            player2MoveText.gameObject.SetActive(true);
            player2MoveText.GetComponent<Text>().text = "Graczu 2 rzuć kostką ";
            player2Icon.gameObject.SetActive(true);
            MyFunctionCalled = false;
            player1StartWaypoint = player1.GetComponent<FollowThePath>().waypointIndex - 1;
            
            StartCoroutine(PytaniaRandomCzekaj());

        }

        if (player2.GetComponent<FollowThePath>().waypointIndex >
            player2StartWaypoint + diceSideThrown)
        {
            player2.GetComponent<FollowThePath>().moveAllowed = false;
            player2MoveText.gameObject.SetActive(false);
            player2Icon.gameObject.SetActive(false);
            player1MoveText.gameObject.SetActive(true);
            player1MoveText.GetComponent<Text>().text = "Graczu 1 rzuć kostką ";
            player1Icon.gameObject.SetActive(true);
            MyFunctionCalled = false;
            player2StartWaypoint = player2.GetComponent<FollowThePath>().waypointIndex - 1;
            
            StartCoroutine(PytaniaRandomCzekaj());
        }
        
       
        //Koniec pytania warunki 

        if (player1.GetComponent<FollowThePath>().waypointIndex == 
            player1.GetComponent<FollowThePath>().waypoints.Length)
        {
            whoWinsTextShadow.gameObject.SetActive(true);
            whoWinsTextShadow.GetComponent<Text>().text = "Wygrał pierwszy gracz";
           

        }

        if (player2.GetComponent<FollowThePath>().waypointIndex ==
            player2.GetComponent<FollowThePath>().waypoints.Length)
        {
            whoWinsTextShadow.gameObject.SetActive(true);
            player1MoveText.gameObject.SetActive(false);
            player2MoveText.gameObject.SetActive(false);
            whoWinsTextShadow.GetComponent<Text>().text = "Wygrał drugi gracz";
            

        }
        //If the game is over and the player has pressed some input...
        if (gameOver && Input.GetMouseButtonDown(0))
        {
            //...reload the current scene.
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //SceneManager.LoadScene("SampleScene");
            SceneManager.LoadScene(0);
            gameOver = false;
            diceSideThrown = 0;
            player1StartWaypoint = 0;
            player2StartWaypoint = 0;
}

    }
    void SetCurenntQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unanswerQuestion.Count);
        currentQuestion = unanswerQuestion[randomQuestionIndex];

        factText.text = currentQuestion.fact;
        unanswerQuestion.RemoveAt(randomQuestionIndex);

    }
    //Obsluga przyciskow
    public void UserSelectTrue()
    {
        if (currentQuestion.isTrue)
        {
            pytanie1Shadow.gameObject.SetActive(false);
            TrueButtonShadow.gameObject.SetActive(false);
            FalseButtonShadow.gameObject.SetActive(false);
            
            //  Debug.Log("Poprawna");
        }
        else
            Debug.Log("Błędna");

    }
    public void UserSelectFalse()
    {
        if (!currentQuestion.isTrue)
        {
           // Debug.Log("Poprawna");
            pytanie1Shadow.gameObject.SetActive(false);
            TrueButtonShadow.gameObject.SetActive(false);
            FalseButtonShadow.gameObject.SetActive(false);
           
        }
        else
            Debug.Log("Błędna");

    }
    //koniec obslugi przyciskow
    // funkcja  losowego pytania
    public IEnumerator PytaniaRandomCzekaj()
    {
        yield return new WaitForSeconds(0.3f);
        if (MyFunctionCalled == false)
        {
            MyFunctionCalled = true;
            pytanie1Shadow.gameObject.SetActive(true);
            TrueButtonShadow.gameObject.SetActive(true);
            FalseButtonShadow.gameObject.SetActive(true);
            SetCurenntQuestion();
        }
    }

    // koniec funkcji losowego pytania
    public static void MovePlayer(int playerToMove)
    {
        switch (playerToMove) { 
            case 1:
                player1.GetComponent<FollowThePath>().moveAllowed = true;
                break;

            case 2:
                player2.GetComponent<FollowThePath>().moveAllowed = true;
                break;
        }
    }
}
