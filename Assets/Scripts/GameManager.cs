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
        Invoke("SetTotalBlocks", 0.1f);  // Un pequeño retraso para que los bloques se inicialicen
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
            LoadNextScene();
        }
    }

    void ShowGameOver()
    {
        Debug.Log("Game Over!");
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ResetScore();
        }
        SceneManager.LoadScene(0); 
    }

    void LoadNextScene()
    {
        Debug.Log("¡Siguiente Nivel!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Cargar siguiente nivel
    }
}
