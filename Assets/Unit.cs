using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    double maxHealth = 150;
    double health = 150;
    public Text text;
    public Text lastActionText;
    Player player;
    EventManager manager;

    public void takeDamage(double damage)
    {
        // maybe either here or some other object calls this function based on card conditionls
        health -= damage;
        if (health <= 0)
        {
            manager.winScreen.SetActive(true);
        }

    }

    public double getHealth()
    {
        return health;
    }

    public void attackPlayer()
    {
        double highestVal = 0;
        foreach (Card card in manager.community)
        {
            if (card.value >= highestVal)
            {
                highestVal = card.value;
            }
        }
        player.takeDamage(highestVal);
        manager.changeState(GameState.PLAYERTURN);
    }

    public void healSelf()
    {
        double highestVal = 0;
        foreach (Card card in manager.community)
        {
            if (card.value >= highestVal)
            {
                highestVal = card.value;
            }
        }
        health += highestVal;
        // can't let enemy go above max hp lol
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        manager.changeState(GameState.PLAYERTURN);
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        manager = FindObjectOfType<EventManager>();
    }
    private void Update()
    {
        text.text = getHealth().ToString();

        if (manager.getState() == GameState.ENEMYTURN)
        {
            int randomChoice = Random.Range(0, 2);
            if (randomChoice == 0)
            {
                lastActionText.text = "I Attacked";
                attackPlayer();
            }
            if (randomChoice == 1)
            {
                lastActionText.text = "I Healed";
                healSelf();
            }
        }
    }
}
