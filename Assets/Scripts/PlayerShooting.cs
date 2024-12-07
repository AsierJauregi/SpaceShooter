using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private GameObject shotPowerUpPrefab;
    [SerializeField] private float cooldown;
    [SerializeField] private GameObject upperSpawpoint;
    [SerializeField] private GameObject middleSpawpoint;
    [SerializeField] private GameObject lowerSpawpoint;
    [SerializeField] private bool hasShotPowerUp = false;
    private float timer = 0;

    [SerializeField] private AudioClip shootingClip;
    [SerializeField] private AudioClip shotPowerUpClip;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        timer += 1 * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && timer > cooldown)
        {
            if (!hasShotPowerUp)
            {
                Instantiate(shotPrefab, upperSpawpoint.transform.position, Quaternion.identity);
                Instantiate(shotPrefab, lowerSpawpoint.transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(shotPrefab, upperSpawpoint.transform.position, Quaternion.Euler(0,0,45));
                Instantiate(shotPrefab, middleSpawpoint.transform.position, Quaternion.identity);
                Instantiate(shotPrefab, lowerSpawpoint.transform.position, Quaternion.Euler(0,0,-45));
            }
            audioSource.PlayOneShot(shootingClip);
            timer = 0;
        }
    }

    public void ActivatePowerUp()
    {
        hasShotPowerUp = true;
        audioSource.PlayOneShot(shotPowerUpClip);
    }
    public void DeactivatePowerUp() 
    {
        hasShotPowerUp = false;
    }

}
