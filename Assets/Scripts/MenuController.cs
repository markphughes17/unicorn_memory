using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void Awake() {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    public void Settings() {
        //TODO Settings scene
        SceneManager.LoadScene("SettingsScene");
    }

    public void EasyGame() {
        SceneManager.LoadScene("EasyGameScene");
    }

    public void MedGame() {
        //TODO Medium scene
    }

    public void HardGame() {
        //TODO Hard scene
    }
    
    internal void ProcessAuthentication(SignInStatus status) {
        if (status == SignInStatus.Success) {
            PlayerPrefs.SetInt("PlayAuthenticated", 1);
        } else {
            PlayerPrefs.SetInt("PlayAuthenticated", 0);
        }
    }
}
