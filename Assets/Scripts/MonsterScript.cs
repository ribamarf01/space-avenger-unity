using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    private int monsterHP;
    private Rigidbody2D rb;
    private GameObject player;
    private float ms;
    private Vector3 playerDir;
    private Vector3 localscale;
    private GameObject status;
    private bool facingRight;
    public Animator animator;
    public GameObject ammoDrop;
    public GameObject heartDrop;
    public GameObject coinDrop;

    // Ataque
    float enemyCooldown = 1.5f;
    bool playerInRange = false;
    bool canAttack = true;
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.player = GameObject.FindWithTag("Player");
        this.ms = 2f;
        localscale = transform.localScale;
        this.status = GameObject.FindWithTag("Player");
        this.monsterHP = 50;
    }

    void loseHP(int amount) {
        this.monsterHP -= amount;
        death();
    }

    void death() // O que acontece quando o monstro morre
    {
        if(this.monsterHP <= 0)
        {
            
            // Adicionar +1 ao contador
            status.GetComponent<Status>().killMonster();

            // Dropar item
            Vector3 itemDrop = this.gameObject.transform.position;
            itemDrop.z = 0;
            
            int rand = Mathf.RoundToInt(Random.Range(1f, 10f));

            switch(rand){
                case 1:
                case 2:
                case 3:
                case 4:
                    Instantiate(ammoDrop, itemDrop, Quaternion.identity);
                    break;
                case 5:
                case 6:
                    Instantiate(heartDrop, itemDrop, Quaternion.identity);
                    break;
                case 7:
                case 8:
                case 9:
                case 10:
                    Instantiate(coinDrop, itemDrop, Quaternion.identity);
                    break;
                default:
                    break;
            }

            // Destruir o monstro
            Destroy(gameObject);
        }
    }
    
    void OnTriggerEnter2D(Collider2D col) 
    {
        if(col.gameObject.tag.Equals("Bullet"))
        {
            Destroy(col.gameObject);
            loseHP(10);
        }
        if(col.gameObject.tag.Equals("Cactus"))
            loseHP(5);
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag.Equals("Player"))
            playerInRange = true;
    }

    private void OnCollisionExit2D(Collision2D col) {
        if(col.gameObject.tag.Equals("Player"))
            playerInRange = false;
    }

    void Update()
    {
        AnimatorSetting();
        if(playerInRange && canAttack)
        {
            int damage = Mathf.RoundToInt(Random.Range(3f, 7f));
            player.GetComponent<Status>().loseHealth(damage);
            StartCoroutine(AttackCooldown());
        }
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        animator.SetBool("Attack", !canAttack);
        yield return new WaitForSeconds(enemyCooldown);
        canAttack = true;
        animator.SetBool("Attack", !canAttack);
    }

    private void FixedUpdate() {
        MoveEnemy();
    }

    private void MoveEnemy() {
        playerDir = (player.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(playerDir.x, playerDir.y) * ms;
        Flip(playerDir.x);
    }

    void AnimatorSetting()
    {
        animator.SetFloat("Horizontal", playerDir.x);
    }

    void Flip(float horizontal) 
    {
        if(horizontal > 0 && facingRight || horizontal < 0 && !facingRight)
        {
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
        }

    }
}
