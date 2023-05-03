using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SceneController : MonoBehaviour {
    public int gridRows = 3;
    public int gridCols = 3;
    public float offsetX = 3f;
    public float offsetY =4f;

    public Level level = Level.Easy;

    private MemoryCard _firstRevealed;
    private MemoryCard _secondRevealed;
    private int turns;
    private int cardsLeft;
    public enum Level {
        Easy,
        Medium,
        Hard
    }

    [SerializeField] private GameObject cardDeck;
    [SerializeField] private MemoryCard originalCard;
    [SerializeField] private Sprite[] images;
    [SerializeField] private TMP_Text turnsValue;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject pausedPanel;
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private AdController adController;

    private LeaderBoardController _leaderBoardController;

    void Start() {
        startPanel.SetActive(true);
        endGamePanel.SetActive(false);
        pausedPanel.SetActive(false);
        pauseButton.SetActive(false);
    }

    public void StartGame() {
        turns = 0;
        startPanel.SetActive(false);
        pauseButton.SetActive(true);
        cardDeck.SetActive(true);
        Vector3 startPos = originalCard.transform.position;
        

        int[] numbers = ShuffleArray();
        cardsLeft = numbers.Length;

        for (int i = 0; i < gridCols; i++) {
            for (int j = 0; j < gridRows; j++) {
                MemoryCard card;
                if (level == Level.Easy) {
                    if (i == 0 && j == 0) {
                        card = originalCard;
                    }
                    else {
                        card = Instantiate(originalCard);
                    }

                    if (i == (gridCols - 1) / 2 && j == (gridRows - 1) / 2) {
                        card.SetCard(4, images[4]);
                        GameObject back = card.transform.GetChild(0).gameObject;
                        back.SetActive(false);
                    } else {
                        int index = j * gridCols + i;
                        if (index < 5) {
                            int id = numbers[index];
                            card.SetCard(id, images[id]);
                        }
                        else {
                            int id = numbers[index-1];
                            card.SetCard(id, images[id]);
                        }
                    }

                    float posX = (offsetX * i) + startPos.x;
                    float posY = -(offsetY * j) + startPos.y;
                    card.transform.SetParent(originalCard.transform.parent);
                    card.transform.position = new Vector3(posX, posY, startPos.z);
                } else if (level == Level.Medium) {
                    if (i == 0 && j == 0) {
                        card = originalCard;
                    }
                    else {
                        card = Instantiate(originalCard);
                    }

                    
                    int index = j * gridCols + i;
                    int id = numbers[index];
                    card.SetCard(id, images[id]);

                    float posX = (offsetX * i) + startPos.x;
                    float posY = -(offsetY * j) + startPos.y;
                    card.transform.SetParent(originalCard.transform.parent);
                    card.transform.position = new Vector3(posX, posY, startPos.z);
                    
                } else if (level == Level.Hard) {
                    if (i == 0 && j == 0) {
                        card = originalCard;
                    }
                    else {
                        card = Instantiate(originalCard);
                    }

                    if (i == (gridCols - 1) / 2 && j == (gridRows - 1) / 2) {
                        card.SetCard(12, images[12]);
                        GameObject back = card.transform.GetChild(0).gameObject;
                        back.SetActive(false);
                    } else {
                        int index = j * gridCols + i;
                        if (index < 13) {
                            int id = numbers[index];
                            card.SetCard(id, images[id]);
                        }
                        else {
                            int id = numbers[index-1];
                            card.SetCard(id, images[id]);
                        }
                    }

                    float posX = (offsetX * i) + startPos.x;
                    float posY = -(offsetY * j) + startPos.y;
                    card.transform.SetParent(originalCard.transform.parent);
                    card.transform.position = new Vector3(posX, posY, startPos.z);
                }
            }
        }
    }

    private int[] ShuffleArray() {
        int[] easyNumbers = { 0, 0, 1, 1, 2, 2, 3, 3 };
        int[] medNumbers = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        int[] hardNumbers = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11 };
        int[] newArray;
        if (level == Level.Easy) {
            newArray = easyNumbers.Clone() as int[];
        } else if (this.level == Level.Medium) {
            newArray = medNumbers.Clone() as int[];
        }
        else {
            newArray = hardNumbers.Clone() as int[];
        }
        
        for (int i = 0; i < newArray.Length; i++) {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }

        return newArray;
    }

    public bool canReveal {
        get { return _secondRevealed == null; }
    }

    public void CardRevealed(MemoryCard card) {
        if (_firstRevealed == null) {
            _firstRevealed = card;
        }
        else {
            _secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch() {
        if (_firstRevealed.Id != _secondRevealed.Id) {
            yield return new WaitForSeconds(0.5f);
            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }
        else {
            cardsLeft -= 2;
        }

        turns++;
        turnsValue.text = turns.ToString();
        _firstRevealed = null;
        _secondRevealed = null;
        if (cardsLeft == 0) {
            GameOver();
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void GameOver() {
        endGamePanel.SetActive(true);
        if (PlayerPrefs.GetInt("PlayAuthenticated") == 1) {
            Social.ReportProgress(_leaderBoardController.GetAchievementID(level), 100f, (bool success) => {
                // handle success or failure
            });
            Social.ReportScore(turns, _leaderBoardController.GetLeaderboardID(level), (bool success) => {
                // handle success or failure
            });
        }

        int rand = Random.Range(0, 2);
        if (rand == 1) {
            adController.ShowInterstitialAd();
        }
        Debug.Log(rand.ToString());

    }

    public void backToMenu() {
        SceneManager.LoadScene("MenuScene");
    }

    public void PauseGame() {
        pausedPanel.SetActive(true);
        cardDeck.SetActive(false);
    }
    public void UnPause() {
        pausedPanel.SetActive(false);
        cardDeck.SetActive(true);
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
