using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basic_hit : MonoBehaviour {

    public float damage;
    public PlayerStatus player;
    public GameController GM;

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Hit");
        if (col.gameObject.tag.Equals("Wall"))
        {
            Destroy(this.gameObject);
        }
        if (col.gameObject.tag.Equals("Enemy")) {
            GM = GameObject.Find("GameController").GetComponent<GameController>();
            if (!GM.GetLoseLifeWave())
            {
                if(col.gameObject.GetComponent<Enemy>().GetnTeam() != player.GetComponent<PlayerCon2d>().GetnJoueur())
                {
                    DesactivateEntity(col);
                    Destroy(this.gameObject);
                }
            }
            else
            {
                Destroy(this.gameObject);
                player.addKill();
                player.addScore(10);
                player.GainEnergy(5);

                DesactivateEntity(col);
            }

        }
    }

    private void DesactivateEntity(Collision2D col)
    {
        col.gameObject.GetComponent<Rigidbody2D>().simulated = false;
        // Kill the entity
        col.gameObject.GetComponent<MovingObject>().Death();

        col.gameObject.GetComponent<MovingObject>().enabled = false;
        col.gameObject.GetComponent<Collider2D>().enabled = false;
        col.gameObject.GetComponent<Enemy>().SetnTeam(player.GetComponent<PlayerCon2d>().GetnJoueur());
        col.gameObject.GetComponent<Animator>().SetTrigger("isDead");
    }
}
