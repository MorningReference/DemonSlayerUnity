using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    // [SerializeField] int damage = 100;

    [SerializeField] Enemy enemy;
    [SerializeField] Player player;
    private int damage;

    void Start()
    {
        Debug.Log("Enemy or nah?" + enemy);
        if (enemy)
        {
            damage = enemy.damage;
        }
        else
        {
            damage = player.damage;
        }
    }

    public int GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }

}
