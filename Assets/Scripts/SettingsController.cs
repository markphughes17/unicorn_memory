using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour
{

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
        //TODO sounds
    }
}
