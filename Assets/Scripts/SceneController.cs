using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SceneController : MonoBehaviour {
    public const int gridRows = 3;
    public const int gridCols = 3;
    public const float offsetX = 3f;
    public const float offsetY =4f;

    public Level level = Level.Easy;

    private MemoryCard _firstRevealed;
    private MemoryCard _secondRevealed;
    private int turns;
    public enum Level {
        Easy,
        Medium,
        Hard
    }
    
    [SerializeField] private MemoryCard originalCard;
    [SerializeField] private Sprite[] images;
    [SerializeField] private TMP_Text turnsValue;
    
    void Start() {
        turns = 0;
        Vector3 startPos = originalCard.transform.position;
        int[] easyNumbers = { 0, 0, 1, 1, 2, 2, 3, 3 };

        int[] numbers = ShuffleArray(easyNumbers);

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
                }
            }
        }
    }

    private int[] ShuffleArray(int[] numbers) {
        int[] newArray = numbers.Clone() as int[];
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

        turns++;
        turnsValue.text = turns.ToString();
        _firstRevealed = null;
        _secondRevealed = null;
    }
}
