using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Settings : MonoBehaviour {
	public List<Texture2D> platforms;

	public GameObject parentForVoxels;

	public static Settings I;

	void Awake() {
		I = this;
		StaticBoolForAudio.fromVoxel = true;
	}

	public static Texture2D GetPlatformTexture(int number) {
		return I.platforms[number];
	}
}
