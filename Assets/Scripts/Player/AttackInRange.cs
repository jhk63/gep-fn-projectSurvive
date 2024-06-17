using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackInRange : MonoBehaviour
{
    public float attackInterval = 2.0f;
    public int attackDamage = 5;

    public GameObject attackEffectPrefab;

    [SerializeField]
    List<GameObject> enemyInRange = new List<GameObject>();

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
    }

    void Update() 
    {
        if (GameManager.instance.player.isDead)
            return;

        timer += Time.deltaTime;

        if (timer >= attackInterval)
        {
            timer = 0.0f;
            Attack();
        }
    }

    void Attack()
    {
        if (enemyInRange.Count <= 0)
            return;


        foreach (GameObject enemy in enemyInRange)
        {
            if (enemy != null)
            {
                Instantiate(attackEffectPrefab, enemy.transform.position, Quaternion.identity);

                Enemy target = enemy.GetComponent<Enemy>();
                target.OnDamaged(attackDamage);
            }
        }
    }

    public void RemoveList(GameObject enemy)
    {
        if (enemyInRange.Contains(enemy))
        {
            enemyInRange.Remove(enemy);
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Enemy"))
        {
            enemyInRange.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Enemy"))
        {
            enemyInRange.Remove(other.gameObject);
        }
    }
}
