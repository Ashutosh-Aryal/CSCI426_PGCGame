using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpRefresh : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            Debug.Log("Player touched refresher");
            player.JumpRefresh();
        }

        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    Debug.Log("Player touched refresher");
        //    Player player = collision.gameObject.GetComponent<Player>();
        //    player.JumpRefresh();
        //}
    }
}
