using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class controlorplayer : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private int startorder;

    [SerializeField] private int uplayer = 38;

    [SerializeField] private int downlayer;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("DownPosition").GetComponent<Transform>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(player.position.y > transform.position.y)
            {
                spriteRenderer.sortingOrder = uplayer;
            }

            if (player.position.y < transform.position.y)
            {
                spriteRenderer.sortingOrder = downlayer;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            spriteRenderer.sortingOrder = 0;

            player = null;
            startorder = 0;
        }
    }
}
