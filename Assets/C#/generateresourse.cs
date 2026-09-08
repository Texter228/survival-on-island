using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateresourse : MonoBehaviour
{
    public GameObject tree;
    public Transform[] points;
    public bool[] isCreated;
    public GameObject[] create;

    public float timeToCreate;
    public float timeRemaining;

    public int isstart;

    public int[] saveobj;

    public item[] items;

    public bool isstuk;

   
    void Start()
    {
        saveobj = new int[points.Length];

        for (int i = 0; i < points.Length; i++)
        {
            if (PlayerPrefs.HasKey("saveobj: " + i))
            {
                int index = PlayerPrefs.GetInt("saveobj: " + i);

                // Проверяем, что индекс находится в пределах всех массивов
                if (index >= 0 && index < create.Length && index < points.Length && index < isCreated.Length)
                {
                    saveobj[i] = index;
                    create[index] = Instantiate(tree, points[index].position, points[index].rotation);
                    if(isstuk)
                        items[index] = create[index].GetComponent<item>();
                    isCreated[index] = true;
                }
            }
        }


        if (PlayerPrefs.HasKey("isstart"))
        {
            PlayerPrefs.GetInt("isstart", isstart);
        }
        else
        {
            isstart = 1;
        }

        if (isstart == 1)
        {
            CreateAllObj();
        }
    }

    void Update()
    {
        PlayerPrefs.SetInt("isstart", isstart);
        PlayerPrefs.Save();
        if (points == null || create == null || points.Length == 0 || create.Length == 0)
            return;

        if (timeRemaining <= 0)
        {
            for (int i = 0; i < points.Length; i++)
            {
                if (!isCreated[i])
                {
                    create[i] = Instantiate(tree, points[i].position, points[i].rotation);
                    items[i] = create[i].GetComponent<item>();
                    isCreated[i] = true;
                    timeRemaining = timeToCreate;

                    saveobj[i] = i;

                    PlayerPrefs.SetInt("saveobj: " + i, saveobj[i]);
                    PlayerPrefs.Save();

                    break;
                }
            }
        }
        else
        {
            timeRemaining -= Time.deltaTime;
        }

        for (int i = 0; i < points.Length; i++)
        {
            if (PlayerPrefs.HasKey("saveobj: " + i))
            {
                int index = PlayerPrefs.GetInt("saveobj: " + i);
                if (index >= 0 && index < create.Length)
                {
                    if (create[index] == null)
                    {
                        saveobj[i] = -1;
                        PlayerPrefs.SetInt("saveobj: " + i, saveobj[i]);
                        PlayerPrefs.Save();
                    }
                }
            }
        }


        CheckTree();
    }

    public void CheckTree()
    {
        if (!isstuk)
        {
            for (int i = 0; i < points.Length; i++)
            {
                if (create[i] == null)
                {
                    isCreated[i] = false;
                }
            }
        }
        else
        {
            for (int i = 0; i < points.Length; i++)
            {
                if (create[i] == null)
                {
                    isCreated[i] = false;
                }
                isCreated[i] = false;
                create[i] = null;
                items[i] = null;
            }

        }
    }

    public void CreateAllObj()
    {
        for (int i = 0; i < points.Length; i++)
        {
            create[i] = Instantiate(tree, points[i].position, points[i].rotation);
            isCreated[i] = true;
            saveobj[i] = i;
            PlayerPrefs.SetInt("saveobj: " + i, saveobj[i]);
            PlayerPrefs.Save();
        }
    }
}
