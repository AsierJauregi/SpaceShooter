using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    [SerializeField] private string tagEnemy = "Enemy";
    [SerializeField] private float speed;
    [SerializeField] private string tag = "ShotEnemy";
    [SerializeField] private float health;
    [SerializeField] private TextMeshProUGUI textHealth;

    // Start is called before the first frame update
    void Start()
    {
        
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
        if (collision.gameObject.CompareTag(tag) || collision.gameObject.CompareTag(tagEnemy))
        {
            health -= 20;
            Debug.Log("Helth: " + health);
            Destroy(collision.gameObject);
            if(health <= 0)
            {
                Debug.Log("GAME OVER");
                Destroy(this.gameObject);
            }
        }
    }
}
