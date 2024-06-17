using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 6;
    int damage = 2;

    float timer;

    Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 10.0f)
            Destroy(gameObject);

        timer += Time.deltaTime;

        rigid.AddForce(transform.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        // Debug.Log("Trigger entered with: " + other.name);

        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().OnDamaged(damage);
            Destroy(gameObject);
            // Debug.Log("Hit!");
        }
    }
}
