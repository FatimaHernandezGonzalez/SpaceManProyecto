using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Singleton 
    public static LevelManager sharedInstance;
    //Lista de niveles
    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();
    public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>(); //Gestiona contenido visual de la escena, guarda en memoria los bloques de nivel 
    //Donde se crea el primer bloque 
    public Transform levelStartPosition; 
    // Start is called before the first frame update

    void Awake()
    {
        if(sharedInstance == null)
        {
            sharedInstance = this;
        }
    }
    void Start()
    {
        GenerateInitialBlocks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddLevelBlock() //Añadir un nuevo bloque
    {
        int randomIdx = Random.Range(0, allTheLevelBlocks.Count); //Numeros aleatorios Uniformes

        LevelBlock block;
        Vector3 spawnPosition = Vector3.zero; //Donde se colocara el bloque? 
        if(currentLevelBlocks.Count == 0) //Si aun no se añade, el bloque cero 
        {
            block = Instantiate(allTheLevelBlocks[0]);
            spawnPosition = levelStartPosition.position;
        }
        else
        {
            block = Instantiate(allTheLevelBlocks[randomIdx]); //Se debe colocar al final del anterior ¿Donde se coloca?
            spawnPosition = currentLevelBlocks
                            [currentLevelBlocks.Count - 1]
                            .exitPoint.position;
        }
        block.transform.SetParent(this.transform,false); //Todos los bloques seran hijos de este codigo 

        Vector3 correction = new Vector3(
            spawnPosition.x - block.startPoint.position.x,
            spawnPosition.y - block.startPoint.position.y,
            0);

        block.transform.position = correction;
        currentLevelBlocks.Add(block);
    }
    public void RemoveLevelBlock()
    {
        LevelBlock oldBlock = currentLevelBlocks[0];
        currentLevelBlocks.Remove(oldBlock);
        Destroy(oldBlock.gameObject);
    }
    public void RemoveAllBlocks()
    {
        while (currentLevelBlocks.Count > 0) //Elimina bloques
        {
            RemoveLevelBlock();
        }
    }
    public void GenerateInitialBlocks()
    {
        for(int i = 0 ; i < 3 ; i ++)
        {
            AddLevelBlock();
        }
    }
}
