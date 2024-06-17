using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer render;
    Animator animator;
    
    Color originalColor;

    public Scanner scanner;
    AttackInRange attackInRange;
    PlayerFire playerFire;

    public float hp;
    public float maxHp;
    [SerializeField]
    float speed;

    public Vector2 inputVec;

    bool isDamaged;
    float timer;
    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        render = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();

        scanner = GetComponent<Scanner>();
        attackInRange = GetComponentInChildren<AttackInRange>();
        playerFire = GetComponent<PlayerFire>();

        originalColor = render.color;

        maxHp = 3.0f;
        hp = maxHp;
        speed = 3.0f;

        isDamaged = false;
        timer = 0f;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
            return;

        if (isDamaged)
        {
            timer += Time.deltaTime;

            if (timer >= 0.7f)
            {
                isDamaged = false;
                timer = 0f;
            }
        }

        inputVec.x = Input.GetAxis("Horizontal");
        inputVec.y = Input.GetAxis("Vertical");

        IsMoving();
        IsDeading();
    }

    private void FixedUpdate() 
    {
        if (isDead)
            return;

        Vector2 moveVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + moveVec);

    }

    private void LateUpdate() 
    {
        if (inputVec.x != 0)
        {
            render.flipX = inputVec.x < 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.transform.CompareTag("Enemy"))
        {
            OnDamaged();
        }
    }

    void OnDamaged()
    {
        if (isDamaged)
        {
            return;
        }

        hp--;
        isDamaged = true;

        StartCoroutine(Flash());
    }

    void IsMoving()
    {
        if (inputVec.magnitude != 0)
        {
            animator.SetBool("isMoving", true);
        } else
        {
            animator.SetBool("isMoving", false);
        }
    }

    void IsDeading()
    {
        if (hp > 0)
            return;

        isDead = true;
        animator.SetBool("isDead", true);

        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = 0;
        rigid.isKinematic = true;

        StartCoroutine(ShowGameOverUIAfterDelay());
    }

    public void IncreaseStat(LevelUpUI.StatType statType)
    {
        switch (statType)
        {
            case LevelUpUI.StatType.Speed:
                speed++;
                Debug.Log("Speed Increase to " + speed);
                break;
            case LevelUpUI.StatType.Slash_Damage:
                attackInRange.attackDamage++;
                Debug.Log("Slash_Damage to " + attackInRange.attackDamage);
                break;
            case LevelUpUI.StatType.Bullet_Damage:
                
                Debug.Log("Bullet_Damage to ");
                break;
            case LevelUpUI.StatType.Slash_Term:
                attackInRange.attackInterval -= 0.2f;
                Debug.Log("Slash_Term to " + attackInRange.attackInterval);
                break;
            case LevelUpUI.StatType.Fire_Term:
                playerFire.fireTerm -= 0.2f;
                Debug.Log("Fire_Term to " + playerFire.fireTerm);
                break;
            case LevelUpUI.StatType.Slash_Range:
                attackInRange.transform.localScale += new Vector3(0.2f, 0.2f);
                Debug.Log("Slash_Range to " + playerFire.fireTerm);
                break;
        }
    }

    IEnumerator Flash()
    {
        render.color = Color.blue;

        yield return new WaitForSeconds(0.2f);
        render.color = originalColor;
    }

    IEnumerator ShowGameOverUIAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);

        GameManager.instance.GameOverPanel.SetActive(true);
    }
}
