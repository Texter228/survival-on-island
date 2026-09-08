using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    public GameObject UIUpgrade;

    public GameObject Text;

    public bool ischeck;

    public bool dubleclick;

    public int lvlbase, lvlTree, lvlRuck, lvlIsland;

    public Button ButtonTreeUpgrade, ButtonRockUpgrade, ButtonIslandUpgrade, ButtonBaseUpgrade;

    public Text lvlTreeimg, lvlRockimg, lvlislandimg, lvlbaseimg;

    public Invantory inv;
    public int[] type;
    public int[] haveresource;
    public int[] b1;


    public int[] needresoursetree;
    public int[] needresourserock;
    public int[] needresoursecloth;

    public Transform inventory;

    public Sprite imglvlbase2, imglvlbase3;
    public SpriteRenderer house;

    public Text txttreeupgrade;
    public Text txtrockupgrade;

    public Text txtrockupgradeisland;
    public Text txttreeupgradeisland;

    public Text txtrockupgradebase;
    public Text txttreeupgradebase;
    public Text txtclothupgradebase;

    public generateresourse genereterock, generetetree;


    public GameObject r1, r2, r3, r4;

    public upgradeislandd upgradeisland;

    public createtable Createtable;

    [SerializeField] private UseButton useButton;

    void Start()
    {
        Load();
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Invantory>();
        upgradeisland = GetComponent<upgradeislandd>();
    }

    void Update()
    {
        Save();


        txtrockupgrade.text = "" + needresourserock[0];

        txttreeupgrade.text = "" + needresoursetree[0];

        txtrockupgradebase.text = "" + needresourserock[1];
        txtclothupgradebase.text = "" + needresoursecloth[0];
        txttreeupgradebase.text = "" + needresoursetree[1];

        txtrockupgradeisland.text = "" + needresourserock[2];
        txttreeupgradeisland.text = "" + needresoursetree[2];

        CheckResource();

        if (ischeck)
        {

            if (Input.GetKeyDown(KeyCode.C))
            {
                dubleclick = !dubleclick;
            }

            if (dubleclick)
            {
                UIUpgrade.active = true;
            }

            if (!dubleclick)
            {
                UIUpgrade.active = false;
            }
        }

        if (lvlbase == 0)
        {
            if (lvlTree < 1)
            {
                ButtonTreeUpgrade.interactable = true;
            }
            else
            {
                ButtonTreeUpgrade.interactable = false;
            }
            ButtonRockUpgrade.interactable = false;
            ButtonIslandUpgrade.interactable = false;
        }

        if (lvlbase == 1)
        {
            if (lvlTree < 2)
            {
                ButtonTreeUpgrade.interactable = true;
            }
            else
            {
                ButtonTreeUpgrade.interactable = false;
            }

            if (lvlRuck < 1)
            {
                ButtonRockUpgrade.interactable = true;
            }
            else
            {
                ButtonRockUpgrade.interactable = false;
            }
            ButtonIslandUpgrade.interactable = false;

        }

        if (lvlbase == 2)
        {
            if (lvlTree < 3)
            {
                ButtonTreeUpgrade.interactable = true;
            }
            else
            {
                ButtonTreeUpgrade.interactable = false;
            }

            if (lvlRuck < 2)
            {
                ButtonRockUpgrade.interactable = true;
            }
            else
            {
                ButtonRockUpgrade.interactable = false;
            }

            if (lvlIsland < 2)
            {
                ButtonIslandUpgrade.interactable = true;
            }
            else
            {
                ButtonIslandUpgrade.interactable = false;
            }

            house.sprite = imglvlbase2;
        }

        if (lvlbase == 3)
        {
            if (lvlTree < 3)
            {
                ButtonTreeUpgrade.interactable = true;
            }
            else
            {
                ButtonTreeUpgrade.interactable = false;
            }

            if (lvlRuck < 3)
            {
                ButtonRockUpgrade.interactable = true;
            }
            else
            {
                ButtonRockUpgrade.interactable = false;
            }

            if (lvlIsland < 3)
            {
                ButtonIslandUpgrade.interactable = true;
            }
            else
            {
                ButtonIslandUpgrade.interactable = false;
            }

            house.sprite = imglvlbase3;
        }

        if (lvlTree == 0)
        {
            lvlTreeimg.text = "UpGrade 0 LVL";
        }

        if (lvlTree == 1)
        {
            lvlTreeimg.text = "UpGrade 1 LVL";
        }
        if (lvlTree == 2)
        {
            lvlTreeimg.text = "UpGrade 2 LVL";
        }
        if (lvlTree == 3)
        {
            lvlTreeimg.text = "Max lvl";
            Destroy(r1);
        }

        if (lvlRuck == 0)
        {
            lvlRockimg.text = "UpGrade 0 LVL";
        }

        if (lvlRuck == 1)
        {
            lvlRockimg.text = "UpGrade 1 LVL";
        }
        if (lvlRuck == 2)
        {
            lvlRockimg.text = "UpGrade 2 LVL";
        }
        if (lvlRuck == 3)
        {
            lvlRockimg.text = "Max lvl";
            Destroy(r2);
        }

        if (lvlIsland == 0)
        {
            lvlislandimg.text = "UpGrade 0 LVL";
        }

        if (lvlIsland == 1)
        {
            lvlislandimg.text = "UpGrade 1 LVL";
        }
        if (lvlIsland == 2)
        {
            lvlislandimg.text = "UpGrade 2 LVL";
        }
        if (lvlIsland == 3)
        {
            lvlislandimg.text = "Max lvl";
            Destroy(r3);
        }

        if (lvlbase == 0)
        {
            lvlbaseimg.text = "UpGrade 0 LVL";
        }

        if (lvlbase == 1)
        {
            lvlbaseimg.text = "UpGrade 1 LVL";
        }
        if (lvlbase == 2)
        {
            lvlbaseimg.text = "UpGrade 2 LVL";
        }
        if (lvlbase == 3)
        {
            lvlbaseimg.text = "Max lvl";
            Destroy(r4);
        }


    }



    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Text.active = true;
            ischeck = true;
            useButton.SetUseBase(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Text.active = false;
            ischeck = false;
            UIUpgrade.active = false;
            dubleclick = false;
            useButton.SetUseBase(false);
        }
    }

    public void BaseUpgrade()
    {
        if(needresoursetree[1] <= haveresource[0] && needresourserock[1] <= haveresource[1] && needresoursecloth[0] <= haveresource[2])
        {
            inv.check[b1[0]].count -= needresoursetree[1];
            inv.check[b1[1]].count -= needresourserock[1];
            inv.check[b1[2]].count -= needresoursecloth[0];
            if (lvlbase < 2)
            {
                lvlbase++;
                if(lvlbase == 1)
                {
                    needresoursetree[1] += 50;
                    needresourserock[1] += 35;
                    needresoursecloth[0] += 10;
                }

                if (lvlbase == 2)
                {
                    needresoursetree[1] += 90;
                    needresourserock[1] += 75;
                    needresoursecloth[0] += 15;
                }

                if (lvlbase == 3)
                {
                    needresoursetree[1] += 150;
                    needresourserock[1] += 100;
                    needresoursecloth[0] += 30;
                }
            }
            else
            {
                lvlbase++;
                ButtonBaseUpgrade.interactable = false;
            }
        }
    }

    public void TreeUpgrade()
    {
        if (needresoursetree[0] <= haveresource[0])
        {
            lvlTree++;
            generetetree.timeToCreate -= 5;
            inv.check[b1[0]].count -= needresoursetree[0];
            if(lvlTree == 1)
            {
                needresoursetree[0] += 40;
                txttreeupgrade.text = "" + needresoursetree[0];
            }
            if (lvlTree == 2)
            {
                needresoursetree[0] += 80;
                txttreeupgrade.text = "" + needresoursetree[0];
            }
            if (lvlTree == 3)
            {
                needresoursetree[0] += 160;
                txttreeupgrade.text = "" + needresoursetree[0];
            }
        }
    }

    public void RucKUpgrade()
    {
        if (needresourserock[0] <= haveresource[1])
        {
            lvlRuck++;
            genereterock.timeToCreate -= 5;
            inv.check[b1[1]].count -= needresourserock[0];
            if (lvlRuck == 1)
            {
                needresourserock[0] += 10;
            }
            if (lvlRuck == 2)
            {
                needresourserock[0] += 40;
            }
            if (lvlRuck == 3)
            {
                needresourserock[0] += 80;
            }
        }
    }

    public void IslandUpgrade()
    {
        if(needresoursetree[2] <= haveresource[0] && needresourserock[2] <= haveresource[1])
        {
            if(lvlTree < 2)
            {
                lvlIsland++;
            }

            if(lvlIsland == 1)
            {
                needresourserock[2] += 70;
                needresoursetree[2] += 100;
                upgradeislandd.startupgrade1 = true;
            }
            if (lvlIsland == 2)
            {
                needresourserock[2] += 210;
                needresoursetree[2] += 350;
            }
            if (lvlIsland == 3)
            {
                needresourserock[2] += 300;
                needresoursetree[2] += 500;
            }
        }
    }

    public void CheckResource()
    {
        // Проверка инициализации массивов и других объектов
        if (type == null || inv == null || inv.check == null || haveresource == null || b1 == null || needresoursetree == null)
        {
            throw new InvalidOperationException("Массивы или объекты не инициализированы.");
        }

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


    public bool canopen;

    public void Interactable()
    {
        dubleclick = !dubleclick;
    }

    public void Save()
    {
        PlayerPrefs.SetInt("lvlbase", lvlbase);
        PlayerPrefs.SetInt("lvlTree", lvlTree);
        PlayerPrefs.SetInt("lvlRuck", lvlRuck);
        PlayerPrefs.SetInt("lvlIsland", lvlIsland);

        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("lvlbase") && PlayerPrefs.HasKey("lvlTree") && PlayerPrefs.HasKey("lvlRuck") && PlayerPrefs.HasKey("lvlIsland")) {
            lvlbase = PlayerPrefs.GetInt("lvlbase");
            lvlTree = PlayerPrefs.GetInt("lvlTree");
            lvlRuck = PlayerPrefs.GetInt("lvlRuck");
            lvlIsland = PlayerPrefs.GetInt("lvlIsland");
        }
    }
}
