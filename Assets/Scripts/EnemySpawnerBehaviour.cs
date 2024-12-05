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
    [SerializeField] private float waveQuantity; 
    [SerializeField] private float levelQuantity; 

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
        for (int i = 0; i < levelQuantity; i++)
        {
            for (int j = 0; j < waveQuantity; j++)
            {
                textLevel.text = "Nivel " + (i + 1) + " - Oleada " + (j + 1);
                for (int k = 0; k < enemyQuantity; k++)
                {
                    float randomY = Random.Range(upperLimit, lowerLimit);
                    Instantiate(enemyPrefab, new Vector3(transform.position.x, randomY, 0), Quaternion.identity);
                    yield return new WaitForSeconds(cooldown);
                    textLevel.text = "";
                }
            }
            
        }
        
    }
}
