using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_close_menu : MonoBehaviour
{
    public bool isopen;

    public GameObject obj;
    public GameObject obj1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            isopen = !isopen;
            check();
        }


    }

    public void check()
    {
        if (isopen)
        {
            obj.SetActive(true);
            obj1.SetActive(false);
        }
        else
        {
            obj.SetActive(false);
            obj1.SetActive(true);
        }
    }
}
