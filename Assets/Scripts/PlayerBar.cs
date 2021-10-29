using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Enumerado
public enum BarType
{
    healthBar,
    manaBar
}


public class PlayerBar : MonoBehaviour
{
    //Referenciar el slider creado
    private Slider slider;
    public BarType type;
    
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();

        switch (type)
        {
            case BarType.healthBar:
                slider.maxValue = PlayerController.MAX_HEALTH;
                break;
            case BarType.manaBar:
                slider.maxValue = PlayerController.MAX_MANA;
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Tomar datos actuales
        switch (type)
        {
            //Transforma los datos a la altura de la barra
            case BarType.healthBar:
                slider.value = GameObject.Find("Player").GetComponent<PlayerController>().
                    GetHealth();
                break;
            case BarType.manaBar:
                slider.value = GameObject.Find("Player").GetComponent<PlayerController>().
                    GetMana();
                break;
        }
    }
}
