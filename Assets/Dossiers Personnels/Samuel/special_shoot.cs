using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class special_shoot : MonoBehaviour
{

    public GameObject prefab;
    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject prefab3;
    public GameObject muzzle;

    private int currentAmmo;

    [SerializeField]
    private int maxShotgun;
    [SerializeField]
    private int maxRifle;
    [SerializeField]
    private int maxLmg;
    [SerializeField]
    private int maxSniper;
    [SerializeField]
    private int maxRail;
    [SerializeField]
    private int maxRocket;
    [SerializeField]
    private int maxBouncer;

    public float Attackrate = 5;
    private float nextTimeToAttack = 0;

    public float BulletSpeed = 400;

    public string nom;

    public AudioSource bruitage;
    public AudioClip shotgun;
    public AudioClip rifle;
    public AudioClip lmg;
    public AudioClip railgun;
    public AudioClip rpg;
    public AudioClip bouncer;

    public void Fire(Vector3 dir)
    {
        if (Time.time >= nextTimeToAttack /*&& currentAmmo > 0*/)
        {
            switch (nom)
            {
                case "shotgun":
                    Attackrate = 1;
                    nextTimeToAttack = (Time.time + 1f / Attackrate);

                    GameObject projectile1 = Instantiate(prefab) as GameObject;
                    GameObject projectile2 = Instantiate(prefab) as GameObject;
                    GameObject projectile3 = Instantiate(prefab) as GameObject;
                    GameObject projectile4 = Instantiate(prefab) as GameObject;
                    GameObject projectile5 = Instantiate(prefab) as GameObject;

                    projectile1.transform.position = muzzle.transform.position;
                    projectile2.transform.position = muzzle.transform.position;
                    projectile3.transform.position = muzzle.transform.position;
                    projectile4.transform.position = muzzle.transform.position;
                    projectile5.transform.position = muzzle.transform.position;

                    projectile1.GetComponent<basic_hit>().damage = 5;
                    projectile2.GetComponent<basic_hit>().damage = 5;
                    projectile3.GetComponent<basic_hit>().damage = 5;
                    projectile4.GetComponent<basic_hit>().damage = 5;
                    projectile5.GetComponent<basic_hit>().damage = 5;

                    float rng = Random.Range(-.25f, .5f);

                    Vector3 tmpdir1 = dir;
                    float tmpx = tmpdir1.x;
                    tmpdir1.x = tmpdir1.x * Mathf.Cos(rng) - tmpdir1.y * Mathf.Sin(rng);
                    tmpdir1.y = tmpx * Mathf.Sin(rng) + tmpdir1.y * Mathf.Cos(rng);

                    rng = Random.Range(-.25f, .5f);
                    Vector3 tmpdir2 = dir;
                    tmpx = tmpdir2.x;
                    tmpdir2.x = tmpdir2.x * Mathf.Cos(rng) - tmpdir2.y * Mathf.Sin(rng);
                    tmpdir2.y = tmpx * Mathf.Sin(rng) + tmpdir2.y * Mathf.Cos(rng);

                    rng = Random.Range(-.25f, .5f);
                    Vector3 tmpdir3 = dir;
                    tmpx = tmpdir3.x;
                    tmpdir3.x = tmpdir3.x * Mathf.Cos(rng) - tmpdir3.y * Mathf.Sin(rng);
                    tmpdir3.y = tmpx * Mathf.Sin(rng) + tmpdir3.y * Mathf.Cos(rng);

                    rng = Random.Range(-.25f, .5f);
                    Vector3 tmpdir4 = dir;
                    tmpx = tmpdir1.x;
                    tmpdir4.x = tmpdir4.x * Mathf.Cos(rng) - tmpdir4.y * Mathf.Sin(rng);
                    tmpdir4.y = tmpx * Mathf.Sin(rng) + tmpdir4.y * Mathf.Cos(rng);

                    rng = Random.Range(-.25f, .5f);
                    Vector3 tmpdir5 = dir;
                    tmpx = tmpdir5.x;
                    tmpdir5.x = tmpdir5.x * Mathf.Cos(rng) - tmpdir5.y * Mathf.Sin(rng);
                    tmpdir5.y = tmpx * Mathf.Sin(rng) + tmpdir5.y * Mathf.Cos(rng);

                    projectile1.GetComponent<basic_hit>().damage = 5;
                    projectile2.GetComponent<basic_hit>().damage = 5;
                    projectile3.GetComponent<basic_hit>().damage = 5;
                    projectile4.GetComponent<basic_hit>().damage = 5;
                    projectile5.GetComponent<basic_hit>().damage = 5;

                    projectile1.GetComponent<basic_hit>().player = transform.parent.GetComponent<PlayerStatus>();
                    projectile2.GetComponent<basic_hit>().player = transform.parent.GetComponent<PlayerStatus>();
                    projectile3.GetComponent<basic_hit>().player = transform.parent.GetComponent<PlayerStatus>();
                    projectile4.GetComponent<basic_hit>().player = transform.parent.GetComponent<PlayerStatus>();
                    projectile5.GetComponent<basic_hit>().player = transform.parent.GetComponent<PlayerStatus>();

                    bruitage.clip = shotgun;
                    bruitage.Play();

                    projectile1.transform.rotation = Quaternion.Euler(tmpdir1 + muzzle.transform.rotation.eulerAngles);
                    projectile2.transform.rotation = Quaternion.Euler(tmpdir2 + muzzle.transform.rotation.eulerAngles);
                    projectile3.transform.rotation = Quaternion.Euler(tmpdir3 + muzzle.transform.rotation.eulerAngles);
                    projectile4.transform.rotation = Quaternion.Euler(tmpdir4 + muzzle.transform.rotation.eulerAngles);
                    projectile5.transform.rotation = Quaternion.Euler(tmpdir5 + muzzle.transform.rotation.eulerAngles);

                    projectile1.GetComponent<Rigidbody2D>().AddForce(tmpdir1 * BulletSpeed);
                    projectile2.GetComponent<Rigidbody2D>().AddForce(tmpdir2 * BulletSpeed);
                    projectile3.GetComponent<Rigidbody2D>().AddForce(tmpdir3 * BulletSpeed);
                    projectile4.GetComponent<Rigidbody2D>().AddForce(tmpdir4 * BulletSpeed);
                    projectile5.GetComponent<Rigidbody2D>().AddForce(tmpdir5 * BulletSpeed);
                    currentAmmo--;
                    break;

                case "rifle":
                    Attackrate = 10;
                    BulletSpeed = 450;
                    nextTimeToAttack = (Time.time + 1f / Attackrate);

                    GameObject projectile = Instantiate(prefab) as GameObject;

                    projectile.transform.position = muzzle.transform.position;
                    projectile.transform.rotation = Quaternion.Euler(dir + muzzle.transform.rotation.eulerAngles);

                    projectile.GetComponent<basic_hit>().damage = 10;
                    projectile.GetComponent<basic_hit>().player = transform.parent.GetComponent<PlayerStatus>();

                    bruitage.clip = rifle;
                    bruitage.Play();

                    projectile.GetComponent<Rigidbody2D>().AddForce(dir * BulletSpeed);
                    currentAmmo--;
                    break;

                case "lmg":
                    Attackrate = 50;
                    BulletSpeed = 300;
                    nextTimeToAttack = (Time.time + 1f / Attackrate);

                    projectile = Instantiate(prefab) as GameObject;

                    projectile.transform.position = this.transform.position;

                    rng = Random.Range(-.5f, .5f);

                    Debug.Log(rng);

                    Vector3 tmpdir = dir;
                    tmpx = tmpdir.x;
                    tmpdir.x = tmpdir.x * Mathf.Cos(rng) - tmpdir.y * Mathf.Sin(rng);
                    tmpdir.y = tmpx * Mathf.Sin(rng) + tmpdir.y * Mathf.Cos(rng);

                    projectile.GetComponent<basic_hit>().damage = 1;
                    projectile.GetComponent<basic_hit>().player = transform.parent.GetComponent<PlayerStatus>();

                    bruitage.clip = lmg;
                    bruitage.PlayOneShot(lmg);


                    projectile.transform.rotation = Quaternion.Euler(tmpdir + muzzle.transform.rotation.eulerAngles);

                    projectile.GetComponent<Rigidbody2D>().AddForce(tmpdir * BulletSpeed);
                    currentAmmo--;
                    break;

                case "sniper":
                    Attackrate = .5f;
                    BulletSpeed = 800;
                    nextTimeToAttack = (Time.time + 1f / Attackrate);

                    projectile = Instantiate(prefab) as GameObject;

                    projectile.transform.position = this.transform.position;

                    projectile.GetComponent<basic_hit>().damage = 50;
                    projectile.GetComponent<basic_hit>().player = transform.parent.GetComponent<PlayerStatus>();

                    projectile.GetComponent<Rigidbody2D>().AddForce(dir * BulletSpeed);
                    currentAmmo--;
                    break;

                case "railgun":
                    Attackrate = 1f;
                    BulletSpeed = 500;
                    nextTimeToAttack = (Time.time + 1f / Attackrate);

                    projectile = Instantiate(prefab1) as GameObject;
                    Physics2D.IgnoreLayerCollision(10, 8);

                    projectile.transform.position = this.transform.position;

                    projectile.GetComponent<railgun_hit>().damage = 10;
                    projectile.GetComponent<railgun_hit>().player = transform.parent.GetComponent<PlayerStatus>();

                    bruitage.clip = railgun;
                    bruitage.Play();

                    projectile.GetComponent<Rigidbody2D>().AddForce(dir * BulletSpeed);
                    currentAmmo--;
                    break;

                case "rpg":
                    Attackrate = .5f;
                    BulletSpeed = 300;
                    nextTimeToAttack = (Time.time + 1f / Attackrate);

                    projectile = Instantiate(prefab2) as GameObject;

                    projectile.transform.position = this.transform.position;

                    projectile.GetComponent<rocket_hit>().damage = 100;
                    projectile.GetComponent<rocket_hit>().player = transform.parent.GetComponent<PlayerStatus>();

                    bruitage.clip = rpg;
                    bruitage.Play();

                    projectile.GetComponent<Rigidbody2D>().AddForce(dir * BulletSpeed);
                    currentAmmo--;
                    break;

                case "bouncer":
                    Attackrate = .5f;
                    BulletSpeed = 300;
                    nextTimeToAttack = (Time.time + 1f / Attackrate);

                    projectile = Instantiate(prefab3) as GameObject;

                    projectile.transform.position = this.transform.position;

                    projectile.GetComponent<ball_hit>().damage = 100;
                    projectile.GetComponent<ball_hit>().player = transform.parent.GetComponent<PlayerStatus>();

                    bruitage.clip = bouncer;
                    bruitage.Play();

                    projectile.GetComponent<Rigidbody2D>().AddForce(dir * BulletSpeed);
                    currentAmmo--;
                    break;
                default:
                    break;
            }
            if (currentAmmo == 0)
            {
                nom = "";
            }
        }
    }

    public void ChargeAmmo()
    {
        switch (nom)
        {
            case "shotgun":
                currentAmmo = maxShotgun;
                break;
            case "rifle":
                currentAmmo = maxRifle;
                break;
            case "lmg":
                currentAmmo = maxLmg;
                break;
            case "sniper":
                currentAmmo = maxSniper;
                break;
            case "railgun":
                currentAmmo = maxRail;
                break;
            case "rpg":
                currentAmmo = maxRocket;
                break;
            case "bouncer":
                currentAmmo = maxBouncer;
                break;
        }
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }
}

