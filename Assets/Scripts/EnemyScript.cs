using UnityEngine;
using System.Collections;

/// <summary>
/// Enemy generic behavior
/// </summary>
public class EnemyScript : MonoBehaviour {
	
	private bool hasSpawn;
	private MoveScript moveScript;
	private WeaponScript[] weapons;

	void Awake() {
		// Retrieve the weapon only once
		weapons = GetComponentsInChildren<WeaponScript> ();

		// Retrieve scripts to disable when not spawn
		moveScript = GetComponent<MoveScript> ();
	}

	// 1 - Disable everything
	void Start() {
		hasSpawn = false;

		// Disable everything
		// -- collider
		collider2D.enabled = false;
		// -- Moving
		moveScript.enabled = false;
		// -- shooting
		foreach (WeaponScript weapon in weapons) {
			weapon.enabled = false;
		}
	}
	// Update is called once per frame
	void Update () {
		// 2 - Check if the enemy has spawned.
		if (hasSpawn == false) {
			if (renderer.IsVisibleFrom (Camera.main)) {
					Spawn ();
			}
		} else {
			// Auto- fire
			foreach (WeaponScript weapon in weapons) {
				if (weapon != null && weapon.CanAttack) {
					weapon.Attack (true);
					SoundEffectsHelper.Instance.MakeEnemyShotSound();
				}
			}
			moveScript.enabled = false;
			// 4 - Out of the camera ? Destroy the game object.
			if(renderer.IsVisibleFrom(Camera.main) == false) {
				Destroy(gameObject);
			}
		}

	}
	// 3 - Activate itself.
	private void Spawn() {
		// Enable everything
		// -- collider
		collider2D.enabled = true;
		// -- Moving
		moveScript.enabled = true;
		// -- shooting
		foreach (WeaponScript weapon in weapons) {
			weapon.enabled = true;
		}
		hasSpawn = true;
	}

	void OnDestroy() {
		GameState.Instance.addScore (60 * 100);
	}
}
