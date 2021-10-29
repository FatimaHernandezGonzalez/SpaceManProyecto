using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Propiedades del enemigo 
    public float runningSpeed = 1.5f;
    Rigidbody2D rigidBody;

    //Daño
    public int enemyDamage = 10;

    public bool facingRigth = false; //A donde mira

    private Vector3 startPosition; 

    private void Awake()
    {
        // para recuperar el componente
        rigidBody = GetComponent<Rigidbody2D>();
        //Donde anda el enemigo? 
        startPosition = this.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = startPosition;
    }

    private void FixedUpdate()
    {
        //Calculos que afectan el motor 

        float currentRunningSpeed = runningSpeed;

        if (facingRigth)
        {
            //mira a la derecha 
            currentRunningSpeed = runningSpeed;
            this.transform.eulerAngles = new Vector3(0,180,0); //Para rotal al contrario de la posicion actual 0 grados en x, 180 en y, 0 en z, gira 180
        }
        else
        {
            //mira a la izquierda, con esto siempre se ira a una velocidad fija. 
            currentRunningSpeed = -runningSpeed;
            this.transform.eulerAngles = Vector3.zero; //para regresar a la posicion original 
        }
        //En caso de estar en partida, la velocidad se aplicara correctamente al enemigo, cuando el game over esta o no se juega se detendra
        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            rigidBody.velocity = new Vector2(currentRunningSpeed, rigidBody.velocity.y); //Se nueve en x
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Se llama cuando el enemigo entra en contacto con otro collider
        //Debug.Log(collision.tag);

        //Que hacer si choca con? 
        if (collision.tag == "Coin")
        {
            return;
        }
       
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().
                CollectHealth(-enemyDamage);
            return;
        }
        //Si llega aqui no choco con naa, aqui hay otro enemigo o escenario 
        //el enemigo entonces rotara
        facingRigth = !facingRigth;

    }
}
