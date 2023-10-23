using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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
    public GameObject winScreen;
    public Text handtext;
    public double multipler;

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

                    Card randCard = deck[UnityEngine.Random.Range(0, deck.Count)];
                    randCard.gameObject.SetActive(true);
                    randCard.transform.position = cardSlots[i].position;
                    randCard.index = i;
                    community[randCard.index] = randCard;
                    availableCards[i] = false;
                    deck.Remove(randCard);

                }
            }
        }
        handtext.text = checkHand(community);
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

    public string checkHand(Card[] checkHand)
    {
        if (check_straight_flush(checkHand))
        {
            multipler = 10;
            return "straight flush";
        }
        else if (check_four_of_a_kind(checkHand))
        {
            multipler = 5;
            return "four of a kind";
        }
        else if (check_full_house(checkHand))
        {
            multipler = 4;
            return "full house";
        }
        else if (check_flush(checkHand))
        {
            multipler = 3.5;
            return "Flush";
        }
        else if (check_straight(checkHand))
        {
            multipler = 3;
            return "straight";
        }
        else if (check_three_of_a_kind(checkHand))
        {
            multipler = 2;
            return "three of a kind";
        }
        else if (check_two_pairs(checkHand))
        {
            multipler = 1.5;
            return "two pairs";
        }
        else if (check_one_pairs(checkHand))
        {
            multipler = 1.25;
            return "one pair";
        }
        else
        {
            multipler = 1;
            return  "high card";
        }
    }


    private bool check_straight_flush(Card[] str8FlHand)
    {
        if (check_flush(str8FlHand) && check_straight(str8FlHand))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    private bool check_four_of_a_kind(Card[] hand)
    {
        Dictionary<int, int> valTally = new Dictionary<int, int>();
        foreach (Card c in hand)
        {
            if (valTally.ContainsKey(c.value))
            {
                valTally[c.value] += 1;
            }
            else
            {
                valTally[c.value] = 1;
            }
        }
        if (valTally.ContainsValue(4))
        {
            return true;
        }
        return false;
    }


    private bool check_full_house(Card[] hand)
    {
        Dictionary<int, int> valTally = new Dictionary<int, int>();
        foreach (Card c in hand)
        {
            if (valTally.ContainsKey(c.value))
            {
                valTally[c.value] += 1;
            }
            else
            {
                valTally[c.value] = 1;
            }
        }
        if (valTally.ContainsValue(3) && valTally.ContainsValue(2))
        {
            return true;
        }
        return false;
    }


    private bool check_flush(Card[] hand)
    {
        Dictionary<char, int> valTally = new Dictionary<char, int>();
        foreach (Card c in hand)
        {
            if (valTally.ContainsKey(c.suit))
            {
                valTally[c.suit] += 1;
            }
            else
            {
                valTally[c.suit] = 1;
            }
        }
        if (valTally.ContainsValue(5))
        {
            return true;
        }
        return false;
    }


    private bool check_straight(Card[] hand)
    {
        HashSet<int> vals = new HashSet<int>();
        int ma = 0;
        int mi = 15;
        int temp;
        foreach (Card c in hand)
        {
            if (vals.Contains(c.value))
            {
                return false;
            }
            temp = c.value;
            vals.Add(temp);
            ma = Math.Max(ma, temp);
            mi = Math.Min(mi, temp);
        }
        if (ma - mi == 4)
        {
            return true;
        }
        if (vals.Contains(2) && vals.Contains(3) && vals.Contains(4) && vals.Contains(5) && vals.Contains(14))
        {
            return true;
        }
        return false;
    }


    private bool check_three_of_a_kind(Card[] hand)
    {
        Dictionary<int, int> valTally = new Dictionary<int, int>();
        foreach (Card c in hand)
        {
            if (valTally.ContainsKey(c.value))
            {
                valTally[c.value] += 1;
            }
            else
            {
                valTally[c.value] = 1;
            }
        }
        if (valTally.ContainsValue(3))
        {
            return true;
        }
        return false;
    }


    private bool check_two_pairs(Card[] hand)
    {
        Dictionary<int, int> valTally = new Dictionary<int, int>();
        foreach (Card c in hand)
        {
            if (valTally.ContainsKey(c.value))
            {
                valTally[c.value] += 1;
            }
            else
            {
                valTally[c.value] = 1;
            }
        }
        int cnt = 0;
        foreach (KeyValuePair<int, int> entry in valTally)
        {
            if (entry.Value == 2)
            {
                cnt += 1;
            }
        }
        return cnt == 2;
    }


    private bool check_one_pairs(Card[] hand)
    {
        Dictionary<int, int> valTally = new Dictionary<int, int>();

        foreach (Card c in hand)
        {
            if (valTally.ContainsKey(c.value))
            {
                valTally[c.value] += 1;
            }
            else
            {
                valTally[c.value] = 1;
            }
        }
        if (valTally.ContainsValue(2))
        {
            return true;
        }
        return false;
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
        enemy.takeDamage(highestVal * multipler);
        player.gainActionPoint();
        state = GameState.ENEMYTURN;
    }

    private void Start()
    {
        enemy = FindObjectOfType<Unit>();
        player = FindObjectOfType<Player>();
    }
}
