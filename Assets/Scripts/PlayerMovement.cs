using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemonSlayer
{
    public class PlayerMovement : MonoBehaviour
    {
        #region VOiD1 Gaming

        // 1. You can directly attach this Script to any GameObject and see it in action
        // 2. All the variables given here are tweakable, You can change them as per you Game Requirement
        // 3. Nearly each line of the Script is Documented so that it can be accessed easily by anyone
        // 4. This Script contains- Basic Horizontal Movement, Smoothing while Stoping, Multi Jump, Coyote Jump,
        //    Short and Long Jump on Button Long Press, Ladder Climb
        // 5. The operations in this Script is divided into Update and Fixed Update for Better Performance
        // 6. An Additional Scene has been included in the Project File in order to help you getting started 

        #endregion VOiD1 Gaming

        [Header("Player")]
        [Tooltip("Reference to the Rigidbody 2D attached to the Player")]
        public Rigidbody2D rb; // Reference to the Rigidbody2D of the Player

        [Header("Movement Properties")]
        [Tooltip("Horizontal Movement Speed of the Player")]
        public float movementspeed; // Horizontal Movement Speed of the Player
        [Range(0f, 1f)]
        [Tooltip("Horizontal Damping when the Player is about to Stop")]
        public float horizontaldampingwhenstopping; // Horizontal Smoothing when the Player stops
        [Range(0f, 1f)]
        [Tooltip("Horizontal Damping when the Player turns its direction of motion")]
        public float horizontaldampingwhenturning; // Horizontal Smoothing when the Player turns its direction of motion
        [Range(0f, 1f)]
        [Tooltip("Basic Horizontal Damping of the Player Movement")]
        public float horizontaldampingbasic; // Basic Smoothing of the Horizontal Movement
        private float horizontalvelocity; // The float used to assign the Unity's default Input System

        [Header("Jump Properties")]
        [Tooltip("Jump Speed of the Player")]
        public float jumpspeed; // Jump Speed of the Player
        [Tooltip("Layermask to detect on which Layer the Player should be able to Jump")]
        public LayerMask whatisground; // Layermask to detect on which Layer the Player should Jump particularly
        [Tooltip("Radius to check whether the Player is colliding with the Layer ascertained")]
        public float groundcheckradius; // Radius to check whether the Player is colliding with the Layer ascertained
        [Tooltip("Boolean to check if the PLayer is Grounded")]
        public bool isgrounded; // Boolean to check if the PLayer is Grounded
        [Tooltip("Transform Position of the GameObject resposible for checking the isgrounded boolean")]
        public Transform groundcheck; // Transform Position of the GameObject resposible for checking the isgrounded boolean

        [Header("Coyote Time")]
        [Tooltip("Response time of the Jump after the Player has left an Platform")]
        public float coyotetime = .2f; // Response time of the Jump after the Player has left an Platform
        [Tooltip("Timer to check for the Jump")]
        public float coyotecounter; // Timer to check for the Jump

        [Header("Minimum Distance of Jump Detection")]
        [Tooltip("Minimum Distance of the Jump detection of a Player")]
        public float jumpbufferlength = .1f; // Minimum Distance of the Jump detection of a Player
        private float jumpbuffercount; // Timer to check the distance and initiate it accordingly

        [Header("Multi Jump")]
        [Tooltip("Total number of jumps a Player can make while in the air")]
        public int totjump = 1; // Total number of jumps a Player can make while in the air
        [Tooltip("Boolean to set whether the Player can do a Multi Jump or not")]
        public bool multijump = true; // Boolean to set whether the Player can do a Multi Jump or not
        private int defaulttotjump; // Variable to store the Total Number of Jumps Allowed in air to reset it in the game

        [Header("Dash")]
        [Tooltip("The Spped at which the Player can Dash")]
        public float dashspeed; // The Spped at which the Player can Dash

        [Header("Ladder Properties")]
        [Tooltip("Layer Mask to detect on which Layer the Player can Climb as a Ladder")]
        public LayerMask whatisladder; // Layer Mask to detect on which Layer the Player can Climb as a Ladder
        [Tooltip("The distance of the Raycast to check the Ladder")]
        public float hitdist; // The distance of the Raycast to check the Ladder
        [Tooltip("Boolean to check wether the Player is Climbing or not")]
        public bool isclimbing; // Boolean to check wether the Player is Climbing or not
        [Tooltip("The Speed at which the Player Climbs the Ladder")]
        public float moveupspeed; // The Speed at which the Player Climbs the Ladder
        private float inputup; // The float to store the Unity's default Input System

        private float defaultgravity; // The float to store the Gravity Scale of the Player

        public bool isAttack;


        public void Start()
        {
            defaulttotjump = totjump; // At start we store the total number of jumps allowed in air to the defaulttotjump variable
            defaultgravity = rb.gravityScale; // We also store the Gravity Scale of the Player whcih we will tweak in the Game
        }
        private void FixedUpdate()
        {
            // In this Fixed Update we will handle most of the Physics Movement of the Player which is primarily depends on the time

            // First we store the velocity of the Player moving in x-direction 
            horizontalvelocity = rb.velocity.x;

            // Then we add the Unity's Default Input System as per the Player Direction
            horizontalvelocity += Input.GetAxisRaw("Horizontal");

            // Here we check the Player's movement and apply the required smoothing as per the condition
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < .01f) // Player is about to stop
            {
                // We apply the Smoothing to the Player's Horizontal Movement
                horizontalvelocity *= Mathf.Pow(1f - horizontaldampingwhenstopping, Time.deltaTime * 10f);

            }
            else if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(horizontalvelocity)) // Player is Turning its direction
            {
                // Here we apply the smoothing when the PLayer Turns at a certain velocity
                horizontalvelocity *= Mathf.Pow(1f - horizontaldampingwhenturning, Time.deltaTime * 10f);

            }
            else
            {
                // Normal Smoothing as per the Player's movement
                horizontalvelocity *= Mathf.Pow(1f - horizontaldampingbasic, Time.deltaTime * 10f);

            }

            // After we calculate the required Horizontal Velocity, we apply it to the Player's Rigidbody
            rb.velocity = new Vector2(horizontalvelocity, rb.velocity.y);

            // If the Player is Grounded then we reset the coyotetime and the total number of jumps allowed 
            if (isgrounded)
            {
                coyotecounter = coyotetime; // We reset the coyote timer to the coyote time 
                totjump = defaulttotjump; // We reset the total number of jumps allowed in the air back to the default settings 
            }
            else
            {
                coyotecounter -= Time.deltaTime; // If the Player is not Grounded then we decrease the coyote timer with a deltatime 
            }

            // Here we check if the Player is Climbing a Ladder
            if (isclimbing)
            {
                rb.gravityScale = 0; // First we disable the Gravity acting upon the Player
                inputup = Input.GetAxisRaw("Vertical"); // Then we store the Vertical Direction from the Unty' Default Input System
                rb.velocity = new Vector2(0, inputup * moveupspeed); // Lastly, we apply the upward force to climb the Ladder

            }
            else
            {
                rb.gravityScale = defaultgravity; // If the Player is not Climbing then we set the Gravity Scale back to the default settings
            }

        }

        private void Update()
        {
            // In this Update Method we will check for all the logic and core functionalities of the game which is primely independent of the time

            // First we check if the Player is Grounded using the Overlap Circle
            isgrounded = Physics2D.OverlapCircle(groundcheck.position, groundcheckradius, whatisground); // isgrounded turns to be true if the OverlapCircle Collides with the specified layer given

            // Here we cast a Ray to check if the Player can Climb the Ladder 
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, hitdist, whatisladder); // The variable hit stores all the information of the Raycast originated from the Player and check if the collided object is Ladder or not

            // Then we check if the collision info in the hit variable is not null, proceed on checking the iput for the Ladder Climb
            if (hit.collider != null)
            {
                // Here we check the Input of the Player to Climb a Ladder
                if (Input.GetKeyDown(KeyCode.W))
                {
                    isclimbing = true; // If the Raycast Collided to the GameObject is a Ladder amd Player has also pressed the W key then we will set the boolean isclimbing to true
                }
            }

            // If the variable hit has no record of the any collision then we will set the isclimbing variable to false
            else if (hit.collider == null)
            {
                isclimbing = false; // We set the isclimbing variable to false as there is not hit to the Ladder
            }

            // Inorder to Multi Jump in the air, We first check if this is tweaked by the User or not
            if (multijump)
            {
                // If the Multi Jump is enabled, greater than 0, and the User Pressed W then our Player can do Multi Jump in the air
                if (Input.GetKeyDown(KeyCode.W) && totjump > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpspeed); // We give a Velocity to the Player in the upward direction while our Player is in air
                    totjump--; // We also decrement the variable totjump that was set by the user in order to restrict further possible jumps in the air
                }
            }

            // If the Multi Jump is not enabled then we can go for basic jumping
            else if (multijump == false)
            {
                // Here we check if the User press W then we set the jumpbuffercount to the jumplength given by default
                if (Input.GetKeyDown(KeyCode.W))
                {
                    jumpbuffercount = jumpbufferlength;
                }
                else
                {
                    // Else jumpbuffercount is decremented with a delta time value
                    jumpbuffercount -= Time.deltaTime;
                }

                // Now we check if the jumpbuffer is not 0 and the coyote timer is greater than 0 then our Player can Jump
                if (jumpbuffercount >= 0f && coyotecounter > 0f)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpspeed); // Here we apply an upward velocity to the Player's Rigidbody
                    jumpbuffercount = 0f; // Also we set the jumpbuffercount to 0
                }

                // Here we make our Player to jump a short distance if the User is not holding the W Button
                if (Input.GetKeyUp(KeyCode.W) && rb.velocity.y > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f); // We reduce the velocity to half and add to the Player's Rigidbody
                }
            }

            // In order to make our Player Dash we check if the User has pressed W and the velocity of our Player is in x-direction
            if (Input.GetKeyDown(KeyCode.Space) && (rb.velocity.x > 0 || rb.velocity.x < 0))
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f) * dashspeed; // Then we add a velocity to the Player's Rigidbody
            }

            // Here we Flip our Player's Sprite Renderer in order to give a response to the Player's Movement direction
            // If the velocity of the Player is in positive x-direction then we keep the flip x in the Sprite Renderer to be False and vice versa
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                isclimbing = false; // Also here we make the isclimbing boolean to be off if the Player is Climbing one Ladder and wants to be out from it
                gameObject.GetComponent<SpriteRenderer>().flipX = false; // Turning back the Sprite Renderer to the default Flip
            }

            // If the PLayer's Motion is in negative x-direction then we flip the Player's Sprite Renderer
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                isclimbing = false; // Again we turn off the boolean isclimbing so that our Player can be out of a Ladder while Climbing
                gameObject.GetComponent<SpriteRenderer>().flipX = true; // Here we Set the FlipX of the Sprite Renderer of the Player to true
            }

            if (Input.GetKeyDown(KeyCode.X) && isAttack == false)
            {
                isAttack = true;
            }
        }
    }

}