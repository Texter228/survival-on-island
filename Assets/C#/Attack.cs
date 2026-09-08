using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Attack : MonoBehaviour
{
    public Transform attackpos;
    public float Range;
    public int Damage;
    public int DamageForGun;
    public int DamageforTool;
    public int Damageforcantreaourse;
    public Collider2D[] attack;
    public item item;
    public int giveresurse = 1;

    public bool ishand;

    public bool istool;

    public string[] can;
    public string[] cant;

    // Start is called before the first frame update
    void Start()
    {
        item = GetComponent<item>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackpos.position, Range);
    }

    public void AttackObj()
    {
        attack = Physics2D.OverlapCircleAll(attackpos.position, Range);
        for (int i = 0; i < attack.Length; i++)
        {
            if (attack != null)
            {
                if (!ishand)
                {
                    for (int j = 0; j < can.Length; j++)
                    {
                        if (attack[i].tag == can[j] && istool)
                        {
                            // Проверка наличия касаний
                            if (Input.touchCount > 0 && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                            {
                                item = GetComponent<item>();
                                attack[i].GetComponent<Tree>().BreakTree(Damage, giveresurse);
                                if (!ishand)
                                {
                                    item.UnBreaking -= DamageforTool;
                                }
                            }
                            else if (Input.GetMouseButton(0)) // Для тестирования на ПК
                            {
                                if (!EventSystem.current.IsPointerOverGameObject())
                                {
                                    item = GetComponent<item>();
                                    attack[i].GetComponent<Tree>().BreakTree(Damage, giveresurse);
                                    if (!ishand)
                                    {
                                        item.UnBreaking -= DamageforTool;
                                    }
                                }
                            }
                        }
                    }

                    for (int j = 0; j < cant.Length; j++)
                    {
                        if (attack[i].tag == cant[j])
                        {
                            item = GetComponent<item>();
                            item.UnBreaking -= Damageforcantreaourse;
                        }
                    }
                }

                if (ishand)
                {
                    if (attack[i].tag == "Tree")
                    {
                        item = GetComponent<item>();
                        attack[i].GetComponent<Tree>().BreakTree(Damage, 1);
                    }
                }
            }
        }
    }

}
