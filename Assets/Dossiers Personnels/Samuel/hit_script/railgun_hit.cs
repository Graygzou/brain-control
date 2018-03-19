using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class railgun_hit : MonoBehaviour {

    public float damage;
    public PlayerStatus player;
    public GameController GM;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {
            Destroy(this);
        }

        if (col.gameObject.CompareTag("Enemy"))
        {
            GM = GameObject.Find("GameController").GetComponent<GameController>();
            if (!GM.GetLoseLifeWave())
            {
                if (col.gameObject.GetComponent<Enemy>().GetnTeam() != player.GetComponent<PlayerCon2d>().GetnJoueur())
                {
                    col.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                    col.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    col.gameObject.GetComponent<MovingObject>().enabled = false;
                    col.gameObject.GetComponent<Collider2D>().enabled = false;
                    col.gameObject.GetComponent<Enemy>().SetnTeam(player.GetComponent<PlayerCon2d>().GetnJoueur());
                    col.gameObject.GetComponent<Animator>().SetTrigger("isDead");
                }
            }
            else
            {
                player.addKill();
                player.addScore(10);
                player.GainEnergy(5);
                col.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                col.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                col.gameObject.GetComponent<MovingObject>().enabled = false;
                col.gameObject.GetComponent<Collider2D>().enabled = false;
                col.gameObject.GetComponent<Enemy>().SetnTeam(player.GetComponent<PlayerCon2d>().GetnJoueur());
                col.gameObject.GetComponent<Animator>().SetTrigger("isDead");
            }
        }
    }
}
