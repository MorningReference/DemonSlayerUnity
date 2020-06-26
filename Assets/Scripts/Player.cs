using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    // Config 
    [SerializeField] public int currentHealth;
    [SerializeField] public int maxHealth;
    [SerializeField] public int currentTension; //probably won't use this
    [SerializeField] public int maxTension; //probably won't use this
    [SerializeField] public int currentBreath;
    [SerializeField] public int maxBreath;
    [SerializeField] public int playerLevel;
    [SerializeField] public int currentStage;
    [SerializeField] public int damage;



    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);

    // State
    bool isAlive = true;

    // Cached component references
    public HealthBar healthBar;
    public Rigidbody2D myRigidBody;
    [SerializeField] Animator myAnimator;
    [SerializeField] CapsuleCollider2D myBodyCollider;
    [SerializeField] BoxCollider2D myFeet;
    [SerializeField] SpriteRenderer mySprite;
    [SerializeField] SoundHandler playerSounds;
    [SerializeField] AudioSource audioSrc;


    float gravityScaleAtStart;

    // Message then methods
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        Debug.Log($"Healthbar is set");
        currentBreath = maxBreath;
        // myRigidBody = GetComponent<Rigidbody2D>();
        // myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;
        // audioSrc = GetComponent<AudioSource>();
        currentStage = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("%^&*( From Player script, Current Stage is: " + currentStage);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }

        Run();
        // ClimbLadder();
        Jump();
        FlipSprite();
        // Die();
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxisRaw("Horizontal"); // value is betweeen -1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
        // GetComponentInChildren<Transform>().localPosition += transform.localPosition;
        // Rigidbody2D hitbox = GetComponentInChildren<Rigidbody2D>();
        // hitbox.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("IsRunning", playerHasHorizontalSpeed);
        if (myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) && playerHasHorizontalSpeed && playerSounds.source.isPlaying == false)
        {
            playerSounds.PlaySound("playerRun");
        }
        // audioSrc.PlayOneShot(playerSounds.PlayerRunningSound);
    }

    // private void ClimbLadder()
    // {
    //     if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Climbing")))
    //     {
    //         myAnimator.SetBool("Climbing", false);
    //         myRigidBody.gravityScale = gravityScaleAtStart;
    //         return;
    //     }

    //     float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
    //     Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);
    //     myRigidBody.velocity = climbVelocity;
    //     myRigidBody.gravityScale = 0f;

    //     bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
    //     myAnimator.SetBool("Climbing", playerHasVerticalSpeed);

    // }

    private void Jump()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }


        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            myAnimator.SetTrigger("Jumping");
            if (playerSounds.source.isPlaying == false)
            {
                playerSounds.PlaySound("playerJump");
            }
            // audioSrc.PlayOneShot(playerSounds.PlayerJumpingSound);
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("this tag is " + other.tag);
        if (other.tag == "Enemy")
        {
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            ProcessHit(damageDealer);
        }
    }
    private void ProcessHit(DamageDealer damageDealer)
    {
        // audioSrc.PlayOneShot(playerSounds.PlayerGettingHitSound);
        currentHealth -= damageDealer.GetDamage();
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            // audioSrc.PlayOneShot(playerSounds.PlayerDyingSound);
            if (playerSounds.source.isPlaying == false)
            {
                playerSounds.PlaySound("playerDie");
            }
            myAnimator.SetTrigger("IsDead");
            // GetComponent<Rigidbody2D>().velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x) * 1.75f, 1.75f);
        }
        // if (Input.GetAxisRaw("Horizontal") > 0)
        // {
        //     // isclimbing = false; // Also here we make the isclimbing boolean to be off if the Player is Climbing one Ladder and wants to be out from it
        //     mySprite.flipX = false; // Turning back the Sprite Renderer to the default Flip
        // }

        // // If the PLayer's Motion is in negative x-direction then we flip the Player's Sprite Renderer
        // else if (Input.GetAxisRaw("Horizontal") < 0)
        // {
        //     // isclimbing = false; // Again we turn off the boolean isclimbing so that our Player can be out of a Ladder while Climbing
        //     mySprite.flipX = true; // Here we Set the FlipX of the Sprite Renderer of the Player to true
        // }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();

        }
    }

}