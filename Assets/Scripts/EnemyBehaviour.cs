using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private string tag = "ShotPlayer";
    [SerializeField] private float speed;
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private float cooldown;
    [SerializeField] private GameObject shotSpawpoint;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(tag))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
