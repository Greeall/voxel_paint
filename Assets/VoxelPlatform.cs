using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class VoxelPlatform {

	public Vector2 position;
	public int voxelColor;

	public Color FindColor(List<VoxelColor> colors) {
		return colors[voxelColor].color;
	}

	public Texture2D FindTexture(List<VoxelColor> colors) {
		return Settings.GetPlatformTexture(colors[voxelColor].number);
	}
}
