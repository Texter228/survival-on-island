using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class socket : MonoBehaviour
{
    public Slider slider;
    public Image img;
    public int Unbreaking, MaxUnBreaking;

    private int i;
    private int f1, f2, f3;
    private Invantory inv;
    private int yourcount;

    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = MaxUnBreaking;
        Unbreaking = MaxUnBreaking;
        f1 = (Unbreaking / 4) * 3;
        f2 = (Unbreaking / 4) * 2;
        f3 = Unbreaking / 4;
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Invantory>();


        for (i = 0; i < inv.Slots.Length; i++)
        {
            if (inv.isfull[i] == false)
            {
                yourcount = i;
                break;
            }
        }
    }

    void Update()
    {
        slider.value = Unbreaking;
        if (Unbreaking < f1+1 && Unbreaking > f2)
        {
            img.color = Color.green;
        } else if (Unbreaking < f2+1 && Unbreaking > f3 && Unbreaking < f1)
        {
            img.color = Color.yellow;
        }else if (Unbreaking < f3+1)
        {
            img.color = Color.red;
        }

    }

    public void updateUnBreaking(int breaking)
    {
        Unbreaking = breaking;
    }
}
