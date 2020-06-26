using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRangeX;
    public float attackRangeY;
    public float attackRangeAngle = 0;
    public int damage;

    public GameObject hitBox;

    [SerializeField] SoundHandler playerSounds;
    AudioSource audioSrc;

    private void Start()
    {
        audioSrc = GetComponentInParent<AudioSource>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && timeBtwAttack <= 0)
        {
            Attack();
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.C) && timeBtwAttack <= 0)
        {
            SpecialAttack();
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }


    void Attack()
    {
        // audioSrc.PlayOneShot(playerSounds.PlayerAttackingSound);
        if (playerSounds.source.isPlaying == false)
        {
            playerSounds.PlaySound("playerAttack");
        }
        GetComponent<Animator>().SetTrigger("Attack");
    }

    void SpecialAttack()
    {
        // audioSrc.PlayOneShot(playerSounds.PlayerSpecialAttackSound);
        if (playerSounds.source.isPlaying == false)
        {
            playerSounds.PlaySound("playerSpecialAttack");
        }
        GetComponent<Animator>().SetTrigger("SpecialAttack");
    }

}