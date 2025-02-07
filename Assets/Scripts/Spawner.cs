using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public int rows = 5;    // Número de filas
    public int columns = 10; // Número de columnas
    public float blockHeight = 10f;
    public float minX = -80f;
    public float maxX = 80f;
    public float startY = 100f;

    void Start()
    {
        SpawnBlocks();
    }

    void SpawnBlocks()
    {
        float blockWidth = (maxX - minX) / (columns - 1); // Espaciado automático

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                // Calcular la posición en X dinámicamente
                float xPos = maxX - (col * blockWidth);
                float yPos = startY - (row * blockHeight);

                Vector2 position = new Vector2(xPos, yPos);
                GameObject block = BlockPool.Instance.GetBlock(position);

                // Debug para ver si los bloques están en la posición correcta
                //Debug.Log($"Bloque en fila {row}, columna {col} -> Posición: {position}");
            }
        }
    }
}
