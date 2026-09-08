using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateIsland : MonoBehaviour
{
    public int horizontal, vertical;
    public int width;
    private int height;

    public GameObject createobj, createobj1, createobj2, createobj3, createobj4, createobj5, createobj6, createobj7, createobj8;

    private GameObject lastCreateHorizontal;
    public GameObject createHorizontal;
    public Transform objThis;

    public bool offleft;
    public bool offright;

    public bool isCreate;
    public float delay;

    public int[] numbertocreate;
    public GameObject[] needcreate;

    public GameObject[] list;

    public bool isgoup;

    public int layer;

    public bool created = false;

    public GameObject create1,create2;

    SpriteRenderer sp;


    void Start()
    {
        height = width;
        vertical = vertical * height;
        horizontal = horizontal * width;
        list = new GameObject[horizontal * vertical + 3];
        objThis = transform;
    }

    void Update()
    {
        if (isCreate)
        {
            StartCoroutine(CreateIslandCoroutine());
            isCreate = false;
            created = false;
        }
    }

    public IEnumerator CreateIslandCoroutine()
    {
        int count = 0;
        Vector3 startPosition = transform.position;

        for (int j = 0; j < vertical; j += height)
        {
            bool nextlvl = true;
            Vector3 rowPosition = startPosition + (isgoup ? new Vector3(0, j, 0) : new Vector3(0, -j, 0));
            lastCreateHorizontal = null;

            for (int i = 0; i < horizontal; i += width)
            {
                Vector3 spawnPosition = rowPosition + new Vector3(i, 0, 0);

                if (count == 0 && !offleft && !isgoup)
                {
                    createHorizontal = Instantiate(createobj6);
                }
                else if (count == 0 && offleft && !isgoup)
                {
                    createHorizontal = Instantiate(createobj);
                }
                else if (count < horizontal && !isgoup)
                {
                    if (count == horizontal - 1 && i == horizontal - 1 && !offleft)
                    {
                        createHorizontal = Instantiate(createobj8);
                    }
                    else if (i == horizontal - 1 && !isgoup)
                    {
                        createHorizontal = Instantiate(createobj);
                    }
                    else
                    {
                        createHorizontal = Instantiate(createobj7);
                    }
                }
                else if (i == horizontal - 1 && count <= horizontal * vertical - 1 && count != (horizontal * vertical) - 1 && !offright && !isgoup)
                {
                    createHorizontal = Instantiate(createobj4);
                }
                else if (count + horizontal < horizontal * vertical && !offleft && !isgoup && nextlvl)
                {
                    createHorizontal = Instantiate(createobj5); //12
                    nextlvl = false;
                }
                else if (count + horizontal == (horizontal * vertical) && !offleft && !isgoup)
                {
                    createHorizontal = Instantiate(createobj1);
                }
                else if (count < horizontal * vertical && count > horizontal * vertical - horizontal && count != (horizontal * vertical) - 1 && !isgoup)
                {
                    createHorizontal = Instantiate(createobj2);
                }
                else if (count == (horizontal * vertical) - 1 && !offright && !isgoup)
                {
                    createHorizontal = Instantiate(createobj3);
                }
                else if(!isgoup)
                {
                    createHorizontal = Instantiate(createobj);
                }
                
                
                
                
                
                
                
                else if (count == 0 && !offleft && isgoup)
                {
                    createHorizontal = Instantiate(createobj1);
                }
                else if (count == 0 && offleft && isgoup)
                {
                    createHorizontal = Instantiate(createobj);
                }
                else if (count < horizontal && isgoup)
                {
                    if (count == horizontal - 1 && i == horizontal - 1 && !offleft && isgoup)
                    {
                        createHorizontal = Instantiate(createobj3);
                    }
                    else if (i == horizontal - 1 && isgoup)
                    {
                        createHorizontal = Instantiate(createobj);
                    }
                    else
                    {
                        createHorizontal = Instantiate(createobj2);
                    }
                }
                else if (i == horizontal - 1 && count <= horizontal * vertical - 1 && count != (horizontal * vertical) - 1 && !offright && isgoup)
                {
                    createHorizontal = Instantiate(createobj4);
                }
                else if (count + horizontal < horizontal * vertical && !offleft && isgoup && nextlvl)
                {
                    createHorizontal = Instantiate(createobj5);
                    nextlvl = false;
                }
                else if (count + horizontal == (horizontal * vertical) && !offleft && isgoup)
                {
                    createHorizontal = Instantiate(createobj6);
                }
                else if (count < horizontal * vertical && count > horizontal * vertical - horizontal && count != (horizontal * vertical) - 1 && isgoup)
                {
                    createHorizontal = Instantiate(createobj7);
                }
                else if (count == (horizontal * vertical) - 1 && !offright && isgoup)
                {
                    createHorizontal = Instantiate(createobj8);
                }
                else
                {
                    createHorizontal = Instantiate(createobj);
                }

                createHorizontal.transform.position = spawnPosition;

                for (int l = 0; l < numbertocreate.Length; l++)
                {
                    if (count == numbertocreate[l])
                    {
                        GameObject g = Instantiate(needcreate[l]);
                        if (g != null)
                        {
                            if(createHorizontal != null)
                            {
                                Destroy(createHorizontal);
                            }

                            g.transform.position = spawnPosition;
                            createHorizontal = g;
                        }
                        SpriteRenderer sp = g.GetComponent<SpriteRenderer>();
                        if (sp != null)
                        {
                            sp.sortingOrder = layer +1;
                        }
                        list[count] = g;
                        break;
                    }
                }

                sp = createHorizontal.GetComponent<SpriteRenderer>();

                if (layer != 0 && sp != null)
                {
                    sp.sortingOrder = layer;
                }

                lastCreateHorizontal = createHorizontal;
                list[count] = createHorizontal;
                count++;
                yield return new WaitForSeconds(delay);
            }
        }
        created = true;

        for(int i = 0; i < count; i++)
        {
            list[i].transform.SetParent(transform);
        }
    }
}
