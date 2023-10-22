using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    public char suit;
    public int value;
    public int index;
    private bool selected;
    private EventManager manager;

    private void OnMouseDown()
    {
        if (!selected)
        {
            selected = true;
            manager.selectCard(this);
        }

        else
        {
            selected = false;
            manager.removeCard(this);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<EventManager>();
        selected = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
