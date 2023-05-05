using UnityEngine;

public class MemoryCard : MonoBehaviour {
    [SerializeField] private SceneController sceneController;
    [SerializeField] private GameObject cardBack;
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioClip cardRevealSound;

    private int _id;

    public int Id {
        get { return _id; }
    }
    private void OnMouseDown() {
        if (cardBack.activeSelf && sceneController.CanReveal) {
            cardBack.SetActive(false);
            if (PlayerPrefs.GetInt("sounds") == 1) {
                Debug.Log("Playerprefs value" + PlayerPrefs.GetInt("sounds"));
                soundSource.PlayOneShot(cardRevealSound);
            }   
            sceneController.CardRevealed(this);
        }
    }

    public void Unreveal() {
        cardBack.SetActive(true);
    }

    public void SetCard(int id, Sprite image) {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }
}