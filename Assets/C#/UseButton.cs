using UnityEngine;

public class UseButton : MonoBehaviour
{
    [SerializeField] private bool useeat, usebase;
    [SerializeField] private Invantory inv;
    [SerializeField] private hotbar Hotbar;
    private GameObject Base;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Base == null)
        {
            Base = GameObject.FindGameObjectWithTag("Base");
        }

        if (inv.check[inv.ChangeSlot] != null)
        {
            if (inv.check[inv.ChangeSlot].caneat)
            {
                useeat = true;
            }
            else
            {
                useeat = false;
            }
        }
        else if(inv.check[inv.ChangeSlot] == null)
        {
            useeat = false;
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            Use();
        }
    }

    public void eat()
    {
        if (useeat)
        {
            Hotbar.regenerateeat(inv.check[inv.ChangeSlot].regenerateeat);
            Hotbar.HealthHp(inv.check[inv.ChangeSlot].heath);
            inv.check[inv.ChangeSlot].count--;
        }
    }

    public void Use()
    {
        if (useeat && !usebase)
        {
            eat();
        }

        if (usebase)
        {
            Base.GetComponent<Base>().Interactable();
        }
    }

    public void SetUseBase(bool sebase)
    {
        usebase = sebase;
    }
}
