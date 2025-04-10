using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBump : MonoBehaviour
{
    private PlayerHp playerHp;

    private int playerLayer;
    public float bumpDamage;

    private void Start() {
        playerLayer = LayerData.playerLayer;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == playerLayer)
        {
            if(playerHp == null)
            {
                playerHp = collision.GetComponent<PlayerHp>();
            }
            
            playerHp.CheckDamage(bumpDamage, true);
        }
    }
}