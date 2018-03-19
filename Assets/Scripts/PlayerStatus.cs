using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {
    [SerializeField]
    private int maxNrj;
    private int nrj;

    [SerializeField]
    private int timeDmg;
    [SerializeField]
    private float lossTimer;
    [SerializeField]
    private bool loseWave;
    [SerializeField]
    private bool losing;
    private bool alive;

    private int kills;

    private int score;

    private GameController gameController;

    IEnumerator coroutineLoss;

    Rigidbody2D rb2D;
    Collider2D coll;
    PlayerCon2d Controller2D;
    Animator anim;

	// Use this for initialization
	void Start () {
        rb2D = GetComponent<Rigidbody2D>();
        Controller2D = GetComponent<PlayerCon2d>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        score = 0;
        kills = 0;
        nrj = maxNrj;

       gameController = GameObject.Find("GameController").GetComponent<GameController>();

        coroutineLoss = EnergyLossRoutine();
        EnergyLoss();
        alive = true;
    }
	
	// Update is called once per frame
	void Update () {
        if(nrj <= 0)
        {
            Death();
        }
        loseWave = gameController.GetLoseLifeWave();
        if (alive)
        {
            EnergyLoss();
        }
            
        if(nrj > maxNrj)
        {
            nrj = maxNrj;
        }
    }

    public void LoseEnergy(int dmg)
    {
        nrj -= (int)dmg;
    }

    public void GainEnergy(int hpGain)
    {
        nrj += (int)hpGain;
    }

    public IEnumerator EnergyLossRoutine()
    {
        while (true)
        {
            LoseEnergy(timeDmg);
            yield return new WaitForSeconds(lossTimer);
            
        }
      
    }

    public void EnergyLoss()
    {
        if(loseWave && !losing)
        {
            losing = true;
            StartCoroutine(coroutineLoss);
        }
        if(!loseWave && losing){
            losing = false;
            StopCoroutine(coroutineLoss);
        }
    }

    public void Death()
    {
        if(Controller2D != null && Controller2D.enabled)
        {
            rb2D.velocity = Vector2.zero;
            Controller2D.enabled = false;
            coll.enabled = false;
            Destroy(anim);
            alive = false;
        }

    }
    #region Get/Set
    public bool GetAlive()
    {
        return alive;
    }
    public int GetScore()
    {
        return score;
    }
    public int GetKills()
    {
        return kills;
    }
    public void SetKills(int value)
    {
        kills = value;
    }
    public float GetEnergyRatio()
    {
        return (float)nrj / (float)maxNrj;
    }
    #endregion

    #region Score
    public void addScore(int toAdd)
    {
        score += toAdd;
    }

    public void addKill()
    {
        kills++;
    }
    #endregion

    public int RamasserObjet(GameObject pickup)
    {
        if (pickup.CompareTag("Special")){
            foreach (Transform child in this.gameObject.transform) if (child.CompareTag("Special"))
                {
                    special_shoot childScript, pickupScript;
                    childScript = child.GetComponent<special_shoot>();
                    pickupScript = pickup.GetComponent<special_shoot>();
                    childScript.nom = pickupScript.nom;
                    childScript.ChargeAmmo();
                    
                }
            
        }
        else if (pickup.CompareTag("Health"))
        {
            GainEnergy(pickup.GetComponent<HealthPack>().GetHealth());
        }
        else if (pickup.CompareTag("Ammo"))
        {
            foreach (Transform child in this.gameObject.transform) if (child.CompareTag("Special") && child.GetComponent<special_shoot>().nom != "")
                {
                    child.GetComponent<special_shoot>().ChargeAmmo();
                }
        }
        return 0;
    }

}
