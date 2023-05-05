using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    Firebase.FirebaseApp _app;
    public void Awake() {
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(SignInToGooglePlayServices);
        FirebaseCheckDependencies();
        if (!PlayerPrefs.HasKey("sounds")) {
            PlayerPrefs.SetInt("sounds", 1);
        }
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

    void FirebaseCheckDependencies() {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available) {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                _app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            } else {
                Debug.LogError(System.String.Format(
                    "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }
}
