using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
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

    IEnumerator Shoot()
    {
        while (true)
        {
            Instantiate(shotPrefab, shotSpawpoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(cooldown);
        }
    }
    public void GivePowerUp()
    {
        hasPowerUp = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerShotTag) && !isDying)
        {
            isDying = true;
            Destroy(collision.gameObject);
            if (hasPowerUp) DeployPowerUp();
            Destroy(this.gameObject);
        }
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
