using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum GameState { START, PLAYERTURN, ENEMYTURN, WON, LOST}
public class EventManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public Transform[] cardSlots;
    public bool[] availableCards;
    public List<Card> selectedCards = new List<Card>();
    public Transform discardSlot;
    public Card[] community = new Card[5];
    public GameObject drawTutorial;
    public GameObject selectTutorial;
    public GameObject discardTutorial;
    public GameObject attackTutorial;
    public GameObject actionPointTutorial;
    public GameObject endingTutorial;

    Unit enemy;
    Player player;

    GameState state;
 
    public void drawCard()
    { 
        if (deck.Count >= 1)
        {
            for (int i = 0; i < availableCards.Length; i++)
            {
                if (availableCards[i] == true)
                {

                    Card randCard = deck[Random.Range(0, deck.Count)];
                    randCard.gameObject.SetActive(true);
                    randCard.transform.position = cardSlots[i].position;
                    randCard.index = i;
                    community[randCard.index] = randCard;
                    availableCards[i] = false;
                    deck.Remove(randCard);

                }
            }
        }
        if (drawTutorial.active)
        {
            drawTutorial.SetActive(false);
            selectTutorial.SetActive(true);
            discardTutorial.SetActive(true);
        }
        return;
    }

    public void discardCard()
    {
        if (selectTutorial.active)
        {
            selectTutorial.SetActive(false);
            discardTutorial.SetActive(false);
            attackTutorial.SetActive(true);
            actionPointTutorial.SetActive(true);
        }
        
        if (selectedCards.Count > 0)
        {
            for (int i = 0; i < selectedCards.Count; i++)
            {
                selectedCards[i].transform.position = discardSlot.position;
                availableCards[selectedCards[i].index] = true;
                community[selectedCards[i].index] = null;
                selectedCards[i].index = -1;
            }
        }
        while (selectedCards.Count > 0)
        {
            selectedCards.RemoveAt(0);
        }
        drawCard();
    }

    public void shuffle()
    {
        for (int i = 0; i <= 50; i++)
        {
            int j = Random.Range(0, 51);
            Card temp = deck[i];
            deck[i] = deck[j];
            deck[j] = temp;
        }
    }

    public void selectCard(Card card)
    {         
        selectedCards.Add(card);
    }

    public void removeCard(Card card)
    {
        selectedCards.Remove(card);
    }

    public GameState getState()
    {
        return state;
    }

    public void changeState(GameState state)
    {
        this.state = state;
    }

    public void attackCard()
    {
        if (attackTutorial.active)
        {
            attackTutorial.SetActive(false);
            actionPointTutorial.SetActive(false);
        }

        int highestVal = 0;
        foreach (Card card in community)
        {
            if (card.value >= highestVal)
            {
                highestVal = card.value;
            }
        }
        if (player.isBuffed())
        {
            highestVal *= 2;
            player.removeBuff();
        }
        enemy.takeDamage(highestVal);
        player.gainActionPoint();
        state = GameState.ENEMYTURN;
    }

    private void Start()
    {
        enemy = FindObjectOfType<Unit>();
        player = FindObjectOfType<Player>();
    }
}
