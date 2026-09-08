using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class item : MonoBehaviour
{
    private bool StartIsShow;
    public bool IsShow;
    private bool StartIsGun;
    public bool IsGun;
    public bool isput = false;

    public int UnBreaking;
    public GameObject prefab;
    public float SizeInUI;

    public bool _isFacingRight = true;

    public bool isfarmingtool;

    private int CountInInventory = 0;
    private Transform hand;
    private Player Player;
    private SpriteRenderer render;
    private GameObject prefabcreate;
    public socket Soket;
    private Invantory inventory;
    private SpriteRenderer spriteRenderer;
    private int MaxUnBreaking;

    private Attack attack;

    public int type;

    public bool isstuck;

    public int count;
    
    public itemstuck Item;

    public GameObject createone;

    public float size = 2f;
    public float sizeUI = 1f;

    public bool createanthother;
    public GameObject anothercreate;

    public GameObject chekenull;

    public cheker c;
    public GameObject check;
    public bool ishand;

    public bool isbuy;

    public bool caneat;
    public int heath, regenerateeat;

    GameObject playerrotate;

    [SerializeField]  private Transform playerrotation;

    [SerializeField] private float rotateyplayer;

    // Start is called before the first frame update
    void Start()
    {
        hand = GameObject.FindGameObjectWithTag("Hand").transform;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Invantory>();
        playerrotation = GameObject.FindGameObjectWithTag("Player").transform;
        render = GetComponent<SpriteRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        MaxUnBreaking = UnBreaking;
        StartIsShow = IsShow;
        StartIsGun = IsGun;
    }

    // Update is called once per frame
    void Update()
    {
        playerrotate = Player.gameObject;
        rotateyplayer = playerrotate.transform.rotation.y;
        if (!isput)
        {
            _isFacingRight = Player._isFacingRight;
        }

        if (!isstuck)
        {
            ishand = inventory.ishand;
            isbuy = inventory.isbuy;
        }

        if (isstuck && inventory.ChangeSlot == CountInInventory && !inventory.isvisible)
        {
            inventory.ishand = true;
        }

        if(!isstuck && inventory.ChangeSlot == CountInInventory && inventory.isvisible)
        {
            inventory.ishand = false;
        }

        if (isstuck)
        {
            if(inventory.ChangeSlot == CountInInventory)
            {
                inventory.isstuckobj = true;
            }
        }

        if (Item != null)
        {
            Item.itm = this;
        }

        if (IsGun)
        {
            attack = GetComponent<Attack>();
        }

        IsShowOnHand();
        TeleportToHand();

        if (UnBreaking <= 0)
        {
            if (Soket != null)
            {
                Destroy(this.gameObject);
                Destroy(Soket.gameObject);
                inventory.isfull[CountInInventory] = false;
            }
            Destroyitem();
        }

        if (Input.GetKeyDown(KeyCode.Q) && isput == false && inventory.ChangeSlot == CountInInventory)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                check = Instantiate(chekenull);

                c = check.GetComponent<cheker>();

                if (!Player._isFacingRight)
                {
                    c.transform.position = new Vector3(transform.position.x - 1.5f, transform.position.y, transform.position.z);
                }
                else
                {
                    c.transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z);
                }
                c.CheckGround();
                if (c.isground == true)
                {
                    putall();
                }
                Destroy(check);
            }
            else
            {
                check = Instantiate(chekenull);

                c = check.GetComponent<cheker>();
                
                if (!Player._isFacingRight)
                {
                    c.transform.position = new Vector3(transform.position.x - 1.5f, transform.position.y, transform.position.z);
                }
                else
                {
                    c.transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z);
                }
                c.CheckGround();
                if (c.isground == true)
                {
                    putItem();
                }
                Destroy(check);
            }
        }


        if (IsGun && !isput && inventory.ChangeSlot == CountInInventory && Player.isanim == false && !ishand && !isbuy){
            if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                Player.anim.SetBool("Attack", true);
            }
        }

        if(Player.isattack == true && !isput && inventory.ChangeSlot == CountInInventory && !ishand && !isbuy){
            Player.isattack = false;
            Player.isanim = false;
            Player.anim.SetBool("Attack", false);
            if(attack != null)
            {
                attack.AttackObj();
            }
        }

        if(inventory.ChangeSlot == CountInInventory && !isput){
            _isFacingRight = Player._isFacingRight;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ItemAddToInvantory();
        }
    }

    

    public void ItemAddToInvantory()
    {
        if(!isstuck){
            for (int i = 0; i < inventory.isfull.Length; i++)
            {
                if (!inventory.isfull[i])
                {
                    if (_isFacingRight != Player._isFacingRight)
                    {
                        Flip();
                    }
                    prefabcreate = Instantiate(prefab);
                    prefabcreate.transform.position = new Vector3(inventory.Slots[i].position.x, inventory.Slots[i].position.y, inventory.Slots[i].position.z);
                    prefabcreate.transform.localScale = new Vector3(SizeInUI, SizeInUI, SizeInUI);
                    isput = false;
                    GetComponent<BoxCollider2D>().enabled = false;
                    Soket = prefabcreate.GetComponent<socket>(); 
                    CountInInventory = i;
                    inventory.isfull[i] = true;
                    prefabcreate.transform.SetParent(inventory.Slots[i]);
                    RectTransform rectTransform = prefabcreate.GetComponent<RectTransform>();
                    rectTransform.localScale = new Vector3(sizeUI, sizeUI,sizeUI);
                    inventory.notstuck[i] = this;
                    Soket = prefabcreate.GetComponent<socket>();
                    if (Soket != null)
                    {
                        Soket.MaxUnBreaking = MaxUnBreaking;
                    }
                    break;

                }
            }
        }
        if(isstuck){
            for(int i = 0; i <= inventory.isfull.Length; i++){
                if (inventory.check[i] != null && inventory.check[i].type == type)
                {
                    inventory.check[i].count += count;
                    Destroy(gameObject);
                    inventory.check[i].Item.countstuck += count;
                    break;
                }else  if (!inventory.isfull[i])
                {
                    if(inventory.check[i] == null){
                        inventory.check[i] = this;
                        isput = false;
                        GetComponent<BoxCollider2D>().enabled = false;
                        prefabcreate = Instantiate(prefab);
                        Soket = prefabcreate.GetComponent<socket>();
                        Item = prefabcreate.GetComponent<itemstuck>();
                        CountInInventory = i;
                        inventory.isfull[i] = true;
                        prefabcreate.transform.SetParent(inventory.Slots[i]);
                        prefabcreate.transform.position = new Vector3(inventory.Slots[i].position.x, inventory.Slots[i].position.y, inventory.Slots[i].position.z);
                        prefabcreate.transform.localScale = new Vector3(SizeInUI, SizeInUI, SizeInUI);
                        Item = prefabcreate.GetComponent<itemstuck>();
                        Item.countstuck = count;
                        break;
                    }
                }
            }
        }
    }

    public void putItem()
    {
        if(!isstuck){
            inventory.isvisible = true;
            if (!Player.isanim) {
            isput = true;
            GetComponent<BoxCollider2D>().enabled = true;

            if (!Player._isFacingRight) {
                transform.position = new Vector3(transform.position.x - 1.5f, transform.position.y, transform.position.z);
            } else {
                transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z);
            }

            inventory.isfull[CountInInventory] = false;
            inventory.notstuck[CountInInventory] = null;

            CountInInventory = 0;

            if (prefabcreate != null) {
                Destroy(prefabcreate);
            }
            transform.SetParent(null);
            spriteRenderer.enabled = true;
            }
        }

        if(isstuck){
            inventory.isvisible = true;
            if (inventory.check[CountInInventory].count > 1 && inventory.check[CountInInventory].count != 1)
            {
                GameObject create = Instantiate(createone);

                if (!Player._isFacingRight)
                {
                    create.transform.position = new Vector3(transform.position.x - 1.5f, transform.position.y, transform.position.z);
                }
                else
                {
                    create.transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z);
                }

                inventory.check[CountInInventory].count --;

                Item.countstuck--;

                inventory.isfull[CountInInventory] = false;

                spriteRenderer.enabled = true;
                create.GetComponent<BoxCollider2D>().enabled = true;
                create.GetComponent<item>().count = 1;
                create.GetComponent<item>().isput = true;
                create.GetComponent<SpriteRenderer>().enabled = true;

                create.transform.localScale = new Vector3(size, size, size);


            }
            else if(inventory.check[CountInInventory].count == 1){
                if (!Player.isanim) {
                    if (!createanthother)
                    {
                        isput = true;
                        GetComponent<BoxCollider2D>().enabled = true;
                    } else
                    {
                        Instantiate(anothercreate);
                        Destroy(this.gameObject);
                    }

                    if (!Player._isFacingRight) {
                        transform.position = new Vector3(transform.position.x - 1.5f, transform.position.y, transform.position.z);
                    } else {
                        transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z);
                    }

                    inventory.isfull[CountInInventory] = false;
                    inventory.check[CountInInventory] = null;

                    CountInInventory = 0;
                    transform.SetParent(null);

                    if (prefabcreate != null) {
                        Destroy(prefabcreate);
                    }
    
                    spriteRenderer.enabled = true;
                }
            }
        }
    }

    public void IsShowOnHand()
    {
        if (isput != true)
        {
            spriteRenderer.enabled = true;
            if (IsShow && isput != true)
            {
                if (inventory.ChangeSlot == CountInInventory)
                {
                    spriteRenderer.enabled = true;
                    if (!isstuck)
                    {
                        inventory.ishand = false;
                    }
                }
                else
                {
                    spriteRenderer.enabled = false;
                }
            }
            if (!IsShow)
            {
                spriteRenderer.enabled = false;
            }
        }

        if (!inventory.isvisible)
        {
            if(inventory.Line.GetComponent<SpriteRenderer>() != null)
            {
                inventory.Line.GetComponent<SpriteRenderer>().enabled = false;
            }
            IsShow = false;
            if (IsGun)
            {
                IsGun = false;
            }
        }

        if (inventory.isvisible && StartIsShow)
        {
            if (inventory.Line.GetComponent<SpriteRenderer>() != null)
            {
                inventory.Line.GetComponent<SpriteRenderer>().enabled = true;
            }
            IsShow = true;
            if (StartIsGun)
            {
                IsGun = true;
            }
        }
    }

    public void TeleportToHand()
    {
        if (!isput && hand != null)
        {
            transform.position = new Vector3(hand.position.x, hand.position.y, 0);
            transform.SetParent(hand);
            if (!isstuck && Soket != null)
            {
                Soket.Unbreaking = UnBreaking;
            }
        }
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(new Vector3(0, 180, 0));

    }

    public void putall()
    {
        if (!Player.isanim)
        {
            inventory.isvisible = true;
            isput = true;
            GetComponent<BoxCollider2D>().enabled = true;

            if (!Player._isFacingRight)
            {
                transform.position = new Vector3(transform.position.x - 1.5f, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z);
            }

            inventory.isfull[CountInInventory] = false;
            inventory.check[CountInInventory] = null;

            CountInInventory = 0;

            if (prefabcreate != null)
            {
                Destroy(prefabcreate);
            }

            spriteRenderer.enabled = true;
            transform.SetParent(null);
        }
    }

    public void Destroyitem()
    {
        if(count < 1)
        {
            Item.countstuck = count;
            inventory.check[CountInInventory] = null;
            if (prefabcreate != null)
            inventory.isfull[CountInInventory] = false;
            {
                Destroy(prefabcreate);
            }
            Destroy(this.gameObject);
        }
    }
}
