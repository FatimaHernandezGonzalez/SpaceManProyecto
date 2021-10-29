using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enumerados de elementos
public enum CollectableType
{
    healthPotion,
    manaPotion,
    money
}

public class Collectable : MonoBehaviour
{

    public CollectableType type = CollectableType.money;
    private SpriteRenderer sprite; //acceder a la imagen visual 
    private CircleCollider2D itemCollider; //Acceder al collider del objeto 
    bool hasBeenCollected = false;  //Ah sido recolectada? 
    public int value = 1; //valor del objeto
    GameObject player; //Variable Switch
    

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        itemCollider = GetComponent<CircleCollider2D>();
    }
    private void Start()
    {
        player = GameObject.Find("Player");
    }
    void Show()
    {
        //Mostrar la moneda 
        sprite.enabled = true;
        itemCollider.enabled = true;
        hasBeenCollected = false;
    } 
    void Hide()
    {
        //Esconder moneda
        sprite.enabled = false;
        itemCollider.enabled = false;
    }
    void Collect()
    {
        Hide();
        hasBeenCollected = true;

        switch (this.type)
        {
            case CollectableType.money:
                //TODO Logica moneda
                GameManager.sharedInstance.CollectObject(this); //Objeto tipo moneda
                GetComponent<AudioSource>().Play();
                break;
            case CollectableType.healthPotion:
                //TODO Logica pocion vida
                player.GetComponent<PlayerController>().CollectHealth(this.value);
                break;
            case CollectableType.manaPotion:
                //TODO Logica pocion mana
                player.GetComponent<PlayerController>().CollectMana(this.value);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //Destroy(gameObject);
            Collect();

        }
    }
}
