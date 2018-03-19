using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffSpawnersManager : MonoBehaviour
{

    // Liste de Spawners (des objets vides avec un transform à l'emplacement où l'on veut le spawn)
    public GameObject[] lieuDeSpawn;
    public GameObject healthPack;
    public GameObject specialAmmo;
    public GameObject[] listeArmes;

    public float ratioSoin = 0.3f;
    public float ratioSpecialAmmo = 0.2f;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
        foreach(GameObject spawn in lieuDeSpawn)
        {

            // Fait apparaitre un pack de soin (selon ratio), un pack de munition (selon ratio) ou une arme au hasard
            StuffSpawner spawnerScript = spawn.GetComponent<StuffSpawner>();
            if (!spawnerScript.EstActif())
            {
                float rng = Random.Range(0.0f, 1.0f); // Valeur aléatoire

                if ( rng < ratioSoin)
                {
                    spawnerScript.Spawn(healthPack); // un pack de soin
                }
                else if (rng < ratioSoin+ratioSpecialAmmo)
                {
                    spawnerScript.Spawn(specialAmmo); // un pack de munitions speciales
                }
                else
                {
                    spawnerScript.Spawn(listeArmes[Random.Range(0,listeArmes.Length)]);
                }
            }
        }

	}

    //private void 
}
