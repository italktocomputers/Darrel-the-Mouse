using UnityEngine;
using System.Collections;
using System;

public static class AudioController {
	public static AudioClip beetSoundWav = Resources.Load ("sounds/beet2") as AudioClip;
	public static AudioClip hitSoundWav = Resources.Load("sounds/hit2") as AudioClip;
	public static AudioClip jumpSoundWav = Resources.Load("sounds/jump") as AudioClip;
	public static AudioClip poofSoundWav = Resources.Load("sounds/poof") as AudioClip;
	public static AudioClip powerSoundWav = Resources.Load("sounds/item") as AudioClip;
	public static AudioClip gameOverSoundWav = Resources.Load("sounds/gameover2") as AudioClip;
	public static AudioClip successSoundWav = Resources.Load("sounds/success") as AudioClip;
	public static AudioClip bangSoundWav = Resources.Load("sounds/bang") as AudioClip;

	private static AudioSource audiosource;

	//
	// We need to call this function every time the game scene
	// loads since the component is destroyed once we leave the 
	// scene. 
	//
	public static void init() {
		audiosource = Camera.main.GetComponent<AudioSource>();
	}

	public static void playMusic() {
		AudioController.audiosource.clip = AudioController.beetSoundWav;
		audiosource.Play ();
	}

	public static void stopMusic() {
		if (AudioController.audiosource.isPlaying) {
			audiosource.Stop ();
		}
	}

	public static void playHitSound() {
		//if (ApplicationModel.getPlayMusicSetting() == 0) {
			AudioController.audiosource.PlayOneShot (AudioController.hitSoundWav);
		//}
	}

	public static void playBangSound() {
		//if (ApplicationModel.getPlayMusicSetting() == 0) {
		AudioController.audiosource.PlayOneShot (AudioController.bangSoundWav);
		//}
	}

	public static void playJumpSound() {
		//if (ApplicationModel.getPlayMusicSetting() == 0) {
			AudioController.audiosource.PlayOneShot (AudioController.jumpSoundWav);
		//}
	}

	public static void playPoofSound() {
		//if (ApplicationModel.getPlayMusicSetting() == 0) {
			AudioController.audiosource.PlayOneShot (AudioController.poofSoundWav);
		//}
	}

	public static void playPowerSound() {
		//if (ApplicationModel.getPlayMusicSetting() == 0) {
			AudioController.audiosource.PlayOneShot (AudioController.powerSoundWav);
		//}
	}

	public static void playGameOverSound() {
		AudioController.audiosource.PlayOneShot (AudioController.gameOverSoundWav);
	}

	public static void playSuccessSound() {
		//if (ApplicationModel.getPlayMusicSetting() == 0) {
			AudioController.audiosource.PlayOneShot (AudioController.successSoundWav);
		//}
	}
}