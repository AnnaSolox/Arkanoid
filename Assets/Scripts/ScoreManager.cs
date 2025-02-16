using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; } 

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI scoreValue;       
    [SerializeField] private TextMeshProUGUI highScoreValue;

    [Header("Score Settings")]
    private int pointsPerBlock = 10;          
    private int paddingZeros = 4;         

    private int currentScore = 0;
    private int highScore = 0;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        currentScore = PlayerPrefs.GetInt("CurrentScore", 0);
        UpdateHighScoreText();
        UpdateScoreText();
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        // Solo reasignar las referencias si no están asignadas aún
        if (scoreValue == null)
        {
            scoreValue = GameObject.Find("ScoreValue")?.GetComponent<TextMeshProUGUI>();
        }

        if (highScoreValue == null)
        {
            highScoreValue = GameObject.Find("HighScoreValue")?.GetComponent<TextMeshProUGUI>();
        }

        // Asegurarnos de que las referencias están asignadas antes de hacer actualizaciones
        if (scoreValue != null && highScoreValue != null)
        {
            UpdateScoreText();
            UpdateHighScoreText();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetScore();
        
    }

    public void AddPoints(int points = -1)
    {
        // Si no se especifican puntos, usa el valor por defecto
        if (points < 0) points = pointsPerBlock;

        // Añadir puntos al marcador
        currentScore += points;
        Debug.Log("Current Score: " + currentScore);
        UpdateScoreText();

        // Actualizar high score si es necesario
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            UpdateHighScoreText();
        }

        PlayerPrefs.SetInt("CurrentScore", currentScore);
    }

    // Método para reiniciar la puntuación
    public void ResetScore()
    {
        currentScore = 0;
        PlayerPrefs.SetInt("CurrentScore", currentScore);
        Debug.Log("Score Reset");
        UpdateScoreText();
    }

    // Método para actualizar el texto de la puntuación
    private void UpdateScoreText()
    {
        if (scoreValue != null)
        {
            // Formatear el número con ceros a la izquierda
            string formattedScore = currentScore.ToString().PadLeft(paddingZeros, '0');
            scoreValue.text = formattedScore;
            Debug.Log("Score Text Updated: " + formattedScore);
        }
    }

    // Método para actualizar el texto de la puntuación máxima
    private void UpdateHighScoreText()
    {
        if (highScoreValue != null)
        {
            string formattedHighScore = highScore.ToString().PadLeft(paddingZeros, '0');
            highScoreValue.text = formattedHighScore;
            Debug.Log("High Score Text Updated: " + formattedHighScore);
        }
    }

    // Método para obtener la puntuación actual
    public int GetCurrentScore()
    {
        return currentScore;
    }

    [RuntimeInitializeOnLoadMethod]
    static void RunOnStart()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += (scene, mode) =>
        {
            if (Instance != null)
            {
                Instance.OnSceneLoaded(scene, mode);
            }
        };
    }
}
