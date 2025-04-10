using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamHit : MonoBehaviour
{
    [SerializeField] float beamDamage;
    [SerializeField] GameObject beamGroup;

    [SerializeField] int playerLayer;

    void Start()
    {
        playerLayer = LayerData.playerLayer;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(beamGroup.GetComponent<CrossBeam>().isForewarning) { return; }

        if (collision.gameObject.layer == playerLayer)
        {
            collision.GetComponent<PlayerHp>().CheckDamage(beamDamage, false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (beamGroup.GetComponent<CrossBeam>().isForewarning) { return; }

        if (collision.gameObject.layer == playerLayer)
        { 
            collision.GetComponent<PlayerHp>().CheckDamage(beamDamage, false);
        }
    }
}
