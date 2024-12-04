using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;

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
}
