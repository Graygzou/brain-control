using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [Header("Timers")]
    [SerializeField] private float timeLimit = 0f;
    [SerializeField] private int enemyCount = 0;
    [SerializeField] private bool loseLifeWave = true;
    [SerializeField] private bool reset = true;

    [Header("Enemies")]
    [SerializeField] private int enemyAmount = 10;
    [SerializeField] private int enemyAmountReady = 200;
    [SerializeField] private GameObject poolEnemies;
    [SerializeField] private float spawnInterval = 0.25f;
    // List that contains all the ready zombies.
    private List<GameObject> zombieListReady;

    private Vector3[] playerSpawnList = { new Vector3(1.5f, 0, 0), new Vector3(0, 1.5f, 0) };
    private float timer = 0f;
    public float GetTimer() { return timer; }
    private int cycle = 0;
    private GameObject players;
    private GameObject zombies;
    private GameObject[] playerList;
    // Current list of zombies
    private List<GameObject> zombieList = new List<GameObject>();

    private bool newCycle = false;
    public bool GetNewCycle() { return newCycle; }
    public void SetNewCycle(bool var) { newCycle = var; }
    private int winner = 0;
    public int GetWinner() { return winner; }

    public GameObject[] listeSpawn; // Contient les prefabs des spawners pour ennemies
    public GameObject[] listeEnnemies; // Contient les prefabs des ennemies
    public GameObject[] playerPrefabList;
    private float tempsDernierSpawnEnnemie; // Temps auquel le dernier ennemie est apparu

    public float GetTimeLimit() { return timeLimit; }

    public bool GetLoseLifeWave() { return loseLifeWave; }

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(20, 20, true);
        Physics2D.IgnoreLayerCollision(20, 21, true);
        Physics2D.IgnoreLayerCollision(20, 22, true);
        Physics2D.IgnoreLayerCollision(21, 21, true);
        Physics2D.IgnoreLayerCollision(21, 22, true);
        Physics2D.IgnoreLayerCollision(21, 23, true);
        Physics2D.IgnoreLayerCollision(9, 20, true);
        Physics2D.IgnoreLayerCollision(9, 21, true);

        zombieListReady = new List<GameObject>();

        tempsDernierSpawnEnnemie = 0f;
        players = GameObject.FindGameObjectWithTag("PlayerParent");
        zombies = GameObject.FindGameObjectWithTag("ZombieParent");

        // Create players
        foreach (GameObject player in playerPrefabList) 
        {
            GameObject current_player = Instantiate(player, playerSpawnList[player.GetComponent<PlayerCon2d>().GetnJoueur() - 1], new Quaternion(0, 0, 0, 0), players.GetComponent<Transform>());
            //current_player.GetComponent<MovingObject>().InitializePosition();
        }
        playerList = GameObject.FindGameObjectsWithTag("Player");

        // Preload a tons of ennemies ready to activate
        for (int i=0; i < enemyAmount; i++)
        {
            Vector3 spawn = listeSpawn[Random.Range(0, listeSpawn.Length)].GetComponent<Transform>().position;
            CreatePoolEnnemies(listeEnnemies[0], spawn);
        }
    }

    private void Update () {

        if(loseLifeWave && reset)
        {
            bool enemyStillAlive = false;
            foreach (GameObject zombie in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                // Get script
                Enemy script = zombie.transform.GetChild(0).GetComponent<Enemy>();
                if (script.IsAlive())
                {
                    enemyStillAlive = true;
                    break;
                }
            }
            // if all the ennemies are dead
            if (!enemyStillAlive) 
            {
                loseLifeWave = false;
                timer = 0f;
                // Iterate over all the zombie in this wave.
                foreach(GameObject enemy in zombieList)
                {
                    // Cast the current gamObject to a movingObject
                    enemy.GetComponent<Collider2D>().enabled = true;
                    enemy.GetComponent<Rigidbody2D>().simulated = true;
                    Enemy script = enemy.GetComponent<Enemy>();
                    script.enabled = true;

                    // Make the player alive again
                    if (script.GetnTeam() == 1)
                    {
                        script.Revive(0);
                    }
                    else if (script.GetnTeam() == 2)
                    {
                        script.Revive(1);
                    }
                }
            }
        }
        else
        {
            if (timer >= timeLimit && reset)
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject item in enemies)
                {
                    //Destroy(item);
                    item.SetActive(false);
                }
                reset = false;
                newCycle = true;
                enemyAmount = 15 + 5 * (cycle + 1);
                timeLimit = 10 + cycle;
                cycle++;
                loseLifeWave = true;
            }
            else if (!reset)
            {
                enemyAmount -= SpawnWave(0.1f);

                if (enemyAmount == 0)
                {
                    reset = true;
                }
            }
            else timer += Time.deltaTime;
        }

        if (!playerList[0].GetComponent<PlayerStatus>().GetAlive() && playerList[1].GetComponent<PlayerStatus>().GetAlive())
        {
            winner = 2;
        }
        if (!playerList[1].GetComponent<PlayerStatus>().GetAlive() && playerList[0].GetComponent<PlayerStatus>().GetAlive())
        {
            winner = 1;
        }
	}

    private void CreatePoolEnnemies(GameObject ennemie, Vector3 spawn)
    {
        Vector3 pos = new Vector3(Random.Range(0f, 0.5f), Random.Range(0f, 0.5f), Random.Range(0f, 0.5f));
        GameObject instanceDeEnnemie = Instantiate(ennemie, spawn + pos, new Quaternion(0, 0, 0, 0), GameObject.FindGameObjectWithTag("Pool").GetComponent<Transform>());
        instanceDeEnnemie.GetComponentInChildren<MovingObject>().InitializePosition();
        instanceDeEnnemie.SetActive(false);
        // Register the ennemie to the list
        zombieListReady.Add(instanceDeEnnemie);
    }

    // Fait apparaitre un ennemie donné à une position donnée
    private void SpawnEnnemie(Vector2 spawn)
    {
        // First spawn of ennemies
        for (int i = 0; i < enemyAmount; i++)
        {
            // Retrieve a bot
            GameObject zombie = zombieListReady[i];
            // Set his position to the world
            Vector2 pos = new Vector3(Random.Range(0f, 0.5f), Random.Range(0f, 0.5f), Random.Range(0f, 0.5f));
            zombie.transform.position = spawn + pos;
            // Add him to the list
            zombieList.Add(zombie);
            // Make him visible
            zombie.gameObject.SetActive(true);
        }
    }

    // Fait apparaitre un ennemie donné à une position donnée
    /*private void SpawnEnnemie(GameObject ennemie, Vector3 spawn)
    {
        GameObject instanceDeEnnemie = Instantiate(ennemie, spawn, new Quaternion(0, 0, 0, 0), GameObject.FindGameObjectWithTag("ZombieParent").GetComponent<Transform>());
        //instanceDeEnnemie.GetComponent<MovingObject>().InitializePosition();
        instanceDeEnnemie.SetActive(true);
    }*/

    private int SpawnWave(float interval)
    {
        if (Time.time > tempsDernierSpawnEnnemie + interval)
        {
            // TODO Pour l'instant c'est un ennemi dans un spawn random
            Vector3 pos = new Vector3(Random.Range(0f, 0.5f), Random.Range(0f, 0.5f), Random.Range(0f, 0.5f));
            SpawnEnnemie(listeSpawn[Random.Range(0, listeSpawn.Length)].GetComponent<Transform>().position + pos);
            tempsDernierSpawnEnnemie = Time.time;
            return 1;
        }
        return 0;
    }

}

