using System;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderBoardController : MonoBehaviour {
    private const string EasyAchievementID = "CgkIzpDcm7AbEAIQAg";
    private const string MedAchievementID = "CgkIzpDcm7AbEAIQAw";
    private const string HardAchievementID = "CgkIzpDcm7AbEAIQBA";

    private const string EasyLeaderboardID = "CgkIzpDcm7AbEAIQAQ";
    private const string MedLeaderboardID = "CgkIzpDcm7AbEAIQBQ";
    private const string HardLeaderboardID = "CgkIzpDcm7AbEAIQBg";

    private void Awake() {
        if (!Social.localUser.authenticated) {
            Social.localUser.Authenticate((bool success) => {
                if (success) {
                    Social.ShowLeaderboardUI();
                }
            });
        }
    }

    private void Start() {
        PlayGamesPlatform.Activate();
    }

    public void LoadEasyBoard() {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(EasyLeaderboardID);
    }

    public void LoadMediumBoard() {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(MedLeaderboardID);
    }

    public void LoadHardBoard() {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(HardLeaderboardID);
    }

    public string GetAchievementID(SceneController.Level level) {
        if (level == SceneController.Level.Easy) {
            return EasyAchievementID;
        } else if (level == SceneController.Level.Medium) {
            return MedAchievementID;
        }
        else {
            return HardAchievementID;
        }
    }
    
    public string GetLeaderboardID(SceneController.Level level) {
        if (level == SceneController.Level.Easy) {
            return EasyLeaderboardID;
        } else if (level == SceneController.Level.Medium) {
            return MedLeaderboardID;
        }
        else {
            return HardLeaderboardID;
        }
    }

    public void BackToSettings() {
        SceneManager.LoadScene("SettingsScene");
    }
}
