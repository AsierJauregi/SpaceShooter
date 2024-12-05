using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private float cooldown;
    [SerializeField] private GameObject upperSpawpoint;
    [SerializeField] private GameObject lowerSpawpoint;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
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
            Instantiate(shotPrefab, upperSpawpoint.transform.position, Quaternion.identity);
            Instantiate(shotPrefab, lowerSpawpoint.transform.position, Quaternion.identity);
            timer = 0;
        }
    }
}
