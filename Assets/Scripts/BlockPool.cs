using UnityEngine;
using System.Collections.Generic;

public class BlockPool : MonoBehaviour
{
    public GameObject[] blockPrefabs; // Prefabs de bloques de distintos colores
    private List<GameObject> blockPool = new List<GameObject>();

    public static BlockPool Instance; // Singleton para acceso fácil

    private int activeBlocks = 0; // Contador de bloques activos

    void Awake()
    {
        Instance = this;
    }

    // Obtiene un bloque del pool y lo activa en la posición indicada
    public GameObject GetBlock(Vector2 position, int blockIndex)
    {
        foreach (GameObject block in blockPool)
        {
            if (!block.activeInHierarchy && block.CompareTag(blockPrefabs[blockIndex].tag)) // Asegurar mismo tipo de bloque
            {
                block.transform.position = position;
                block.SetActive(true);
                activeBlocks++;
                return block;
            }
        }

        // Crear los bloques que hagan falta
        GameObject newBlock = Instantiate(blockPrefabs[blockIndex]);
        newBlock.transform.position = position;
        newBlock.SetActive(true);
        blockPool.Add(newBlock);
        activeBlocks++;

        return newBlock;
    }

    // Devuelve un bloque al pool y lo desactiva
    public void ReturnBlock(GameObject block)
    {
        block.SetActive(false);
        activeBlocks--; // Disminuye el contador de bloques activos
    }

    // Método para obtener el número de bloques activos
    public int GetActiveBlockCount()
    {
        return activeBlocks;
    }
}
