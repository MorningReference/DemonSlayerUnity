/*
 ***Hero's Legend Studio scripts***
 *Follow Me on Instagram @HerosLegendStudio 
 *Developer:Robin
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndThrow_Lite : MonoBehaviour {
	/*How to use this script!!!
	 	***This Script is intended for 2D purposes but will work for 3D games***
	 	
	 *This script should be attached to theHand gameObject as the collision is based off what this script's collider.
	 *theHand gameObject should have a Collider2D component
	 *grabTag gameObjects should have a Collider2D and RigidBody2D components
	 ***RigidBody2D on the grabTag allows the game object to use Unity's 2D Physics for throws
	 */

	public GameObject startPos;//starting position of the object which grabs grabbable items
	public GameObject theHand;//this is the gameObject that extends and is able to grab other gameObjects
	public GameObject theMC;//the main character to correctly parent theHand object to, theHand should travel with theMC
	private Vector3 theHandPos;//temp Vector3 to hold theHand transform position in the game world
	public int extLength = 10;//extension length of the grabbing object
	public string  grabTag;//tags defined to be part of grabbable items
	public int throwDist = 10;//how far to throw the grabTag object
	public float throwSpeed = 5.0f;//how fast to throw theHand object
	private bool playerControl = false;//they player has control of how to throw the grabTag gameObject

	private Vector3 sentPos;//the position when theHand was sent out
	private bool launched;//has theHand gameObject be sent out?
	private bool facingRight;//is the character facing right when the grab is launched?
	private bool retract;//retract theHand gameObject to startPosObj

	// Use this for initialization
	void Start () 
	{
		//initialize all private variables
		theHandPos = theHand.transform.position;//assign temp Vector3 to hold theHand position in the world
		sentPos = new Vector3();
		launched = false;
		facingRight = true;
		retract = false;
		if(extLength <= 0)
			extLength = 10;
		if(throwDist <= 0)
			throwDist = 10;
		if(throwSpeed <= 0)
			throwSpeed = 5.0f;
		Debug.Assert(grabTag != "", "Please define a grabTag");//throw an error to user that the grabTag has not been defined
		Debug.Assert(startPos != null, "Please assign a startPos gameObject");
		Debug.Assert(theHand != null,"Please assign theHand a gameObject");
		Debug.Assert(theMC != null,"Please assign theMC gameObject");
	}
	
	// Update is called once per frame
	void Update () 
	{
		//The ability activation button is tied to the "Fire1" Input. Please see Edit > Project Settings > Input > Axes > Fire1 
			// in the InputManager to find what the Positive Buttons are, modify as necessary for your game project. 
		if (Input.GetButtonDown("Fire1"))//Fire button has been pressed
		{
			if(!launched && !retract)//if theHand has not been sent out yet or currently retracting call LaunchGrab() method
			{
				LaunchGrab();
			}
		}
		
		if(launched)
		{
			Launching();//do launching method
		}
		else if(retract)
		{
			Retracting();//do retracting method	
		}
			
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		//If theHand has been launched and it has collided with a grabTag take control of that gameObject and throw it
		// depending on throwDist and direction[] do throw action
		if(launched)
		{
			if(collision.transform.tag == grabTag)// on contact of an grabTab object
			{
				if(playerControl)//not used for Lite version
				{
					
				}
				else
				{
					//use random generator to determine where to throw the grabTag gameObject
					int tempNum = Random.Range(1,4);
					//select between toss up, toss back(behind MC), toss forward(further away from MC)
					Debug.Log(tempNum);
					if(tempNum == 1)
					{
						//toss enemy up
						collision.rigidbody.AddForce(transform.up * 500);
						Debug.Log("enemy name is: " + collision.transform.name);
					}
					else if(tempNum == 2)
					{
						//toss back(behind/closer to MC)
						if(facingRight)
						{
							//toss left
							collision.rigidbody.AddForce(new Vector2(-250.0f,250.0f));
							Debug.Log("enemy name is: " + collision.transform.name);
						}
						else
						{
							//toss right
							collision.rigidbody.AddForce(new Vector2(250.0f,250.0f));
							Debug.Log("enemy name is: " + collision.transform.name);
						}
					}
					else if(tempNum == 3)
					{
						//toss forward(further away from MC)
						if(facingRight)
						{
							//toss right
							collision.rigidbody.AddForce(new Vector2(250.0f,250.0f));
							Debug.Log("enemy name is: " + collision.transform.name);
						}
						else
						{
							//toss left
							collision.rigidbody.AddForce(new Vector2(-250.0f,250.0f));
							Debug.Log("enemy name is: " + collision.transform.name);
						}
					}
					launched = false;
					retract = true;
				}
			}
		}
	}

	void LaunchGrab()
	{
		//Here theHand gameObject will be sent in the forward direction of the character and will either hit the first grabTag
		// reach the extLength before doing another action
		//is the character facing right or left shoot in that direction
		theHand.gameObject.transform.parent = null;//remove theHand gameObject from the MC so it's collision does not effect MC
		if(startPos.transform.lossyScale.x > 0)
		{
			//facing right
			facingRight = true;
		}
		else
		{
			//facing left
			facingRight = false;
		}
		launched = true;
		theHandPos = theHand.transform.position;//set temp Vector3 to theHand position
		sentPos = theHand.transform.position;//set sentPos to measure extLength later
	}

	void Launching()
	{
		//if the Distance between the current location of the hand and the Sent Position are less then the extLength keep moving theHand
		//otherwise return it to the MainCharacter(MC)
		if(Vector3.Distance(sentPos, theHand.transform.position) < extLength){
			//CharacterRobotBoy changes the scale of the transform to change its art facing direction
			//I will keep with this format and adjust ability to work with this foundation
			if(facingRight)//if facing right when thehand launched move to the Right
			{
				theHandPos.x += throwSpeed*Time.deltaTime;
				theHand.transform.position = theHandPos;
			}
			else//move theHand to the left
			{
				theHandPos.x -= throwSpeed*Time.deltaTime;
				theHand.transform.position = theHandPos;
			}
		}
		else
		{
			Debug.Log("hit the extLength of: " + extLength);
			//theHand has reached the extLength so return to startPos
			//this should return slowly or at specified speed
			//this will be moved to an unlaunched section
			launched = false;
			retract = true;
		}
	}

	void Retracting()
	{
		theHandPos = theHand.transform.position;
		float returnedDist = Vector3.Distance(theHandPos,startPos.transform.position);
		if(returnedDist < 0.5f)
		{
			retract = false;
			theHand.transform.SetParent(theMC.transform);
			theHand.transform.position = startPos.transform.position;
			theHand.transform.rotation = startPos.transform.rotation;
		}
		else
		{
			theHand.transform.position = Vector3.MoveTowards(theHandPos,startPos.transform.position, throwSpeed*Time.deltaTime);
		}
	}
}
