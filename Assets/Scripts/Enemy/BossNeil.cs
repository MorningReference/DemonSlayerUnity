using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class BossNeil : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 500;
    public int attackDamage = 20;
    public int angryAttackDamage = 40;
    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;
    public Transform player;
    public bool isFlipped = false;
    public HealthBar healthBar;
    protected AudioSource audioSrc;

    [SerializeField] SoundHandler bossSounds;

    public bool isInvulnerable = false;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        audioSrc = GetComponent<AudioSource>();

    }
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Sword")
        {
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            ProcessHit(damageDealer);
        }

    }

    public void ProcessHit(DamageDealer damageDealer)
    {
        currentHealth -= damageDealer.GetDamage();
        // ***edit here if adding VFX for displaying # of dmg hit***
        if (bossSounds.source.isPlaying == false)
        {
            bossSounds.PlayBossSound("bossGetHit");
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Attack()
    {

        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<Player>().TakeDamage(attackDamage);
        }
    }

    public void AngryAttack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<Player>().TakeDamage(angryAttackDamage);
        }
    }
    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
        {
            return;

        }

        currentHealth -= damage;

        if (currentHealth <= 200)
        {
            if (bossSounds.source.isPlaying == false)
            {
                bossSounds.PlayBossSound("bossGetHit");
            }
            GetComponent<Animator>().SetBool("isAngry", true);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // audio goes here
        if (bossSounds.source.isPlaying == false)
        {
            bossSounds.PlayBossSound("bossDie");
        }
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        // Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }
}
