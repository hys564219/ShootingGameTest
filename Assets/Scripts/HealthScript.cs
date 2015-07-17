using UnityEngine;
using System.Collections;

/// <summary>
/// Handle hitpoints and damages
/// </summary>
public class HealthScript : MonoBehaviour
{
	/// <summary>
	/// Total hitpoints
	/// </summary>
	public int hp = 1;
	
	/// <summary>
	/// Enemy or player?
	/// </summary>
	public bool isEnemy = true;
	
	/// <summary>
	/// Inflicts damage and check if the object should be destroyed
	/// </summary>
	/// <param name="damageCount"></param>
	public void Damage(int damageCount)
	{
		hp -= damageCount;
		//SpecialEffectsHelper.Instance.FireUp(transform.position);
		if (hp <= 0)
		{
			// 'Splosion!
			SpecialEffectsHelper.Instance.Explosion(transform.position);

			SoundEffectsHelper.Instance.MakeExplosionSound();

			// Game Over
			if(isEnemy == false || gameObject.GetComponent<BossScript>() != null) {
				gameObject.GetComponentInParent<GameOverScript> ().enabled = true;
				GameState.Instance.gameOver();
			}
			// Dead!
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		// Is this a shot?
		ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
		if (shot != null)
		{
			// Avoid friendly fire
			if (shot.isEnemyShot != isEnemy)
			{
				Damage(shot.damage);
				
				// Destroy the shot
				Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
			}
		}
	}
}
