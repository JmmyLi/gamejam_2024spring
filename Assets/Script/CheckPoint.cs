using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Vector3 respawnPosition;
    public int respawnTime;
    public SpriteRenderer spriteRenderer;
    public Material inactiveMaterial;
    public Material activeMaterial;

    public GameObject player;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (player.GetComponent<PlayerController>().checkPoint == gameObject)
        {
            spriteRenderer.material = activeMaterial;
        }
        else
        {
            spriteRenderer.material = inactiveMaterial;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject == player)
        {
            player.GetComponent<PlayerController>().checkPoint = gameObject;
        }
    }
}
