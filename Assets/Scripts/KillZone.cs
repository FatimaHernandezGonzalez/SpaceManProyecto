using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Toca el collider muere
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") //si topa con el jugador, muere
        {
            PlayerController controller =
                collision.GetComponent<PlayerController>();
            controller.Die();
        }
    }
}
