using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

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
}
