using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void Awake() {
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(SignInToGooglePlayServices);
    }

    public void Settings() {
        //TODO Settings scene
        SceneManager.LoadScene("SettingsScene");
    }

    public void EasyGame() {
        SceneManager.LoadScene("EasyGameScene");
    }

    public void MedGame() {
        SceneManager.LoadScene("MedGameScene");
    }

    public void HardGame() {
        SceneManager.LoadScene("HardGameScene");
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    // ReSharper disable Unity.PerformanceAnalysis
    internal void SignInToGooglePlayServices(SignInStatus status) {
        PlayGamesPlatform.Instance.Authenticate((success) => {
            if (status == SignInStatus.Success) {
                print("Login with Google Play Games successful");
                PlayerPrefs.SetInt("PlayAuthenticated", 1);
            }
            else {
                print("Status = " + status);
                print("Failed to retrieve Google Play games authorization code");
                PlayerPrefs.SetInt("PlayAuthenticated", 0);
            }
        });
    }
    
    internal void ProcessAuthentication(SignInStatus status) {
        if (status == SignInStatus.Success) {
            PlayerPrefs.SetInt("PlayAuthenticated", 1);
        } else {
            PlayerPrefs.SetInt("PlayAuthenticated", 0);
        }
    }
}
