using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CardSelectionScreen: MonoBehaviour {
    public statController playerStats;
    public Transform cardDisplayParent;
    public List<Card> availableCards;
    public int numberOfCardsToDisplay = 3;

    public InputActionAsset cardSelectionActions;
    private InputAction selectAction;

    void Awake() {
        selectAction = cardSelectionActions.FindActionMap("UI").FindAction("Click");
        if(selectAction == null) {
            Debug.LogError("Click action not found in the Input Action Asset.  Check Action Map and Action name.");
        }
        selectAction.Enable();
    }

    private List<Card> displayedCards = new List<Card>();

    void Start() {
        if(availableCards.Count < numberOfCardsToDisplay) {
            Debug.LogError("Not enough cards in the availableCards list!");
            return;
        }

        DisplayCards();
    }

    public void DisplayCards() {
        foreach(Transform child in cardDisplayParent) {
            Destroy(child.gameObject);
        }
        displayedCards.Clear();

        // Shuffle cards (same as before)
        for(int i = 0; i < availableCards.Count; i++) {
            int j = Random.Range(0, i + 1);
            Card temp = availableCards[i];
            availableCards[i] = availableCards[j];
            availableCards[j] = temp;
        }

        for(int i = 0; i < numberOfCardsToDisplay; i++) {
            Card cardToDisplay = availableCards[i];
            displayedCards.Add(cardToDisplay);

            GameObject cardDisplay = new GameObject("CardDisplay");
            cardDisplay.transform.SetParent(cardDisplayParent, false);

            Image cardImage = cardDisplay.AddComponent<Image>();
            cardImage.sprite = cardToDisplay.cardImage;
            cardImage.preserveAspect = true;

            Button cardButton = cardDisplay.AddComponent<Button>();
            cardButton.transform.localScale *= new Vector2(2, 2);

            // Correctly capture the index:
            int cardIndex = i; // Capture 'i' in a local variable
            cardButton.onClick.AddListener(() => OnCardSelected(displayedCards[cardIndex]));
        }


        gameObject.SetActive(true);
    }

    void OnCardSelected(Card selectedCard) {
        playerStats.ApplyCard(selectedCard);

        gameObject.SetActive(false);

        availableCards.Remove(selectedCard);
        displayedCards.Remove(selectedCard);
    }


    void OnEnable() {
        if(selectAction != null) {
            selectAction.Enable();
        }
    }

    void OnDisable() {
        if(selectAction != null) {
            selectAction.Disable();
        }
    }
}