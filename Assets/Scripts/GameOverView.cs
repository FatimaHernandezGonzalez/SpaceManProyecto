using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour
{
    public Text coinsText, scoreText;

    private PlayerController controller;
    // Start is called before the first frame update
    void Start()
    {
        //Traemos información del jugador
        controller = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            int coins = GameManager.sharedInstance.collectedObject;
            float score = controller.GetTravelDistance();
            float maxScore = PlayerPrefs.GetFloat("maxscore", 0f);

            coinsText.text = coins.ToString(); //Pasa d entero a string
            scoreText.text = score.ToString("f1");
            

        }
    }
}
