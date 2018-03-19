using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_hit : MonoBehaviour {

    public float damage;
    public PlayerStatus player;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Enemy"))
        {
            Destroy(this.gameObject);
            Destroy(col.gameObject);
            player.addKill();
        }
    }
}
