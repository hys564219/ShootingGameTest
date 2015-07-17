using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Player controller and behavior
/// </summary>
public class PlayerScript : MonoBehaviour {

	/// <summary>
	/// 1 - speed of the ship
	/// </summary>
	public Vector2 speed = new Vector2(50,50);

	// 2 -Store the movement
	private Vector2 movement;

	void Update () {
		// 3 - retrieve axis information
		float inputX = Input.GetAxis ("Horizontal");
		float inputY = Input.GetAxis ("Vertical");

		// 4 - Movement per direction
		movement = new Vector2 (
			speed.x * inputX,
			speed.y * inputY);

		// 6 - shooting
		bool shoot = Input.GetButton ("Fire1");
		shoot |= Input.GetButton("Fire2");
		//a |= b相当于a = a | b

		if (shoot) {
			WeaponScript weapon = this.GetComponent<WeaponScript> ();
			if (weapon != null) {
					// false because the player is not an enemy
					weapon.Attack (false);
					SoundEffectsHelper.Instance.MakePlayerShotSound();
			}
		}

		// 6 - Make sure we are not outside the camera bounds
		var dist = (transform.position - Camera.main.transform.position).z;

		var leftBorder = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, dist)).x;

		var rightBorder = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, dist)).x;

		var topBorder = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, dist)).y;

		var bottomBorder = Camera.main.ViewportToWorldPoint (new Vector3 (0, 1, dist)).y;

		transform.position = new Vector3 (
			Mathf.Clamp (transform.position.x, leftBorder, rightBorder),
			Mathf.Clamp (transform.position.y, topBorder, bottomBorder),
			transform.position.z
		);
		// End of the update method
	}

	void FixedUpdate()
	{
		// 5 - Move the game project
		rigidbody2D.velocity = movement;
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		bool damagePlayer = false;
		
		// Collision with enemy
		EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript> ();
		if (enemy != null) {
			// Kill the enemy
			HealthScript enemyHealth = enemy.GetComponent<HealthScript>();
			if(enemyHealth != null)
			{
				enemyHealth.Damage(enemyHealth.hp);
			}

			damagePlayer = true;

		}

		// Damage the player
		if (damagePlayer) 
		{
			HealthScript playerHealth = this.GetComponent<HealthScript>();
			if(playerHealth != null)
			{
				playerHealth.Damage(1);
			}
		}
	}
	void OnDestroy() {
		// Game Over.
		// Add the script to the parent because the current game
		// object is likely going to be destroyed immediately.
		// @@Moved to HealthScript
	}
}
