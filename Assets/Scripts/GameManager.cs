using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform ball;
    public float gameOverY = -110f; // Límite donde la bola cae fuera
    private int totalBlocks;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        if (UIManager.Instance == null)
        {
            Debug.LogError("UIManager no está asignado en la escena.");
            return;
        }
        // Obtener el número de nivel actual
        int nivelActual = SceneManager.GetActiveScene().buildIndex + 1;
        // Mostrar mensaje en pantalla
        UIManager.Instance.ShowMessage($"Nivel {nivelActual}: ¡A jugar!", 1f);
        // Asegurarse de que los bloques estén inicializados antes de contar
        InitializeBlockCount();
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.GetCurrentScore();
        }
    }

    // Método para inicializar el contador de bloques
    void InitializeBlockCount()
    {
        // Asegurarse de que BlockPool esté completamente inicializado
        Invoke(nameof(SetTotalBlocks), 0.1f);  // Un pequeño retraso para que los bloques se inicialicen
    }

    void SetTotalBlocks()
    {
        totalBlocks = BlockPool.Instance.GetActiveBlockCount();
        Debug.Log("Bloques en la escena: " + totalBlocks);
    }

    void Update()
    {
        CheckGameOver();
    }

    void CheckGameOver()
    {
        if (ball.transform.position.y < gameOverY)
        {
            ShowGameOver();
        }
    }

    public void BlockHidden()
    {
        totalBlocks--;
        if (totalBlocks <= 0)
        {
            UIManager.Instance.ShowMessage("¡Nivel completado!", 1f);
            //Pausar el juego hasta cargar la siguiente escena
            Time.timeScale = 0f;
            StartCoroutine(WaitAndLoadNextScene(2f));
        }
    }

    void ShowGameOver()
    {
        Debug.Log("Game Over!");
        UIManager.Instance.ShowMessage("¡Game Over!", 2f);
        Invoke(nameof(RestartGame), 2f);
    }

    void RestartGame()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ResetScore();
        }
        SceneManager.LoadScene(0);
    }

    IEnumerator WaitAndLoadNextScene(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); // Usamos WaitForSecondsRealtime para no depender de Time.timeScale
        Time.timeScale = 1f; // Restaurar el tiempo
        LoadNextScene();
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
