using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public int rows = 5;  
    private int columns = 10; 
    private float blockHeight = 10f;
    private float minX = -93f;
    private float maxX = 93f;
    private float startY = 100f;

    void Start()
    {
        SpawnBlocks();
    }

    void SpawnBlocks()
    {
        // Distancia entre bloques
        float blockWidth = (maxX - minX) / (columns - 1); 
        // Cantidad de colores de bloques
        int colorCount = BlockPool.Instance.blockPrefabs.Length; 
        int rowsPerColor = rows / colorCount; // Filas completas por color
        int extraRows = rows % colorCount; // Filas sobrantes a repartir

        int currentRow = 0; // Contador de filas generadas

        for (int colorIndex = 0; colorIndex < colorCount; colorIndex++)
        {
            // Asigna las filas extras de manera justa
            int assignedRows = rowsPerColor + (colorIndex < extraRows ? 1 : 0);

            for (int i = 0; i < assignedRows; i++)
            {
                // Si ya llenamos todas las filas, salimos
                if (currentRow >= rows) break; 

                for (int col = 0; col < columns; col++)
                {
                    float xPos = maxX - (col * blockWidth);
                    float yPos = startY - (currentRow * blockHeight);
                    Vector2 position = new Vector2(xPos, yPos);

                    GameObject block = BlockPool.Instance.GetBlock(position, colorIndex);
                }

                currentRow++; 
            }
        }
    }

}
