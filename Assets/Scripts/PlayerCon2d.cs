using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCon2d : MovingObject {

    

    //speed modification

    private float speed;
    [SerializeField]
    private float speedMulti;
    [SerializeField]
    private float staminaRegen;
    [SerializeField]
    private float maxStamina;
    private float currentStamina;
    private bool exhausted;
    private AudioSource audio;

    //Shooting Mod
    [SerializeField]
    private float shootMulti;
    [SerializeField]
    private float shootDur;
    [SerializeField]
    private float shootWait;
    private float shootTime;


    [SerializeField]
    private int nJoueur;

    private Vector3 ShootVec;
    float horizontalAim;
    float verticalAim;

    [SerializeField]
    private GameObject activeWeapon;    



    Transform playerTrans;

    Quaternion playerDir;

    public bool usingController;
    

    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        playerTrans = GetComponent<Transform>();
        ShootVec = Vector3.right;
        shootTime = 0;
        currentStamina = maxStamina;
        m_ownAnimator = GetComponent<Animator>();
        exhausted = false;

        InitializePosition();

        audio = this.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        Sprint();

        Walking();

        LookAround();

        if (this.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
        {
            audio.enabled = true;
        }
        else audio.enabled = false;

        horizontalAim = Input.GetAxis("HorizontalAim" + nJoueur);
        verticalAim = Input.GetAxis("VerticalAim" + nJoueur);
        if (verticalAim != 0 || horizontalAim != 0)
        {
            ShootVec = new Vector3(Input.GetAxis("HorizontalAim" + nJoueur), Input.GetAxis("VerticalAim" + nJoueur), 0.0f);
        }

        if (Input.GetAxis("Attaque" + nJoueur) != 0)
        {
            Attaquer();
        }
         
        if(Input.GetButtonDown("Switch" + nJoueur))
        {
            SwitchWeapon();
        }

        if(Input.GetButtonDown("AtSpeed" + nJoueur))
        {
            PlusUltraMcreeMan();
        }

        if (activeWeapon.GetComponent<special_shoot>()  != null && activeWeapon.GetComponent<special_shoot>().nom == "")
        {
            foreach (Transform child in this.gameObject.transform) if (child.CompareTag("Basic"))
                {
                    activeWeapon.SetActive(false);
                    activeWeapon = child.gameObject;
                    activeWeapon.SetActive(true);
                }
        }


        AnimationManager();
    }

    public void LookAround()
    {
        float horizontal = Input.GetAxis("HorizontalAim" + nJoueur);
        float vertical = Input.GetAxis("VerticalAim" + nJoueur);
        if (vertical != 0 || horizontal != 0)
        {
            playerTrans.rotation = Quaternion.Euler(playerTrans.rotation.x, playerTrans.rotation.y, Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg);
        }
    }

    #region Combat

    public void Attaquer()
    {
        if (activeWeapon.CompareTag("Basic"))
        {
            activeWeapon.GetComponent<pistol_shoot>().Fire(ShootVec);
        }
        else
        {
            activeWeapon.GetComponent<special_shoot>().Fire(ShootVec);
        }
    }

    public void SwitchWeapon()
    {
        

        if (activeWeapon.CompareTag("Basic"))
        {
            foreach(Transform child in this.gameObject.transform) if (child.CompareTag("Special") && child.GetComponent<special_shoot>().nom != "")
                {
                    activeWeapon.SetActive(false);
                    activeWeapon = child.gameObject;
                    activeWeapon.SetActive(true);
                }
        }
        else
        {
            foreach (Transform child in this.gameObject.transform) if (child.CompareTag("Basic"))
                {
                    activeWeapon.SetActive(false);
                    activeWeapon = child.gameObject;
                    activeWeapon.SetActive(true);
                }
        }
    }

    public void PlusUltraMcreeMan()
    {
        if (Time.time >= shootTime)
        {
            shootTime = Time.time + shootWait;
            StartCoroutine(PlusUltraMcreeManCo());
        }
    }
    public IEnumerator PlusUltraMcreeManCo()
    {
        if (activeWeapon.CompareTag("Basic"))
        {
            activeWeapon.GetComponent<pistol_shoot>().Attackrate *= shootMulti;
            yield return new WaitForSecondsRealtime(shootDur);
            activeWeapon.GetComponent<pistol_shoot>().Attackrate /= shootMulti;
        }
        else
        {
            activeWeapon.GetComponent<special_shoot>().Attackrate *= shootMulti;
            yield return new WaitForSecondsRealtime(shootDur);
            activeWeapon.GetComponent<special_shoot>().Attackrate /= shootMulti;
        }
    }
    #endregion

    #region Movement
    public void Walking()
    {
        if (usingController)
        {
            rb2d.velocity = new Vector2(Input.GetAxis("Horizontal" + nJoueur) * speed, Input.GetAxis("Vertical" + nJoueur) * speed);
        }
        else
        {
            rb2d.velocity = new Vector2(Input.GetAxis("KeyHo") * speed, Input.GetAxis("KeyVe") * speed);
        }
    }
    public void Sprint()
    {
        if(currentStamina == 0)
        {
            exhausted = true;
        }
        if(currentStamina == maxStamina)
        {
            exhausted = false;
        }
        if(Input.GetAxis("Sprint" + nJoueur) > 0 && !exhausted && rb2d.velocity != Vector2.zero)
        {
            speed = speedMulti * baseSpeed;
            currentStamina--;
        }
        else
        {
            speed = baseSpeed;
            if(currentStamina < maxStamina)
            {
                currentStamina += staminaRegen;
            }
        }
    }
    #endregion

    public void AnimationManager()
    {
        if (activeWeapon.CompareTag("Basic"))
        {
            if (m_ownAnimator.GetBool("handgun") == false)
            {
                m_ownAnimator.SetBool("handgun", true);
                m_ownAnimator.SetBool("shotgun", false);
                m_ownAnimator.SetBool("rifle", false);
                m_ownAnimator.SetBool("knife", false);
            }
            if (Input.GetAxis("Attaque" + nJoueur) != 0)
            {
                if (m_ownAnimator.GetBool("isShooting") == false)
                {
                    m_ownAnimator.SetBool("isShooting", true);
                    m_ownAnimator.SetBool("isMoving", false);
                }

            }
            else if (Input.GetAxis("Horizontal" + nJoueur) != 0 || Input.GetAxis("Vertical" + nJoueur) != 0)
            {
                m_ownAnimator.SetBool("isShooting", false);
                m_ownAnimator.SetBool("isMoving", true);
            }
            else
            {
                m_ownAnimator.SetBool("isShooting", false);
                m_ownAnimator.SetBool("isMoving", false);
            }
        }
        else
        {
            switch (activeWeapon.GetComponent<special_shoot>().nom)
            {
                case "shotgun":
                    if (m_ownAnimator.GetBool("shotgun") == false)
                    {
                        m_ownAnimator.SetBool("handgun", false);
                        m_ownAnimator.SetBool("shotgun", true);
                        m_ownAnimator.SetBool("rifle", false);
                        m_ownAnimator.SetBool("knife", false);
                    }
                    if (Input.GetAxis("Attaque" + nJoueur) != 0)
                    {
                        if (m_ownAnimator.GetBool("isShooting") == false)
                        {
                            m_ownAnimator.SetBool("isShooting", true);
                            m_ownAnimator.SetBool("isMoving", false);
                            m_ownAnimator.SetBool("isReloading", false);
                        }

                    }
                    else if (Input.GetAxis("Horizontal" + nJoueur) != 0 || Input.GetAxis("Vertical" + nJoueur) != 0)
                    {
                        m_ownAnimator.SetBool("isShooting", false);
                        m_ownAnimator.SetBool("isMoving", true);
                        m_ownAnimator.SetBool("isReloading", false);
                    }
                    else
                    {
                        m_ownAnimator.SetBool("isShooting", false);
                        m_ownAnimator.SetBool("isMoving", false);
                        m_ownAnimator.SetBool("isReloading", false);
                    }
                    break;
                case "rifle":
                case "lmg":
                    if (m_ownAnimator.GetBool("rifle") == false)
                    {
                        m_ownAnimator.SetBool("handgun", false);
                        m_ownAnimator.SetBool("shotgun", false);
                        m_ownAnimator.SetBool("rifle", true);
                        m_ownAnimator.SetBool("knife", false);
                    }
                    if (Input.GetAxis("Attaque" + nJoueur) != 0)
                    {
                        if (m_ownAnimator.GetBool("isShooting") == false)
                        {
                            m_ownAnimator.SetBool("isShooting", true);
                            m_ownAnimator.SetBool("isMoving", false);
                            m_ownAnimator.SetBool("isReloading", false);
                        }

                    }
                    else if (Input.GetAxis("Horizontal" + nJoueur) != 0 || Input.GetAxis("Vertical" + nJoueur) != 0)
                    {
                        m_ownAnimator.SetBool("isShooting", false);
                        m_ownAnimator.SetBool("isMoving", true);
                        m_ownAnimator.SetBool("isReloading", false);
                    }
                    else
                    {
                        m_ownAnimator.SetBool("isShooting", false);
                        m_ownAnimator.SetBool("isMoving", false);
                        m_ownAnimator.SetBool("isReloading", false);
                    }
                    break;
                case "knife":
                    if (m_ownAnimator.GetBool("knife") == false)
                    {
                        m_ownAnimator.SetBool("handgun", false);
                        m_ownAnimator.SetBool("shotgun", false);
                        m_ownAnimator.SetBool("rifle", false);
                        m_ownAnimator.SetBool("knife", true);
                    }
                    if (Input.GetAxis("Attaque" + nJoueur) != 0)
                    {
                        if (m_ownAnimator.GetBool("isShooting") == false)
                        {
                            m_ownAnimator.SetBool("isShooting", true);
                            m_ownAnimator.SetBool("isMoving", false);
                            m_ownAnimator.SetBool("isReloading", false);
                        }

                    }
                    else if (Input.GetAxis("Horizontal" + nJoueur) != 0 || Input.GetAxis("Vertical" + nJoueur) != 0)
                    {
                        m_ownAnimator.SetBool("isShooting", false);
                        m_ownAnimator.SetBool("isMoving", true);
                        m_ownAnimator.SetBool("isReloading", false);
                    }
                    else
                    {
                        m_ownAnimator.SetBool("isShooting", false);
                        m_ownAnimator.SetBool("isMoving", false);
                        m_ownAnimator.SetBool("isReloading", false);
                    }
                    break;

            }
        }
    }




    #region Get/Set
    public float GetCurrentStaminaRatio()
    {
        return currentStamina / maxStamina;
    }
    public int GetnJoueur()
    {
        return nJoueur;
    }
    public GameObject GetActive()
    {
        return activeWeapon;
    }
    #endregion

}
