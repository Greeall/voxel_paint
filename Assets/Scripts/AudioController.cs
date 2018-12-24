using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour {

	public float vlm;
	public Slider sldrVolume;

	public GameObject audioObject;

	AudioSource audioSrc;

	 private void Awake()
     {
		audioSrc = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioSource>();
		
		if(!StaticBoolForAudio.fromVoxel) 
    		DontDestroyOnLoad(audioObject.transform.gameObject);
		else
			Destroy(audioObject.gameObject);
     }

 	public void PlayMusic()
     {
         if (audioSrc.isPlaying) return;
         audioSrc.Play();
     }

	public void StopMusic()
     {
         audioSrc.Stop();
     }
	void Start () {
	
		vlm = PlayerPrefs.GetFloat("volume", 0.5f);
		audioSrc.volume = sldrVolume.value = vlm;
	}
	
	public void ChangeVolume()
	{
		vlm = sldrVolume.value;
		audioSrc.volume = vlm;
		PlayerPrefs.SetFloat("volume", vlm);
	}
}
