using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_old : MonoBehaviour
{
    [SerializeField] int currentHP;
    public int maxHP;
    [SerializeField] float dazedTime;
    public float startDazedTime;
    public float speed;
    Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (dazedTime <= 0)
        {
            speed = 1;
        }
        else
        {
            speed = -2;
            dazedTime -= Time.deltaTime;
        }
        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
    }
    public void DealDamage()
    {

    }
}
