using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject initialUIElements;       // Reference til Initial UI (fx Start-knap osv.)
    public GameObject secondaryUIElements;    // Reference til Secondary UI
    public GameObject calibrationUIElements;  // Reference til Calibration UI
    public GameObject calibrationUIInfoElements; // Reference til Calibration Info UI
    public GameObject lastUIElements;         // Reference til Last UI

    [Header("Calibration Points")]
    public GameObject leftCalibrationPoint;    // Reference til venstre kalibreringscirkel
    public GameObject rightCalibrationPoint;   // Reference til h?jre kalibreringscirkel

    // Metode til at aktivere Secondary UI og deaktivere Initial UI
    public void OnStartButtonPressed()
    {
        if (initialUIElements != null)
        {
            initialUIElements.SetActive(false);
        }

        if (secondaryUIElements != null)
        {
            secondaryUIElements.SetActive(true);
        }
    }

    // Metode til at g? tilbage fra andre UI-sektioner til Initial UI
    public void OnBackButtonPressed()
    {
        if (initialUIElements != null)
        {
            initialUIElements.SetActive(true);
        }

        if (secondaryUIElements != null)
        {
            secondaryUIElements.SetActive(false);
        }
        if (calibrationUIElements != null)
        {
            calibrationUIElements.SetActive(false);
        }
        if (lastUIElements != null)
        {
            lastUIElements.SetActive(false);
        }

        // Deaktiver kalibreringspunkter
        if (leftCalibrationPoint != null)
        {
            leftCalibrationPoint.SetActive(false);
        }
        if (rightCalibrationPoint != null)
        {
            rightCalibrationPoint.SetActive(false);
        }
    }

    // Metode til at v?lge kalibreringsmuligheder
    public void OnSelectCalibrationButtonPressed()
    {
        if (secondaryUIElements != null)
        {
            secondaryUIElements.SetActive(false);
        }
        if (calibrationUIElements != null)
        {
            calibrationUIElements.SetActive(true);
        }
    }

    // Metode til at aktivere Mode 1 kalibreringslogik
    public void OnMode1Pressed()
    {
        // Aktiver kalibreringspunkter
        if (leftCalibrationPoint != null)
        {
            leftCalibrationPoint.SetActive(true);
        }
        if (rightCalibrationPoint != null)
        {
            rightCalibrationPoint.SetActive(true);
        }

        // Deaktiver Calibration UI Elements
        if (calibrationUIElements != null)
        {
            calibrationUIElements.SetActive(false);
        }

        // Aktiver Calibration Info UI Elements
        if (calibrationUIInfoElements != null)
        {
            calibrationUIInfoElements.SetActive(true);
        }

      //  Debug.Log("Mode 1 pressed: Calibration points activated and Calibration Info UI displayed");
    }
}
