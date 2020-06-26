using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    [SerializeField] public AudioClip PlayerRunningSound;
    [SerializeField] public AudioClip PlayerAttackingSound;
    [SerializeField] public AudioClip PlayerJumpingSound;
    [SerializeField] public AudioClip PlayerDyingSound;
    [SerializeField] public AudioClip PlayerSpecialAttackSound;
    [SerializeField] public AudioClip PlayerGettingHitSound;
    [SerializeField] public AudioClip EnemyAttackingSound;
    [SerializeField] public AudioClip EnemyDyingSound;
    [SerializeField] public AudioClip EnemyGettingHitSound;
    [SerializeField] public AudioClip BossAttackingSound;
    [SerializeField] public AudioClip BossDyingSound;
    [SerializeField] public AudioClip BossGettingHitSound;
    [SerializeField] public AudioSource source;
    [SerializeField] public AudioSource enemySource;
    [SerializeField] public AudioSource bossSource;

    public void SwitchClip(AudioClip sound)
    {
        source.clip = sound;
    }

    public void PlaySound(string clipName)
    {
        switch (clipName)
        {
            case "playerRun":
                source.PlayOneShot(PlayerRunningSound);
                break;
            case "playerAttack":
                source.PlayOneShot(PlayerAttackingSound);
                break;
            case "playerJump":
                source.PlayOneShot(PlayerJumpingSound);
                break;
            case "playerDie":
                source.PlayOneShot(PlayerDyingSound);
                break;
            case "playerSpecialAttack":
                source.PlayOneShot(PlayerSpecialAttackSound);
                break;
            case "playerGetHit":
                source.PlayOneShot(PlayerGettingHitSound);
                break;
            case "enemyAttack":
                source.PlayOneShot(EnemyAttackingSound);
                break;
            case "enemyDie":
                source.PlayOneShot(EnemyDyingSound);
                break;
            case "enemyGetHit":
                source.PlayOneShot(EnemyGettingHitSound);
                break;
            case "bossAttack":
                bossSource.PlayOneShot(BossAttackingSound);
                break;
            case "bossDie":
                bossSource.PlayOneShot(BossDyingSound);
                break;
            case "bossGetHit":
                bossSource.PlayOneShot(BossGettingHitSound);
                break;
        }
    }

    public void PlayBossSound(string clipName)
    {
        switch (clipName)
        {
            case "bossAttack":
                bossSource.PlayOneShot(BossAttackingSound);
                break;
            case "bossDie":
                bossSource.PlayOneShot(BossDyingSound);
                break;
            case "bossGetHit":
                bossSource.PlayOneShot(BossGettingHitSound);
                break;
        }
    }

}
