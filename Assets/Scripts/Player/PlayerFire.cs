using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    Scanner scanner;
    public float fireTerm = 1f;
    float fireDelay = 0f;

    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        scanner = GetComponent<Scanner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.player.isDead)
            return;

        float timer = GameManager.instance.timer;
        if (timer < fireDelay)
            return;

        fireDelay = timer + fireTerm;
        Fire();
    }

    
    void Fire()
    {
        if (!scanner.nearTarget)
            return;

        Vector3 targetPos = scanner.nearTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir.Normalize();

        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

        if (!bulletRigidbody)
            return;

        bulletRigidbody.AddForce(dir * 500f);
    }
}
