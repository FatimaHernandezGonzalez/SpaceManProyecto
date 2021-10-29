using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour

{
    //Variables Movimiento Personaje 
    public float jumpForce = 6f;
    public float runningSpeed = 2.6f; 

    Rigidbody2D rigidBody;
    Animator animator;
    Vector3 startPosition; 

    const string STATE_ALIVE = "isAlive";
    const string STATE_ON_THE_GROUND = "isOnTheGround";

    //Programar vida
    
    [SerializeField]
    private int healthPoints, manaPoints;
    public const int INITIAL_HEALTH = 100, INITIAL_MANA = 15, 
        MAX_HEALTH = 200, MAX_MANA = 30,
        MIN_HEALTH = 10, MIN_MANA = 0;

    public const int SUPERJUMP_COST = 5;
    public const float SUPERJUMP_FORCE = 1.5f;

    public float jumpRaycastDistance = 1.31f;

    public LayerMask groundMask;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>(); //arranca accion 
        animator = GetComponent<Animator>();
    }


    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
    }

    public void StartGame()
    {

        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);

        healthPoints = INITIAL_HEALTH;
        manaPoints = INITIAL_MANA;

        //Invocar la llamada a dicho metodo despues de cierto tiempo 
            Invoke("RestartPosition", 0.2f);
    }

    void RestartPosition()
    {
        this.transform.position = startPosition; //Gravedad 0
        this.rigidBody.velocity = Vector2.zero;
        GameObject mainCamera = GameObject.Find("Main Camera");
        mainCamera.GetComponent<CameraFollow>().ResetCameraPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))  //Con que tecla o boton reaccionara o elegira la accion atrapada por el imput manager 
        {
            Jump(false); //Delega la tarea a otro metodo 
        }
        if (Input.GetButtonDown("Superjump"))
        {
            Jump(true);
        }


        animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());
        Debug.DrawRay(this.transform.position, Vector2.down * jumpRaycastDistance , Color.cyan);
    }
    private void FixedUpdate()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame) 
        {
            if (rigidBody.velocity.x < runningSpeed)
            {
                rigidBody.velocity = new Vector2(runningSpeed, //x
                                                    rigidBody.velocity.y //y
                                                    );
            }
        }
        else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y); //Indica que se detendra si no esta en la partida 
        }
    }

    void Jump(bool superjump)
    {
        float jumpForceFactor = jumpForce;
        //detectar si hay supersalto
        if (superjump && manaPoints>=SUPERJUMP_COST)
        {
            manaPoints -= SUPERJUMP_COST;
            jumpForceFactor *= SUPERJUMP_FORCE;
        }

        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (IsTouchingTheGround())
            {
                GetComponent<AudioSource>().Play();
                rigidBody.AddForce(Vector2.up * jumpForceFactor, ForceMode2D.Impulse); //Reaccion para saltar 
            }
        }
    }

    //Metodo que indica si el personaje esta tocando el suelo 
    bool IsTouchingTheGround() 
    {
        //De donde a donde se traza el rayo
        if(Physics2D.Raycast(this.transform.position,
                                    Vector2.down,
                                    jumpRaycastDistance,
                                    groundMask))
        {
            return true; //Toca el suelo
            
        }
        else
        {
            //TO DO: Programar logica de no contacto
            //animator.enabled = false; //Pausa animacion salto
            return false; //No toca el suelo 
        }
            
    }

    //LLama animacion muerte y el juego termina
    public void Die()
    {//Rrecoge valor de otras pertidas
        float travelledDistance = GetTravelDistance();
        float previusMaxDistance = PlayerPrefs.GetFloat("maxscore", 0f);
        if(travelledDistance > previusMaxDistance)
        {
            PlayerPrefs.SetFloat("maxscore", travelledDistance);
        }

        this.animator.SetBool(STATE_ALIVE, false);
        GameManager.sharedInstance.GameOver(); 

    }

    public void CollectHealth (int points)
    {
        //Incrementa por puntos suministrados en parametros 
        this.healthPoints += points;
        if(this.healthPoints >= MAX_HEALTH)
        {
            this.healthPoints = MAX_HEALTH;
        }
        //Muere el personaje por daño
        if(this.healthPoints<=0){
            Die();
        }
    }
    public void CollectMana(int points)
    {
        this.manaPoints += points;
        if (this.manaPoints >= MAX_MANA)
        {
            this.manaPoints = MAX_MANA;
        }
    }
    public int GetHealth()
    {
        return healthPoints;
    }
    public int GetMana()
    {
        return manaPoints;
    }

    public float GetTravelDistance()
    {
        //Total de espacio recorrido en las x
        return this.transform.position.x - startPosition.x;
    }
}

