using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public Transform[] cardSlots;
    public bool[] availableCards;
    public List<Card> selectedCards = new List<Card>();
 
    public void drawCard()
    { 
        if (deck.Count >= 1)
        {
            Card randCard = deck[Random.Range(0, deck.Count)];

            for (int i = 0; i < availableCards.Length; i++)
            {
                 randCard.gameObject.SetActive(true);
                 randCard.transform.position = cardSlots[i].position;
                 availableCards[i] = false;
                 deck.Remove(randCard);
                 return;
            }
        }
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


}
