using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

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
    [SerializeField] private float powerUpDuration;
    [SerializeField] private bool hasShieldPowerUp;
    [SerializeField] private bool hasShotPowerUp;
    [SerializeField] private bool isTakingDamage = false;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        shieldGameObject.GetComponent<SpriteRenderer>().enabled = false;
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
        hasShieldPowerUp = true;
        shieldGameObject.GetComponent<SpriteRenderer>().enabled = true;

        yield return new WaitForSeconds(powerUpDuration);

        hasShieldPowerUp = false;
        shieldGameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    IEnumerator BoostShot()
    {
        hasShotPowerUp = true;
        GetComponent<PlayerShooting>().ActivatePowerUp();

        yield return new WaitForSeconds(powerUpDuration);

        hasShotPowerUp = false;
        GetComponent<PlayerShooting>().DeactivatePowerUp();
    }

    private void Heal()
        {
            if(health < maxHealth)
            {
                health += 20;
                Debug.Log("HEAL!! Health: " + health);
            }
        }
    private void TakeDamage()
    {

        if (!hasShieldPowerUp && !isTakingDamage)
        {
            isTakingDamage = true;
            health -= 20;
            Debug.Log("Health: " + health);
        }
        else if(hasShieldPowerUp) 
        {
            Debug.Log("Protected by shield!");
        }
        
        
        if (health <= 0)
        {
            Debug.Log("GAME OVER");
            Destroy(this.gameObject);
        }
    }
}
