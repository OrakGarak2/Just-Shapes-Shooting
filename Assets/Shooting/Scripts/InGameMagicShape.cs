using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMagicShape : MonoBehaviour
{
    int playerLayer;
    PlayerHp playerHp;

    void Start()
    {
        playerLayer = LayerData.playerLayer;
        GetComponent<Collider2D>().enabled = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == playerLayer)
        {
            if(playerHp == null)
            {
                playerHp = collision.GetComponent<PlayerHp>();
            }

            playerHp.EnterMagicShape();
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == playerLayer)
        {
            playerHp.ExitMagicShape();
        }
    }
}
