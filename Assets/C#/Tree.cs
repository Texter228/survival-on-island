using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public int breking;
    public GameObject wood;

    private Transform player; 
    public ParticleSystem particleSystem; 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        particleSystem.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (breking <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void BreakTree(int damage, int giveresours)
    {
        breking -= damage;
        GameObject create = Instantiate(wood, player.transform);

        create.GetComponent<item>().count = giveresours;
        create.transform.SetParent(null);  

        particleSystem.Stop();
        particleSystem.Play();

        StartCoroutine(StopParticlesAfterDelay(0.5f));
    }
    private IEnumerator StopParticlesAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (particleSystem != null && particleSystem.isPlaying)
        {
            particleSystem.Stop();
        }
    }
}
