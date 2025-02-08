using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform ball;
    public float gameOverY = -110f;
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
        UIManager.Instance.ShowMessage($"Level {nivelActual}: Redy...Play!", 1f);
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
        Invoke(nameof(SetTotalBlocks), 0.1f);
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
            UIManager.Instance.ShowMessage("Level completed!", 1f);
            //Pausar el juego hasta cargar la siguiente escena
            Time.timeScale = 0f;
            StartCoroutine(WaitAndLoadNextScene(2f));
        }
    }

    void ShowGameOver()
    {
        Debug.Log("Game Over!");
        UIManager.Instance.ShowMessage("¡Game Over!", 2f);
        //Pausar el juego hasta cargar la siguiente escena
        Time.timeScale = 0f;
        StartCoroutine(RestartGame(2f));
    }

    IEnumerator RestartGame(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        // Restaurar el tiempo
        Time.timeScale = 1f;
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ResetScore();
        }
        SceneManager.LoadScene(0);
    }

    IEnumerator WaitAndLoadNextScene(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        // Restaurar el tiempo
        Time.timeScale = 1f; 
        LoadNextScene();
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
