using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 2;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;
    // Start is called before the first frame update

    [SerializeField] SoundHandler enemySounds;
    private AudioSource audioSrc;

    [SerializeField] public int damage;
    // for the follower enemy AI
    // public AIPath aiPath;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // EnemyFacingPlayer();
        // transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        // ProcessHit();
        if (health <= 0)
        {
            Debug.Log($"***************************we dead yet??  {health}");
            Die();
        }
    }

    // private void EnemyFacingPlayer()
    // {
    //     if (aiPath.desiredVelocity.x >= 0.01f)
    //     {
    //         transform.localScale = new Vector3(-1f, 1f, 1f);
    //     }
    //     else if (aiPath.desiredVelocity.x <= -0.01f)
    //     {
    //         transform.localScale = new Vector3(1f, 1f, 1f);
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Player player = other.gameObject.GetComponent<Player>();
        // ProcessHit(player);
        // if (other.tag == "Player")
        // {

        // }
        // else 
        Debug.Log(other.tag);
        if (other.tag == "Sword")
        {
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            ProcessHit(damageDealer);
        }

    }

    public void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        // ***edit here if adding VFX for displaying # of dmg hit***
        if (enemySounds.source.isPlaying == false)
        {
            enemySounds.PlaySound("enemyGetHit");
        }
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log($"***************************WE IN THE DIEf(x)  {health}");

        if (enemySounds.source.isPlaying == false)
        {
            enemySounds.PlaySound("enemyDie");
        }
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (enemySounds.source.isPlaying == false)
        {
            enemySounds.PlaySound("enemyGetHit");
        }
        if (health <= 0)
        {
            Die();
        }
    }

}
