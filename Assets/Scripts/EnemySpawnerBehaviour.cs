using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawnerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private TextMeshProUGUI textLevel;
    [SerializeField] private float upperLimit;
    [SerializeField] private float lowerLimit;
    [SerializeField] private float cooldown;
    [SerializeField] private float enemyQuantity;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemyQuantity; i++)
        {
            float randomY = Random.Range(upperLimit, lowerLimit);
            Instantiate(enemyPrefab, new Vector3(transform.position.x, randomY, 0), Quaternion.identity);
            yield return new WaitForSeconds(cooldown); 
        }
        
    }
}
