using UnityEngine;
using System.Collections;

/// Creating instance of sounds from code with no effort
public class SoundEffectsHelper : MonoBehaviour {

	/// <summary>
	/// Singleton
	/// </summary>
	public static SoundEffectsHelper Instance;

	public AudioClip explosionSound;
	public AudioClip playerShotSound;
	public AudioClip enemyShotSound;

	void Awake() {
		//Register the singleton
		if (Instance != null) {
			Debug.LogError("Multiple instances of SoundEffectsHelper!");
		}
		Instance = this;
	}

	public void MakeExplosionSound() {
		MakeSound (explosionSound);
	}

	public void MakePlayerShotSound() {
		MakeSound (playerShotSound);
	}

	public void MakeEnemyShotSound() {
		MakeSound (enemyShotSound);
	}

	/// Play a given sound
	private void MakeSound(AudioClip OriginalClip) {
		// As it is not 3D audio clip, position doesn't matter.
		AudioSource.PlayClipAtPoint (OriginalClip, transform.position);
	}
}
