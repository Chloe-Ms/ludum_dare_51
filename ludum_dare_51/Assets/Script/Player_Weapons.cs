using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public enum Weapon{
    Pistol,
    AssaultRifle,
    ShotGun
}

public class Player_Weapons : MonoBehaviour
{
    public float CurrentCharge = 3f;
    
    [SerializeField] 
    private GameObject bulletPrefab;
    private GameObject GrenadePrefab;
    public GameObject Explosion;
    public float bulletSpeed = 20f;

    public Weapon currentWeapon;

    public GameObject PistolModel;
    public GameObject AssaultRifleModel;
    public GameObject ShotGunModel;
    
    public GameObject pistol;
    public float damageWeaponPistol;
    public float rangeWeaponPistol;
    public float fireRateWeaponPistol;
    public float impactForceWeaponPistol;
    public float reloadTimeWeaponPistol;
    public bool isReloadingWeaponPistol;

    public GameObject weaponAssaultRifle;
    public float damageWeaponAssaultRifle;
    public float rangeWeaponAssaultRifle;
    public float fireRateWeaponAssaultRifle;
    public float reloadTimeWeaponAssaultRifle;
    public bool isReloadingWeaponAssaultRifle;
    
    public GameObject weaponShotGun;
    public float damageWeaponShotGun;
    public float rangeWeaponShotGun;
    public float fireRateWeaponShotGun;
    public float reloadTimeWeaponShotGun;
    public bool isReloadingWeaponShotGun;

    public int Direction;
    private int Spread;

    private bool GrenadeActive;
    private bool ShieldActive;

    private Quaternion aim;


    private void Start()
    {
    
    }

    IEnumerator Reload()
    {
        isReloadingWeaponPistol = true;
        yield return new WaitForSeconds(reloadTimeWeaponPistol);
        isReloadingWeaponPistol = false;
    }
    IEnumerator ReloadAssaultRifle()
    {
        isReloadingWeaponAssaultRifle = true;
        yield return new WaitForSeconds(reloadTimeWeaponAssaultRifle);
        isReloadingWeaponAssaultRifle = false;
    }
    IEnumerator ReloadShotGun()
    {
        isReloadingWeaponShotGun = true;
        yield return new WaitForSeconds(reloadTimeWeaponShotGun);
        isReloadingWeaponShotGun = false;
    }
    IEnumerator Rafale()
    {
        shoot();
        yield return new WaitForSeconds(0.1f);
        shoot();
        yield return new WaitForSeconds(0.1f);
        shoot();
    }
    IEnumerator ShotGun()
    {
        shootShotGun();
        yield return new WaitForSeconds(0.01f);
        shootShotGun();
        yield return new WaitForSeconds(0.01f);
        shootShotGun();
        yield return new WaitForSeconds(0.01f);
        shootShotGun();

        yield return new WaitForSeconds(5f);
        
    }
    IEnumerator Shield()
    {
        Player_Life vie = GetComponent<Player_Life>();
        Inventory inventory = GetComponent<Inventory>();
        vie.canTakeDamage = false;
        inventory.RemoveItem();
        yield return new WaitForSeconds(5f);
        vie.canTakeDamage =  true;
    }

    private IEnumerator Gren()
    {
        Player_move monSprite = GetComponent<Player_move>();
        Inventory inventory = GetComponent<Inventory>();
        GameObject[] Grenade;
        Grenade = GameObject.FindGameObjectsWithTag("Grenade");

        if (monSprite.facing == true)
        {
            Vector2 playerPos = new Vector2(inventory.player.position.x - 1 , inventory.player.position.y );
            GameObject drop = Instantiate(inventory.item, playerPos, Quaternion.identity);
            drop.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            Vector2 playerPos1 = new Vector2(inventory.player.position.x - 2, inventory.player.position.y + 1);
            GameObject drop1 = Instantiate(inventory.item, playerPos1, Quaternion.identity);
            drop.SetActive(false);
            drop1.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            Vector2 playerPos2 = new Vector2(inventory.player.position.x + 3, inventory.player.position.y + 1);
            GameObject drop2 = Instantiate(inventory.item, playerPos2, Quaternion.identity);
            drop.SetActive(false);
            drop1.SetActive(false);
            drop2.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            Vector2 playerPos3 = new Vector2(inventory.player.position.x, inventory.player.position.y - 4);
            GameObject drop3 = Instantiate(inventory.item, playerPos3, Quaternion.identity);
            drop.SetActive(false);
            drop1.SetActive(false);
            drop2.SetActive(false);
            drop3.SetActive(true);
            inventory.RemoveItem();
            yield return new WaitForSeconds(2f);
            drop3.SetActive(false);




            GameObject Explode = Instantiate(Explosion, playerPos3, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            Destroy(Explode);
            Destroy(drop);
            Destroy(drop1);
            Destroy(drop2);
            Destroy(drop3);


        }
        else
        {
            Vector2 playerPos = new Vector2(inventory.player.position.x + 1, inventory.player.position.y);
            GameObject drop = Instantiate(inventory.item, playerPos, Quaternion.identity);
            drop.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            Vector2 playerPos1 = new Vector2(inventory.player.position.x + 2, inventory.player.position.y + 1);
            GameObject drop1 = Instantiate(inventory.item, playerPos1, Quaternion.identity);
            drop.SetActive(false);
            drop1.SetActive(true);
            
            yield return new WaitForSeconds(0.1f);
            Vector2 playerPos2 = new Vector2(inventory.player.position.x + 3, inventory.player.position.y + 1);
            GameObject drop2 = Instantiate(inventory.item, playerPos2, Quaternion.identity);
            drop.SetActive(false);
            drop1.SetActive(false);
            drop2.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            Vector2 playerPos3 = new Vector2(inventory.player.position.x + 4, inventory.player.position.y);
            GameObject drop3 = Instantiate(inventory.item, playerPos3, Quaternion.identity);
            drop.SetActive(false);
            drop1.SetActive(false);
            drop2.SetActive(false);
            drop3.SetActive(true);
            inventory.RemoveItem();
            yield return new WaitForSeconds(2f);
            drop3.SetActive(false);




            GameObject Explode = Instantiate(Explosion, playerPos3, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            Destroy(Explode);
            Destroy(drop);
            Destroy(drop1);
            Destroy(drop2);
            Destroy(drop3);
            
        }
        
        
    }

        // Update is called once per frame
    void Update()
    {
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 difference = target - transform.position;
        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        aim = Quaternion.Euler(0f, 0f, angle - 90);

        Inventory inventory = GetComponent<Inventory>();
        
        GameObject[] Grenade;
        Grenade = GameObject.FindGameObjectsWithTag("Grenade");
        if (GrenadeActive)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ShotGrenade();
                GrenadeActive = false;
            }
            
        }
        else if (ShieldActive)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                UseShield();
            }
        }


        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == "Weapon")
            {
                if (collider.gameObject.name == "Pistol")
                {
                    pistol.SetActive(false);
                    currentWeapon = Weapon.Pistol;
                }
                if (collider.gameObject.name == "AssaultRifle")
                {
                    weaponAssaultRifle.SetActive(false);
                    currentWeapon = Weapon.AssaultRifle;
                }
                if (collider.gameObject.name == "fusil")
                {
                    currentWeapon = Weapon.ShotGun;
                }
            }
        }

        if (currentWeapon == Weapon.Pistol && !isReloadingWeaponPistol)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                if (CurrentCharge > 0)
                {
                    shoot();
                    CurrentCharge--;
                }
                else
                {
                    StartCoroutine(Reload());
                }
            }
        }
        if (currentWeapon == Weapon.AssaultRifle && !isReloadingWeaponAssaultRifle)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                if (CurrentCharge > 0)
                {
                    StartCoroutine(Rafale());
                    CurrentCharge--;
                }
                else
                {
                    StartCoroutine(ReloadAssaultRifle());
                }
            }
        }
        if (currentWeapon == Weapon.ShotGun && !isReloadingWeaponShotGun)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                if (CurrentCharge > 0)
                {
                    StartCoroutine(ShotGun());
                    CurrentCharge--;
                }
                else
                {
                    StartCoroutine(ReloadShotGun());
                }
            }
        }

    }

    void ShotGrenade()
    {
        StartCoroutine(Gren());
    }
    void shoot()
    {
        float spread = Random.Range(-2, 2);
        GameObject bullet = Instantiate(bulletPrefab, transform.position, aim);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(spread, bulletSpeed));
    }
    void shootShotGun()
    {
        float spread = Random.Range(-3, 3);
        GameObject bullet = Instantiate(bulletPrefab, transform.position, aim);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(spread, bulletSpeed));
    }
    void UseShield()
    {
        StartCoroutine(Shield());
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Grenade")
        {
            GrenadeActive = true;
        }
        if (collision.gameObject.tag == "Shield")
        {
            ShieldActive = true;
        }
    }
}
