using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int health = 150;
    int currency = 0;
    int actionPoint = 5;
    

    public void gainCurrency(int gold) 
    {
        currency += gold;
    }

    public void takeDamage(int damage)
    {
        // maybe either here or some other object calls this function based on card conditionls
        health -= damage;
        if (health <= 0)
        {
            // gg lol
        }
        
    }
    public void useActionPoints(int point)
    {
        if (actionPoint - point < 0)
        {
            // not enough points to do action
            return;
        }
        actionPoint -= point;
    }

}
