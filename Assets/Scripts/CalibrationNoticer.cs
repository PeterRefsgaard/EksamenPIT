using UnityEngine;
using TMPro;

public class CalibrationNoticer : MonoBehaviour
{
    public GameObject leftController;          // Reference til venstre controller
    public GameObject rightController;         // Reference til h?jre controller
    public GameObject leftCalibrationPoint;    // Venstre kalibreringscirkel
    public GameObject rightCalibrationPoint;   // H?jre kalibreringscirkel
    public float countdownDuration = 3f;       // Nedt?lling i sekunder

    [Header("UI Elements")]
    public TMP_Text countdownText;             // Reference til TMP Text til nedt?lling
    public GameObject infoText;                // Reference til "Info Text"
    public GameObject beginFlappingText;       // Reference til "Begin Flapping Text"
    public GameObject uiCanvas;                // Reference til hele UI Canvas

    private bool leftInCircle = false;         // Om venstre controller er inden for cirklen
    private bool rightInCircle = false;        // Om h?jre controller er inden for cirklen
    private float countdownTimer = 0f;         // Nedt?llingens aktuelle v?rdi
    private bool calibrationComplete = false;  // Om kalibreringen er succesfuld

    private void Start()
    {
        // S?rg for, at starten er korrekt sat
        if (beginFlappingText != null)
        {
            beginFlappingText.SetActive(false);
        }
        if (infoText != null)
        {
            infoText.SetActive(true); // Info Text aktiv i starten
        }
        if (countdownText != null)
        {
            countdownText.text = ""; // Nedt?llingstekst tom i starten
        }
    }

    private void Update()
    {
        // Hvis kalibreringen allerede er fuldf?rt, g?r intet
        if (calibrationComplete)
        {
            return;
        }

        // Tjek om venstre controller er i cirklen
        if (IsInsideCircle(leftController.transform.position, leftCalibrationPoint.transform.position, leftCalibrationPoint.transform.localScale.x / 2))
        {
            leftInCircle = true;
        }
        else
        {
            leftInCircle = false;
            countdownTimer = 0f; // Nulstil nedt?lling
            UpdateCountdownText(""); // Ryd tekst
        }

        // Tjek om h?jre controller er i cirklen
        if (IsInsideCircle(rightController.transform.position, rightCalibrationPoint.transform.position, rightCalibrationPoint.transform.localScale.x / 2))
        {
            rightInCircle = true;
        }
        else
        {
            rightInCircle = false;
            countdownTimer = 0f; // Nulstil nedt?lling
            UpdateCountdownText(""); // Ryd tekst
        }

        // Hvis begge controllere er i cirklerne
        if (leftInCircle && rightInCircle)
        {
            countdownTimer += Time.deltaTime; // Start nedt?lling

            // Opdater nedt?llingsteksten
            UpdateCountdownText(Mathf.Ceil(countdownDuration - countdownTimer).ToString());

            if (countdownTimer >= countdownDuration)
            {
                CompleteCalibration();
                countdownTimer = 0f; // Reset nedt?llingen
            }
        }
    }

    // Tjek om en position er inden for en cirkel
    private bool IsInsideCircle(Vector3 point, Vector3 circleCenter, float radius)
    {
        float distance = Vector3.Distance(new Vector3(point.x, 0, point.z), new Vector3(circleCenter.x, 0, circleCenter.z));
        return distance <= radius;
    }

    // Opdater countdown-teksten
    private void UpdateCountdownText(string text)
    {
        if (countdownText != null)
        {
            countdownText.text = text;
        }
    }

    // Fuldf?r kalibreringen
    private void CompleteCalibration()
    {
        calibrationComplete = true;

        // Deaktiver kalibreringspunkterne
        if (leftCalibrationPoint != null)
        {
            leftCalibrationPoint.SetActive(false);
        }
        if (rightCalibrationPoint != null)
        {
            rightCalibrationPoint.SetActive(false);
        }

        // Deaktiver Info Text
        if (infoText != null)
        {
            infoText.SetActive(false);
        }

        // Ryd nedt?llingsteksten
        UpdateCountdownText("");

        // Vis "Begin Flapping Text"
        if (beginFlappingText != null)
        {
            beginFlappingText.SetActive(true);
        }

        // S?t isGameStarted til true i GameManagement
        if (GameManagement.Instance != null)
        {
            GameManagement.Instance.StartGame(); // Dette s?tter isGameStarted til true
            Debug.Log("Calibration complete! Game started.");
        }

        // Start coroutine for at deaktivere UI
        StartCoroutine(DeactivateUICanvasAfterDelay(2f));
    }

    // Coroutine til at deaktivere UI Canvas efter en forsinkelse
    private System.Collections.IEnumerator DeactivateUICanvasAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Deaktiver "Begin Flapping Text"
        if (beginFlappingText != null)
        {
            beginFlappingText.SetActive(false);
        }

        // Deaktiver hele UI Canvas
        if (uiCanvas != null)
        {
            uiCanvas.SetActive(false);
        }

       // Debug.Log("UI Canvas deactivated.");
    }
}
