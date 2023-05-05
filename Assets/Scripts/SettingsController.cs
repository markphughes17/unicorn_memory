using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour {
    [SerializeField] private Toggle toggle;

    void Start() {
        toggle.isOn = PlayerPrefs.GetInt("sounds") == 1;
    }
    
    public void ToLeaderboards() {
        SceneManager.LoadScene("LeaderboardsScene");
    }

    public void ToPrivacy() {
        Application.OpenURL("https://www.markhughes.tech/privacy-app/");
    }

    public void ToMenu() {
        SceneManager.LoadScene("MenuScene");
    }

    public void ToggleSound() {
        PlayerPrefs.SetInt("sounds", toggle.isOn ? 1 : 0);
        Debug.Log("sounds changed to " + toggle.isOn);
    }
}
