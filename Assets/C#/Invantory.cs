using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Invantory : MonoBehaviour
{
    public bool[] isfull;
    public Transform[] Slots;
    public int[] Number;
    public bool ishand;

    public item[] check;
    public item[] notstuck;

    public GameObject Line;
    public int StartChangeSlot;
    public int ChangeSlot;

    public int[] doublestate;
    public bool[] doubleisfull;

    public Animator anim;

    public bool isvisible = true;

    private Player Player;

    private Attack attack;

    public bool isbuy;

    public bool isstuckobj;

    // Start is called before the first frame update
    void Start()
    {
        doublestate = new int[3];
        doubleisfull = new bool[3];
        doublestate[0] = -1;
        doublestate[1] = -2;
        doublestate[2] = -3;
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        attack = GameObject.FindGameObjectWithTag("Hand").GetComponent<Attack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (check[ChangeSlot] != null)
        {
            isvisible = false;
        }
        else
        {
            isvisible=true;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            isbuy = !isbuy;
        }

        if (!isfull[ChangeSlot] && isvisible == true)
        {
            ishand = true;
        }
        else if (isfull[ChangeSlot] && isvisible == false)
        {
            ishand = true;
        }
        else if (isfull[ChangeSlot] && !isstuckobj)
        {
            ishand = false;
        }

        // Проверка для тапа и удержания на мобильных устройствах
        bool isTouching = false;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Если касание началось, продолжается или перемещается
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                isTouching = true;
            }
        }

        // Если касание началось или продолжается (или удерживается кнопка мыши на ПК)
        if (ishand && Player.isanim == false && !isbuy && isTouching)
        {
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                Player.anim.SetBool("Attack", true);
            }

        }
        else if (Input.GetMouseButton(0)) // Для тестирования на ПК
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Player.anim.SetBool("Attack", true);
            }
        }

        // Проверка на завершение атаки
        if (Player.isattack == true && !isbuy && ishand)
        {
            Player.isattack = false;
            Player.isanim = false;
            Player.anim.SetBool("Attack", false);
            attack.ishand = true;
            attack.istool = true;
            attack.AttackObj();
        }

        // Логика смены слота с помощью колеса мыши
        if (Input.mouseScrollDelta.y < 0)
        {
            ChangeSlot += 1;
            isvisible = true;
        }

        if (Input.mouseScrollDelta.y > 0)
        {
            isvisible = true;
        }

        // Ограничение изменения слотов в пределах допустимого диапазона
        if (ChangeSlot >= Slots.Length)
        {
            ChangeSlot = 0;
        }
        else if (ChangeSlot < 0)
        {
            ChangeSlot = 3;
        }

        // Обработка переключения слотов с помощью клавиш 1-4
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeSlot = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeSlot = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeSlot = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeSlot = 3;
            ishand = false; // Отключаем руку при выборе 4-го слота
        }

        // Обновление позиции линии слота
        Line.transform.position = new Vector3(Slots[ChangeSlot].position.x, Slots[ChangeSlot].position.y, 0);
    }

    // Обработка смены слота и логика двойного клика

    public void ChangeSLOT(int number)
    {
        ChangeSlot = number;
       
    }

    public void Put()
    {
        if (check[ChangeSlot] != null)
        {
            check[ChangeSlot].putItem();
        }
        else
        {
            notstuck[ChangeSlot].putItem();
        }
    }

    public void PutAll()
    {
        if (check[ChangeSlot] != null)
        {
            check[ChangeSlot]?.putall();
        }
        else if (notstuck[ChangeSlot] != null)
        {
            notstuck[ChangeSlot]?.putItem();
        }
        else
        {
            Debug.LogWarning($"Slot {ChangeSlot} is empty or not properly assigned.");
        }
    }

}
