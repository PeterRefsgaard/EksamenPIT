using UnityEngine;

public class GameManagement : MonoBehaviour
{
    // Singleton instance
    public static GameManagement Instance { get; private set; }
    private bool isGameStarted = false;

    [Header("UI Elements")]
    public GameObject initialUIElements;       // Parent til Start-knappen osv.
    public GameObject secondaryUIElements;    // Parent til Secondary UI Elements
    public GameObject calibrationModeUI;      // Parent til Calibration Mode UI
    public GameObject lastUIElements;         // Parent til Last UI Elements
    public GameObject calibrationPoints;

    private void Awake()
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
    }

    public void StartGame()
    {
        isGameStarted = true;

        // Skjul initial UI
        if (initialUIElements != null)
            initialUIElements.SetActive(false);

        // Skjul alle andre UI-elementer
        if (secondaryUIElements != null)
            secondaryUIElements.SetActive(false);

        if (calibrationModeUI != null)
            calibrationModeUI.SetActive(false);

        if (lastUIElements != null)
            lastUIElements.SetActive(false);

        if (calibrationPoints != null)
            calibrationPoints.SetActive(false);
    }
    public bool IsGameStarted()
    {
        return isGameStarted;
    }
    public void ResetGame()
    {
        StartGame();
    }

    public void EndGame()
    {
        isGameStarted = false;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
