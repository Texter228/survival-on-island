using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    [SerializeField] private int damage, durability, giveresurse, food, health;
    [SerializeField] private Slider Damage, Durability, GiveResourse, Food, Health;

    [SerializeField] private Image img;

    [SerializeField] private Sprite imgitem;

    [SerializeField] private string nameitem;
    [SerializeField] private Text Name;

    [SerializeField] private bool istool;

    [SerializeField] private GameObject Tools, Items;

    private bool isclicked;

    [SerializeField] private NameItem nameiteminfo;

    [SerializeField] private float height, wight, rotate;

    [SerializeField] private bool rock, tree, enamy;

    [SerializeField] private Toggle RockToggle, TreeToggle, EnamyToggle;



    [SerializeField] private int crafttree, craftrock, craftcloth;
    [SerializeField] private Text txtcrafttree, txtcraftrock, txtcraftcloth;
    [SerializeField] private GameObject objcrafttree, objcraftrock, objcraftcloth;

    [SerializeField] private GameObject createObj;

    [SerializeField] private createtable btn;



    public void Update()
    {
        if (isclicked)
        {
            if (nameiteminfo != null)
            {
                if (nameiteminfo.GetName() != name)
                {
                    isclicked = false;
                }
            }
        }
    }

    public void ClickButton()
    {
        isclicked = !isclicked;
        if (isclicked)
        {
            if (istool)
            {
                Tools.SetActive(true);
                Items.SetActive(false);

                nameiteminfo = Tools.GetComponent<NameItem>();
                nameiteminfo.SetName(name);

                GameObject objimg = img.gameObject;
                objimg.transform.localScale = new Vector3(wight,height);
                objimg.transform.rotation = Quaternion.Euler(0, 0, rotate);

                Damage.value = damage;
                Durability.value = durability;
                GiveResourse.value = giveresurse;
                Name.text = nameitem;
                img.sprite = imgitem;

                RockToggle.isOn = rock;
                TreeToggle.isOn = tree;
                EnamyToggle.isOn = enamy;

                txtcrafttree.text = "" + crafttree;
                txtcraftrock.text = "" + craftrock;
                txtcraftcloth.text = "" + craftcloth;

                if (crafttree <= 0)
                {
                    objcrafttree.SetActive(false);
                }
                else
                {
                    objcrafttree.SetActive(true);
                }

                if (craftrock <=0)
                {
                    objcraftrock.SetActive(false);
                }
                else
                {
                    objcraftrock.SetActive(true);
                }

                if (craftcloth <= 0)
                {
                    objcraftcloth.SetActive(false);
                }
                else
                {
                    objcraftcloth.SetActive(true);
                }

                btn.needtype1 = crafttree;
                btn.needtype2 = craftrock;
                btn.createobj = createObj;

            }
            else
            {
                Items.SetActive(true);
                Tools.SetActive(false);
                Food.value = food;
                Health.value = health;
            }
        }
        else
        {
            Items.SetActive(false);
            Tools.SetActive(false);
        }
    }
}
