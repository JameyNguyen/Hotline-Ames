using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public Transform[] cardSlots;
    public bool[] availableCards;

    public void drawCard()
    { 
        if (deck.Count >= 1)
        {
            Card randCard = deck[Random.Range(0, deck.Count)];

            for (int i = 0; i < availableCards.Length; i++)
            {
                if (availableCards[i] == true)
                {
                    randCard.gameObject.SetActive(true);
                    randCard.transform.position = cardSlots[i].position;
                    availableCards[i] = false;
                    deck.Remove(randCard);
                    return;
                }
            }
        }
    }
}
