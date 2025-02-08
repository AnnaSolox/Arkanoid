using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button botonQuit;
    [SerializeField] private Button botonRestart;

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        Debug.Log("QUIT!");
        botonQuit.onClick.AddListener(ExitGame);
        botonRestart.onClick.AddListener(RestartGame);
    }

    // Método para mostrar un mensaje en pantalla
    public void ShowMessage(string message, float duration)
    {
        if (messageText != null)
        {
            messageText.text = message;
            messageText.gameObject.SetActive(true);
            StartCoroutine(HideMessageAfterTime(duration));
        }
    }

    // Corrutina para ocultar el mensaje después de un tiempo
    private IEnumerator HideMessageAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        messageText.gameObject.SetActive(false);
    }

    // Lógica para salir de la aplicación
    void ExitGame()
    {
        // Si estamos en la aplicación de escritorio, cierra la aplicación
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    //Lógica para reiniciar el juego
    void RestartGame()
    {
        // Reiniciar la escena actual
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
