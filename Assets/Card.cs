using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    char suit;
    char value;
    public int index;

    private EventManager manager;

    private void OnMouseDown()
    {
        manager.selectCard(this);
        manager.availableCards[index] = true;
    }



    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<EventManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
