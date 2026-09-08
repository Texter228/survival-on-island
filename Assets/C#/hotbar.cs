using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class hotbar : MonoBehaviour
{
    [SerializeField] private int health,eat,maxhealth,maxeat;
    [SerializeField] private Text healthtext, eattext;

    [SerializeField] private float TimetoMinusEat, StartTimeToMinusEat, TimetoMinusHealth, StartTimeToMinusHealth;

    [SerializeField] private GameObject dethMonitore;

    [SerializeField] private GameObject[] offobj;

    [SerializeField] private generateresourse[] createresourse;

    [SerializeField] private generateresourse[] SpawnAll;

    [SerializeField] private Animator animator;

    public bool hit;


    // Start is called before the first frame update
    void Start()
    {
        Load();
        TimetoMinusEat = StartTimeToMinusEat;
        TimetoMinusHealth = StartTimeToMinusHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (hit)
        {
            DamagePlayer(1);
            hit = false;
        }

        Save();
        CheckDeath();

        if (health > 0)
        {
            healthtext.text = "" + health;
            eattext.text = "" + eat;
        }

        if (eat > 0) {
            if (TimetoMinusEat <= 0)
            {
                for (int i = 0; i < maxeat; i++)
                {
                    TimetoMinusEat = StartTimeToMinusEat;
                    eat--;
                    break;
                }
            }
            else
            {
                TimetoMinusEat -= Time.deltaTime;
            }
        }

        if (eat <= 0)
        {
            if (TimetoMinusHealth <= 0)
            {
                for (int i = 0; i < maxhealth; i++)
                {
                    TimetoMinusHealth = StartTimeToMinusHealth;
                    health--;
                    animator.SetTrigger("Hit");
                    break;
                }
            }
            else
            {
                TimetoMinusHealth -= Time.deltaTime;
            }
        }
    }

    public void CheckDeath()
    {
        if(health <= 0)
        {
            PlayerPrefs.DeleteAll();
            dethMonitore.SetActive(true);
            for (int i = 0;i < offobj.Length; i++)
            {
                Destroy(offobj[i]);
            }
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt("eat", eat);
        PlayerPrefs.SetInt("health", health);
        PlayerPrefs.Save();
    }
    public void Load()
    {
        if (PlayerPrefs.HasKey("health"))
        {
            health = PlayerPrefs.GetInt("health");
        }

        if (PlayerPrefs.HasKey("eat"))
        {
            eat = PlayerPrefs.GetInt("eat");
        }
    }

    public void Respawn(int lvlscene)
    {
        health = maxhealth;
        eat = maxeat;
        for(int i = 0; i < createresourse.Length; i++)
        {
            createresourse[i].isstart = 1;
        }
        SceneManager.LoadScene(lvlscene);
        PlayerPrefs.DeleteAll();
        for (int i = 0; i < SpawnAll.Length; i++) 
        {
            SpawnAll[i].CreateAllObj();
        }
    }

    public void HealthHp(int addhealth)
    {
        if(health+addhealth < maxhealth)
        {
            health += addhealth;
        }
        else
        {
            if (addhealth > 0)
                health = maxhealth;
        }
    }

    public void regenerateeat(int addeat)
    {
        if (eat + addeat < maxeat)
        {
            eat += addeat;
        }
        else
        {
            if (addeat > 0)
                eat = maxeat;
        }
    }

    public int GetEat()
    {
        return eat;
    }

    public int GetMaxEat()
    {
        return maxeat;
    }

    public void DamagePlayer(int damage)
    {
        health -= damage;
        animator.SetTrigger("Hit");
    }
}
