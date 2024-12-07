using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private bool keepShooting = true;
    [SerializeField] private string playerShotTag = "ShotPlayer";
    [SerializeField] private float speed;
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private GameObject shotPowerUpPrefab;
    [SerializeField] private GameObject shieldPowerUpPrefab;
    [SerializeField] private GameObject healingPowerUpPrefab;
    [SerializeField] private float cooldown;
    [SerializeField] private GameObject shotSpawpoint;
    [SerializeField] private bool hasPowerUp = false;
    [SerializeField] private bool isDying = false;
    [SerializeField] private GameObject ExplosionAnimationGameObject;

    [SerializeField] private AudioClip explosionClip;
    [SerializeField] private AudioClip ShootingClip;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime);
    }

    private void StopMoving()
    {
        speed = 0;
    }

    IEnumerator Shoot()
    {
        while (keepShooting)
        {
            Instantiate(shotPrefab, shotSpawpoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            audioSource.PlayOneShot(ShootingClip);
            yield return new WaitForSeconds(cooldown);
        }
    }
    private void StopShooting()
    {
        keepShooting = false;
    }
    public void GivePowerUp()
    {
        hasPowerUp = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerShotTag) && !isDying)
        {
            StartCoroutine( Die(collision));
        }
    }

    IEnumerator Die(Collider2D collision)
    {
        isDying = true;
        Destroy(collision.gameObject);
        StopMoving();
        StopShooting();

        GameObject deathExplosion = Instantiate(ExplosionAnimationGameObject, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        audioSource.PlayOneShot(explosionClip);
        yield return new WaitForSeconds(0.3f);
        Destroy(deathExplosion);

        if (hasPowerUp) DeployPowerUp();
        Destroy(this.gameObject);
    }

    private void DeployPowerUp()
    {
        int randomValue = UnityEngine.Random.Range(1, 4); // 1, 2, 3

        if(randomValue == 1)
        {
            Instantiate(shieldPowerUpPrefab, transform.position, Quaternion.identity);
        }
        else if(randomValue == 2)
        {
            Instantiate(shotPowerUpPrefab, transform.position, Quaternion.identity);
        }
        else if(randomValue == 3)
        {
            Instantiate(healingPowerUpPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
