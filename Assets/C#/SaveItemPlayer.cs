using NUnit;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class SaveItemPlayer : MonoBehaviour
{
    public GameObject[] ListObj;

    public Transform player;

    public Invantory inv;
    public int[] type;
    public int[] haveresource;
    public int[] b1;

    private bool start = true;

    [SerializeField] private bool[] isloaded;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Invantory>();
    }

    void Update()
    {
        if (start)
        {
            Load();
            start = false;
        }

        Check();
        Save();
    }

    public void Check()
    {

        // Присвоение значений на основе типа ресурса
        for (int i = 0; i < type.Length; i++)
        {
            bool found = false;
            for (int j = 0; j < inv.check.Length; j++)
            {
                if (inv.check[j] != null && inv.check[j].type == type[i])
                {
                    haveresource[i] = inv.check[j].count; // Количество найденного ресурса
                    b1[i] = j; // Индекс найденного элемента в массиве inv.check
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                haveresource[i] = 0; // Ресурс не найден, устанавливаем значение в 0
                b1[i] = -1; // Обозначаем отсутствие элемента
            }
        }

        // Проверка на уникальность типов ресурсов
        HashSet<int> seenTypes = new HashSet<int>();
        for (int i = 0; i < type.Length; i++)
        {
            if (!seenTypes.Add(type[i])) // Если тип уже был добавлен, значит он не уникален
            {
                haveresource[i] = 0; // Обнуляем количество ресурса
            }
        }
    }

    public void Load()
    {
        for (int i = 0; i < haveresource.Length; i++)
        {
            if(PlayerPrefs.HasKey("haveresource: " + i))
            {
                haveresource[i] = PlayerPrefs.GetInt("haveresource: " + i);
                
                for(int j = 0;j < haveresource[i]; j++)
                {
                    if (!isloaded[i])
                    {
                        Instantiate(ListObj[i], player.position, player.rotation);
                        if(j > haveresource[i])
                        {
                            isloaded[i] = true;
                        }
                    }
                }
            }
        }
    }

    public void Save()
    {
        for (int i = 0; i < haveresource.Length; i++)
        {
            PlayerPrefs.SetInt("haveresource: " + i, haveresource[i]);
        }
    }
}
