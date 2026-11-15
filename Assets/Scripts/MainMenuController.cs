using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OpenSoloTravel()
    {
        SceneManager.LoadScene("SoloTravelScene");
    }

    public void OpenQuickRace()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OpenSettings()
    {
        Debug.Log("Settings opened");
    }

    public void OpenGarage()
    {
        Debug.Log("Garage opened");
    }

    public void OpenMultiplayer()
    {
        Debug.Log("Multiplayer coming soon");
    }

    public void OpenCareerMode()
    {
        Debug.Log("Career Mode coming soon");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
