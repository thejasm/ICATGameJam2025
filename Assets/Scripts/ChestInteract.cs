using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteract : MonoBehaviour {
    public CardSelectionScreen cardSelectionScreen;
    public string playerTag = "Player";

    private bool chestOpened = false;


    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag(playerTag) && !chestOpened) {
            Debug.Log("OnTriggerEnter called - Player collided with chest!");
            OpenChest();
        }
    }

    void OpenChest() {
        chestOpened = true; 
        if(gameObject != null) {
            gameObject.SetActive(false); 
        }

        if(cardSelectionScreen != null) {
            cardSelectionScreen.DisplayCards(); 
        } else {
            Debug.LogError("CardSelectionScreen not assigned to the chest!");
        }
    }
}