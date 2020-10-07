using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpRefresh : MonoBehaviour
{
    private bool shouldSpawn = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            //Debug.Log("Player touched refresher");
            player.JumpRefresh();

            if (shouldSpawn)
            {
                RefresherRandomizer.SpawnRefresher(gameObject.transform.position);
                shouldSpawn = false;
            }
        }
    }

    public void ResetShouldSpawn()
    {
        shouldSpawn = true;
    }
}
