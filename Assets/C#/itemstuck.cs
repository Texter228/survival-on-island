using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemstuck : MonoBehaviour
{
    public int countstuck;
    public Text text;
    public item itm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        countstuck = itm.count;
        text.text = " " + countstuck;
    }
}
