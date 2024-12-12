using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawnerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private TextMeshProUGUI textLevel;
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private float upperLimit;
    [SerializeField] private float lowerLimit;
    [SerializeField] private float cooldown;
    [SerializeField] private float enemyQuantity;
    [SerializeField] private float waveQuantity; 
    [SerializeField] private float levelQuantity;  
    private float playerScore = 0;

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
                textLevel.text = "Level " + (i + 1) + " - Wave " + (j + 1);
                for (int k = 0; k < enemyQuantity; k++)
                {
                    float randomY = Random.Range(upperLimit, lowerLimit);
                    GameObject newEnemy = Instantiate(enemyPrefab, new Vector3(transform.position.x, randomY, 0), Quaternion.identity, this.transform);

                    int randomValue = Random.Range(1, 5); // 1, 2, 3, 4
                    if (randomValue == 1) newEnemy.GetComponent<EnemyBehaviour>().GivePowerUp();

                    yield return new WaitForSeconds(cooldown);
                    textLevel.text = "";
                }
            }
            
        }
        
    }

    public void UpdateScore(float points)
    {
        playerScore += points;
        textScore.text = "Score: " + playerScore;
    }
}
