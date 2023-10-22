using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    int health = 150;
    public Text text;

    public void takeDamage(int damage)
    {
        // maybe either here or some other object calls this function based on card conditionls
        health -= damage;
        if (health <= 0)
        {
            // gg lol
        }

    }

    public int getHealth()
    {
        return health;
    }

    private void Update()
    {
        text.text = getHealth().ToString();
    }
}
