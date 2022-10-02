using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Player_Weapons : MonoBehaviour
{
    public float CurrentCharge = 3f;
    
    [SerializeField] 
    private GameObject bulletPrefab;
    public float bulletSpeed = 20f;

    public GameObject currentWeapon;

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
        

    private void Start()
    {
        PistolModel.SetActive(false);
        AssaultRifleModel.SetActive(false);
        ShotGunModel.SetActive(false);
        
    }

    IEnumerator Reload()
    {
        isReloadingWeaponPistol = true;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTimeWeaponPistol);
        isReloadingWeaponPistol = false;
    }
    IEnumerator ReloadAssaultRifle()
    {
        isReloadingWeaponAssaultRifle = true;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTimeWeaponAssaultRifle);
        isReloadingWeaponAssaultRifle = false;
    }
    IEnumerator ReloadShotGun()
    {
        isReloadingWeaponShotGun = true;
        Debug.Log("Reloading...");
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

    // Update is called once per frame
    void Update()
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == "Weapon")
            {
                if (collider.gameObject.name == "Pistol")
                {
                    pistol.SetActive(false);
                    gameObject.GetComponent<Player_Weapons>().currentWeapon = pistol;
                    PistolModel.SetActive(true);
                    AssaultRifleModel.SetActive(false);
                    ShotGunModel.SetActive(false);
                }
                if (collider.gameObject.name == "AssaultRifle")
                {
                    weaponAssaultRifle.SetActive(false);
                    gameObject.GetComponent<Player_Weapons>().currentWeapon = weaponAssaultRifle;
                    PistolModel.SetActive(false);
                    AssaultRifleModel.SetActive(true);
                    ShotGunModel.SetActive(false);
                }
                if (collider.gameObject.name == "fusil")
                {
                    weaponShotGun.SetActive(false);
                    gameObject.GetComponent<Player_Weapons>().currentWeapon = weaponShotGun;
                    PistolModel.SetActive(false);
                    AssaultRifleModel.SetActive(false);
                    ShotGunModel.SetActive(true);
                }
            }
        }

        if (currentWeapon == pistol && !isReloadingWeaponPistol)
        {
            if (Input.GetButtonDown("Fire1"))
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
        if (currentWeapon == weaponAssaultRifle && !isReloadingWeaponAssaultRifle)
        {
            if (Input.GetButtonDown("Fire1"))
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
        if (currentWeapon == weaponShotGun && !isReloadingWeaponShotGun)
        {
            if (Input.GetButtonDown("Fire1"))
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
    void shoot()
    {
        Spread = Random.Range(1, -2);
        Player_move monSprite = GetComponent<Player_move>();
        if (monSprite.facing == true)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletSpeed, Spread);
            Direction = -1;

        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, Spread);
            Direction = 1;

        }
        
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, new Vector2(Direction, 0) * transform.localScale.y, rangeWeaponPistol);
        Debug.DrawRay(transform.position, new Vector2(Direction, 0) * transform.localScale.y, Color.red, 10f);
        if (hitInfo)
        {
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
            Debug.Log(hitInfo.transform.name);
            if (enemy != null)
            {
                enemy.TakeDamage(damageWeaponPistol);
                Debug.Log("Hit");
            }
        }
    }

    void shootShotGun()
    {
        Spread = Random.Range(1, -2);
        Player_move monSprite = GetComponent<Player_move>();
        if (monSprite.facing == true)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletSpeed, Spread);
            Direction = -1;
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, Spread);
            Direction = 1;
        }
        
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, new Vector2(Direction, 0) * transform.localScale.y, rangeWeaponShotGun);
        Debug.DrawRay(transform.position, new Vector2(Direction, 0) * transform.localScale.y, Color.red, 10f);
        if (hitInfo)
        {
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
            Debug.Log(hitInfo.transform.name);
            if (enemy != null)
            {
                enemy.TakeDamage(damageWeaponShotGun);
                Debug.Log("Hit");
            }
        }
    }
}
