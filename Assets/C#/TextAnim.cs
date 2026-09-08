using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextAnim : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject text;
    [SerializeField] private bool islvlload;
    [SerializeField] private int lvlload;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
        text.SetActive(true);
        animator.SetTrigger("start");
        PlayerPrefs.DeleteAll();
        if (islvlload)
        {
            SceneManager.LoadScene(lvlload);
        }
    }

    public void offtext()
    {
        text.SetActive(false);
    }
}
