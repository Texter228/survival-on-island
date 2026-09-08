using UnityEngine;

public class createtable : MonoBehaviour
{
    public Invantory inv;
    public int havetype1;
    public int havetype2;

    public int type1, type2, needtype1, needtype2;

    public int count1;
    public int count2;

    public GameObject createobj;

    public Transform player;

    public bool createcordinate;

    public Transform positioncreate;

    public GameObject Base;

    public bool iscreatedbase;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Invantory>();
    }


    void Update()
    {

    }

    public void createstaff()
    {
        havetype1 = 0;
        havetype2 = 0;
        count1 = -1;
        count2 = -1;

        // Проходим по всему инвентарю и проверяем наличие нужных предметов
        for (int i = 0; i < inv.check.Length; i++)
        {
            if (inv.check[i] != null)
            {
                if (inv.check[i].type == type1)
                {
                    havetype1 = inv.check[i].count;
                    count1 = i;
                }
                if (inv.check[i].type == type2)
                {
                    havetype2 = inv.check[i].count;
                    count2 = i;
                }
            }
        }

        // Проверяем, достаточно ли ресурсов для создания
        if (havetype1 >= needtype1 && havetype2 >= needtype2 && count1 != -1 && count2 != -1)
        {
            if (!createcordinate)
            {
                Instantiate(createobj, player.position, createobj.transform.rotation);
            }
            else
            {
                Base.active = true;
                iscreatedbase = true;

            }

            inv.check[count1].count -= needtype1;
            inv.check[count2].count -= needtype2;

            if (inv.check[count1].count <= 0)
            {
                inv.check[count1].Destroyitem();
            }

            if (inv.check[count2].count <= 0)
            {
                inv.check[count2].Destroyitem();
            }

            // Если создаём на определённых координатах, уничтожаем этот объект
            if (createcordinate)
            {
                Destroy(gameObject);
            }
        }
    }




}
