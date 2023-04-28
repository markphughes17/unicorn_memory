using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour {
    [SerializeField] private SceneController sceneController;
    [SerializeField] private GameObject cardBack;

    private int _id;

    public int Id {
        get { return _id; }
    }
    private void OnMouseDown() {
        if (cardBack.activeSelf && sceneController.canReveal) {
            cardBack.SetActive(false);
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