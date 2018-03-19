using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffSpawner : MonoBehaviour {

    //public float tempsAvantProchaineApparition = 10;
    private float tempsAvantNonActif;

    private bool actif; // permet de savoir si on peut faire spawn quelquechose
    private bool sansItem; // informe si oui ou non une item est sur le spawn

    private GameObject instanceDeStuff;
    private GameController gameContr;

    // Use this for initialization
    void Start () {
        actif = false;
        //tempsAvantNonActif = -tempsAvantProchaineApparition;
        sansItem = true;
        gameContr = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

    }
	
	// Update is called once per frame
	void Update () {

        if (sansItem && actif && /*(Time.time > tempsAvantNonActif ||*/ gameContr.GetNewCycle())
        {
            actif = false;
        }

	}

    public bool EstActif()
    {
        return actif;
    }

    // Quand quelqu'un rentre dans le collider du spawn
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player") && !sansItem)
        {

            collision.GetComponent<PlayerStatus>().RamasserObjet(instanceDeStuff);
            Destroy(instanceDeStuff, 0.1f);

            sansItem = true;
            

            // On doit attendre un certain temps avant de faire respawn quelque chose
            //tempsAvantNonActif = Time.time + tempsAvantProchaineApparition;
        }
    }

    // Fait apparaitre item
    public void Spawn(GameObject item)
    {

        instanceDeStuff = (GameObject)Instantiate(item, this.transform.position, new Quaternion(0, 0, 0, 0));
        sansItem = false;
        actif = true;
        gameContr.SetNewCycle(false);
        //switch (item)
        //{
        //    case "healthpack":
                
        //        break;

        //    case "shotgun":
        //        instanceDeStuff = (GameObject)Instantiate(shotgun, this.transform.position, new Quaternion(0, 0, 0, 0));
        //        sansItem = false;
        //        actif = true;
        //        break;

        //    case "rifle":
        //        instanceDeStuff = (GameObject)Instantiate(rifle, this.transform.position, new Quaternion(0, 0, 0, 0));
        //        sansItem = false;
        //        actif = true;
        //        break;

        //    case "lmg":
        //        instanceDeStuff = (GameObject)Instantiate(lmg, this.transform.position, new Quaternion(0, 0, 0, 0));
        //        sansItem = false;
        //        actif = true;
        //        break;

        //    case "sniper":
        //        instanceDeStuff = (GameObject)Instantiate(sniper, this.transform.position, new Quaternion(0, 0, 0, 0));
        //        sansItem = false;
        //        actif = true;
        //        break;

        //    case "railgun":
        //        instanceDeStuff = (GameObject)Instantiate(railgun, this.transform.position, new Quaternion(0, 0, 0, 0));
        //        sansItem = false;
        //        actif = true;
        //        break;
        //}
        
    }

}
