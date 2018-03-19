using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket_hit : MonoBehaviour {

    public float damage;
    public PlayerStatus player;
    public GameController GM;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Wall") || col.gameObject.tag.Equals("Enemy"))
        {
            Collider2D[] inexplosion = Physics2D.OverlapCircleAll(transform.position,3f);
            Destroy(this.gameObject);

            foreach (Collider2D item in inexplosion)
            {
                if (col.gameObject.tag.Equals("Enemy"))
                {
                    GM = GameObject.Find("GameController").GetComponent<GameController>();
                    if (!GM.GetLoseLifeWave())
                    {
                        if (col.gameObject.GetComponent<Enemy>().GetnTeam() != player.GetComponent<PlayerCon2d>().GetnJoueur())
                        {
                            Destroy(col.gameObject);
                        }
                    }
                    else
                    {
                        Destroy(this.gameObject);
                        player.addKill();
                        player.addScore(10);
                        player.GainEnergy(5);
                        Destroy(col.gameObject);
                    }
                }
            }
        }
    }
}
