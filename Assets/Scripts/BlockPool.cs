using UnityEngine;
using System.Collections.Generic;

public class BlockPool : MonoBehaviour
{
    public GameObject[] blockPrefabs; // Prefabs de bloques de distintos colores
    public int poolSize = 50; // Tamaño del pool
    private List<GameObject> blockPool = new List<GameObject>();

    public static BlockPool Instance; // Singleton para acceso fácil

    private int activeBlocks = 0; // Contador de bloques activos

    void Awake()
    {
        Instance = this;
        InitializePool();
    }

    void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject block = Instantiate(blockPrefabs[Random.Range(0, blockPrefabs.Length)]);
            block.SetActive(false);
            blockPool.Add(block);
        }
    }

    // Obtiene un bloque del pool y lo activa en la posición indicada
    public GameObject GetBlock(Vector2 position)
    {
        foreach (GameObject block in blockPool)
        {
            if (!block.activeInHierarchy) // Si el bloque está inactivo
            {
                block.transform.position = position; // Asigna la posición correcta antes de activarlo
                block.SetActive(true);
                activeBlocks++; // Aumenta el contador de bloques activos
                return block;
            }
        }
        return null; // Si todos los bloques están en uso
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
