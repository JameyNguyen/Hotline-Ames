using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class handret : MonoBehaviour
{
    private Card[] currhand;
    private EventManager manager;
    public Text handOut;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<EventManager>();
    }

    void Update()
    {

    }

    
}
