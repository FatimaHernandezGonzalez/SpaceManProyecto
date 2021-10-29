using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enumerado 
public enum GameState
{
    menu, 
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour
{

    public GameState currentGameState = GameState.menu;

    public static GameManager sharedInstance;
    private PlayerController controller; //Para inicializarla 
    public int collectedObject = 0; //contador de objetos 

    private void Awake()  //No sera llamada a nungun otro lugar
    {
        if (sharedInstance ==null)
        {
            sharedInstance = this; //Asigna al unico Manager
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("Player").GetComponent<PlayerController>(); //Recuperar controlador 
    }

    // Update is called once per frame
    void Update()
    {
        //arrancar con una tecla
        if (Input.GetButtonDown("Submit") && currentGameState != GameState.inGame)
        {
            StartGame(); //Cambia el estado a juego
        }
    }

    //Codigo para comenzar una partida 
    public void StartGame()
    {
        //Cambia el estado del juego 
        SetGameState(GameState.inGame);

    }

    //Codigo terminar partida 
    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }

    //Codigo regresar al menu 
    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }

    //
    private void SetGameState(GameState newGameState)
    {
        if(newGameState == GameState.menu)
        {
            //TODO: Colocar la logica del menu  
            MenuManager.sharedInstance.ShowMainMenu();
            MenuManager.sharedInstance.HideGameMenu();
            MenuManager.sharedInstance.HideGameOverMenu();
        }
        else if (newGameState == GameState.inGame)  //Si esta dentro de la partida
        {
            /*if (currentGameState != GameState.inGame && Input.GetButtonDown("Submit"))
            {*/
                //TODO: Hay que preparar la escena para jugar
            LevelManager.sharedInstance.RemoveAllBlocks();
            LevelManager.sharedInstance.GenerateInitialBlocks(); //Reiniciar bloques
            controller.StartGame();
            MenuManager.sharedInstance.HideMainMenu();//Oculta menu
            MenuManager.sharedInstance.ShowGameMenu();
            MenuManager.sharedInstance.HideGameOverMenu();
            //Invoke("ReloadLevel", 0.1f);
        }
        else if (newGameState == GameState.gameOver)
        {
            //TODO: Preparar el juego para un GAME OVER
            MenuManager.sharedInstance.HideMainMenu();//Oculta menu
            MenuManager.sharedInstance.HideGameMenu();
            MenuManager.sharedInstance.ShowGameOverMenu();
        }
        this.currentGameState = newGameState;
    }

    public void CollectObject(Collectable collectable)
    {
        collectedObject += collectable.value;

    }

    void ReloadLevel() // Limpieza 
    {
        LevelManager.sharedInstance.GenerateInitialBlocks();
        controller.StartGame();
    }

}
