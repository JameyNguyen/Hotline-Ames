using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    double health = 150;
    int currency = 0;
    int actionPoint = 0;
    bool buffed = false;
    public Text text;
    public Text actionPointText;
    EventManager manager;

    public void gainCurrency(int gold) 
    {
        currency += gold;
    }

    public void takeDamage(double damage)
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
        if (actionPoint - point < 0 || buffed)
        {
            // not enough points to do action
            return;
        }
        actionPoint -= point;
        buffed = true;
    }

    public int getActionPoints()
    {
        return actionPoint;
    }
    public bool isBuffed()
    {
        return buffed;
    }

    public void removeBuff()
    {
        buffed = false;
    }
    public void gainActionPoint()
    {
        actionPoint += 1;
    }

    private void Update()
    {
        text.text = health.ToString();
        actionPointText.text = actionPoint.ToString();
    }
}
