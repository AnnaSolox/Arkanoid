using UnityEngine;
using System.Collections.Generic;

public class BlockPool : MonoBehaviour
{
    public GameObject[] blockPrefabs; 
    private List<GameObject> blockPool = new List<GameObject>();

    public static BlockPool Instance; 

    private int activeBlocks = 0; 

    public ParticleSystem redExplosionPrefab;
    public ParticleSystem blueExplosionPrefab;
    public ParticleSystem greenExplosionPrefab;
    public ParticleSystem pinkExplosionPrefab;
    public ParticleSystem yellowExplosionPrefab;

    void Awake()
    {
        Instance = this;
    }

    // Obtiene un bloque del pool y lo activa en la posición indicada
    public GameObject GetBlock(Vector2 position, int blockIndex)
    {
        foreach (GameObject block in blockPool)
        {
            // Asegurar mismo tipo de bloque
            if (!block.activeInHierarchy && block.CompareTag(blockPrefabs[blockIndex].tag)) 
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
        activeBlocks--; 
    }

    // Método para obtener el número de bloques activos
    public int GetActiveBlockCount()
    {
        return activeBlocks;
    }

    // Método para obtener el prefab de partículas de explosión basado en el tag del bloque que colisionó
    public ParticleSystem GetExplosionPrefab(string blockTag)
    {
        switch (blockTag)
        {
            case "RedBlock":
                return redExplosionPrefab;
            case "BlueBlock":
                return blueExplosionPrefab;
            case "GreenBlock":
                return greenExplosionPrefab;
            case "YellowBlock":
                return yellowExplosionPrefab;
            case "PinkBlock":
                return pinkExplosionPrefab;
            default:
                return null;
        }
    }
}
