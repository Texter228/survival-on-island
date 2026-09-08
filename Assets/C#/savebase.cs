using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class savebase : MonoBehaviour
{
    public createtable Createtable;

    public int iscreatebase;
    public GameObject Base;
    // Start is called before the first frame update
    void Start()
    {
        Load();

        if (iscreatebase == 1 && Createtable != null)
        {
            Createtable.createcordinate = true;
            Destroy(Createtable.gameObject);
            Base.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Save();
        if (Createtable != null)
        {
            if (Createtable.iscreatedbase && Createtable != null)
            {
                iscreatebase = 1;
                Destroy(Createtable.gameObject);
            }
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt("iscreatebase", iscreatebase);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("iscreatebase"))
        {
            iscreatebase = PlayerPrefs.GetInt("iscreatebase");
        }
    }
}