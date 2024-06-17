using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    [SerializeField]
    int maxHp = 10;
    [SerializeField]
    float speed = 2.0f;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer render;

    Color originalColor;

    float knockbackForce = 5.0f;
    bool isKnockback;
    bool isDead{get; set;}

    Rigidbody2D target;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();

        // speed = 2.0f;
        isKnockback = false;
        originalColor = render.color;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDead)
            return;

        Vector2 dirVec = target.position - rigid.position;
        Vector2 moveVec = dirVec.normalized * speed * Time.fixedDeltaTime;

        rigid.MovePosition(rigid.position + moveVec);
        rigid.velocity = Vector2.zero;
    }

    void LateUpdate() 
    {
        if (isDead)
            return;

        render.flipX = target.position.x < rigid.position.x;

        if (hp <= 0)
        {
            isDead = true;

            render.color = originalColor;
            isKnockback = false;

            // GameManager.instance.kill++;
            GameManager.instance.IsKill();

            GameManager.instance.attackInRange.RemoveList(gameObject);
            gameObject.SetActive(false);
        }
    }

    void OnEnable() 
    {
        hp = maxHp;
        isDead = false;

        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
    }

    public void OnDamaged(int damage)
    {
        hp -= damage;

        Vector2 attackPos = GameManager.instance.player.transform.position;
        Vector2 knockbackDir = (rigid.position - attackPos).normalized;
        rigid.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);

        if (!isKnockback)
        {
            StartCoroutine(FlashRed());
            isKnockback = true;
        }
    }

    IEnumerator FlashRed()
    {
        render.color = Color.red;

        yield return new WaitForSeconds(0.1f);
        render.color = originalColor;
        isKnockback = false;
    }
}
