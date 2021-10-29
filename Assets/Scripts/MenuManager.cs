using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public Canvas menuCanvas; //Permitira gestionar el canvas 
    public Canvas gameCanvas;
    public Canvas gameOverCanvas;


    public static MenuManager sharedInstance;

    // Start is called before the first frame update

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }
    //Activa Canvas
    public void ShowMainMenu()
    {
        menuCanvas.enabled = true;
    }
    public void HideMainMenu()
    {
        menuCanvas.enabled = false;
    }

    public void ShowGameMenu()
    {
        gameCanvas.enabled = true;
    }

    public void HideGameMenu()
    {
        gameCanvas.enabled = false;
    }
    public void ShowGameOverMenu()
    {
        gameOverCanvas.enabled = true;
    }
    public void HideGameOverMenu()
    {
        gameOverCanvas.enabled = false;
    }

    public void ExitGame(){
        #if UNITY_EDITOR 
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
