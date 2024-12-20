using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerLogic : MonoBehaviour
{
    [SerializeField] private string shieldPowerUpTag = "ShieldPowerUp";
    [SerializeField] private string shotPowerUpTag = "ShotPowerUp";
    [SerializeField] private string healingPowerUpTag = "HealingPowerUp";
    [SerializeField] private string enemyTag = "Enemy";
    [SerializeField] private string shotEnemyTag = "ShotEnemy";
    [SerializeField] private float speed;
    private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private TextMeshProUGUI textHealth;
    [SerializeField] private GameObject shieldGameObject;
    [SerializeField] private GameObject ExplosionAnimationGameObject;
    [SerializeField] private float powerUpDuration;
    private float pickedShieldPowerUpp = 0;
    private float pickedShotPowerUp = 0;
    [SerializeField] private bool isTakingDamage = false;

    [SerializeField] private AudioClip healingClip;
    [SerializeField] private AudioClip shieldClip;
    [SerializeField] private AudioClip explosionClip;
    private AudioSource audioSource;

    [SerializeField] private GameObject heartVisual1;
    [SerializeField] private GameObject heartVisual2;
    [SerializeField] private GameObject heartVisual3;
    [SerializeField] private GameObject heartVisual4;
    [SerializeField] private GameObject heartVisual5;

    [SerializeField] private GameObject enemySpawner;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        shieldGameObject.GetComponent<SpriteRenderer>().enabled = false;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        DelimitMovement();
    }

    private void Move()
    {
        float inputH = Input.GetAxisRaw("Horizontal");
        float inputV = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector2(inputH, inputV).normalized * speed * Time.deltaTime);
    }
    private void StopMoving()
    {
        speed = 0;
    }

    private void DelimitMovement()
    {
        float xClamped = Mathf.Clamp(transform.position.x, -8.24f, 8.24f);
        float yClamped = Mathf.Clamp(transform.position.y, -4.53f, 4.53f);
        transform.position = new Vector3(xClamped, yClamped, 0);
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(shotEnemyTag) || collision.gameObject.CompareTag(enemyTag))
        {
            Destroy(collision.gameObject);
            TakeDamage();
        }
        else if (collision.gameObject.CompareTag(healingPowerUpTag))
        {
            Destroy(collision.gameObject);
            Heal();
        }
        else if (collision.gameObject.CompareTag(shotPowerUpTag))
        {
            Destroy(collision.gameObject);
            StartCoroutine(BoostShot());
        }
        else if (collision.gameObject.CompareTag(shieldPowerUpTag))
        {
            Destroy(collision.gameObject);
            StartCoroutine(ActivateShield());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(shotEnemyTag) || collision.gameObject.CompareTag(enemyTag))
        {
            isTakingDamage = false;
        }
    }
    IEnumerator ActivateShield()
    {
        pickedShieldPowerUpp++;
        shieldGameObject.GetComponent<SpriteRenderer>().enabled = true;
        audioSource.PlayOneShot(shieldClip);

        yield return new WaitForSeconds(powerUpDuration);

        pickedShieldPowerUpp--;
        if (pickedShieldPowerUpp == 0) shieldGameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    IEnumerator BoostShot()
    {
        pickedShotPowerUp++;
        GetComponent<PlayerShooting>().ActivatePowerUp();

        yield return new WaitForSeconds(powerUpDuration);

        pickedShotPowerUp--;
        if(pickedShotPowerUp == 0) GetComponent<PlayerShooting>().DeactivatePowerUp();
    }

    private void Heal()
        {
            if(health < maxHealth)
            {
                health += 20;
                audioSource.PlayOneShot(healingClip);
                Debug.Log("HEAL!! Health: " + health);
                UpdateHealthUI();
            }
        }
    private void TakeDamage()
    {

        if (pickedShieldPowerUpp == 0 && !isTakingDamage)
        {
            isTakingDamage = true;
            health -= 20;
            Debug.Log("Health: " + health);
            UpdateHealthUI();
        }
        else if(pickedShieldPowerUpp > 0) 
        {
            Debug.Log("Protected by shield!");
        }
        
        
        if (health <= 0)
        {
            UpdateHealthUI();
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        Debug.Log("GAME OVER");
        StopMoving();
        GameObject deathExplosion = Instantiate(ExplosionAnimationGameObject, transform.position, Quaternion.identity);
        audioSource.PlayOneShot(explosionClip);
        yield return new WaitForSeconds(0.7f);
        Destroy(deathExplosion);
        enemySpawner.GetComponent<EnemySpawnerBehaviour>().StopSpawning();
        Destroy(this.gameObject);
    }

    private void UpdateHealthUI()
    {
        if (health < maxHealth)
        {
            if(health == 80)
            {
                heartVisual1.GetComponent<SpriteRenderer>().enabled = true;
                heartVisual2.GetComponent<SpriteRenderer>().enabled = true;
                heartVisual3.GetComponent<SpriteRenderer>().enabled = true; 
                heartVisual4.GetComponent<SpriteRenderer>().enabled = true; 
                heartVisual5.GetComponent<SpriteRenderer>().enabled = false; 
            }else if(health == 60)
            {
                heartVisual1.GetComponent<SpriteRenderer>().enabled = true;
                heartVisual2.GetComponent<SpriteRenderer>().enabled = true;
                heartVisual3.GetComponent<SpriteRenderer>().enabled = true;
                heartVisual4.GetComponent<SpriteRenderer>().enabled = false;
                heartVisual5.GetComponent<SpriteRenderer>().enabled = false;
            }
            else if (health == 40)
            {
                heartVisual1.GetComponent<SpriteRenderer>().enabled = true;
                heartVisual2.GetComponent<SpriteRenderer>().enabled = true;
                heartVisual3.GetComponent<SpriteRenderer>().enabled = false;
                heartVisual4.GetComponent<SpriteRenderer>().enabled = false;
                heartVisual5.GetComponent<SpriteRenderer>().enabled = false;
            }
            else if (health == 20)
            {
                heartVisual1.GetComponent<SpriteRenderer>().enabled = true;
                heartVisual2.GetComponent<SpriteRenderer>().enabled = false;
                heartVisual3.GetComponent<SpriteRenderer>().enabled = false;
                heartVisual4.GetComponent<SpriteRenderer>().enabled = false;
                heartVisual5.GetComponent<SpriteRenderer>().enabled = false;
            }
            else if (health == 0)
            {
                heartVisual1.GetComponent<SpriteRenderer>().enabled = false;
                heartVisual2.GetComponent<SpriteRenderer>().enabled = false;
                heartVisual3.GetComponent<SpriteRenderer>().enabled = false;
                heartVisual4.GetComponent<SpriteRenderer>().enabled = false;
                heartVisual5.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        else
        {
            heartVisual1.GetComponent<SpriteRenderer>().enabled = true;
            heartVisual2.GetComponent<SpriteRenderer>().enabled = true;
            heartVisual3.GetComponent<SpriteRenderer>().enabled = true;
            heartVisual4.GetComponent<SpriteRenderer>().enabled = true;
            heartVisual5.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
