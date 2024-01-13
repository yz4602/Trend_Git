using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Events;

public class SoundMgr : BaseManager<SoundMgr>
{
	private AudioSource bkMusic = null;
	private float bkValue = 1;
	
	private GameObject soundObj = null;
	private List<AudioSource> soundList = new List<AudioSource>();
	private float soundValue = 1;
	
	public SoundMgr()
	{
		MonoManager.Instance.AddUpdateListener(Update);
	}
	
	private void Update()
	{
		for(int i = 0; i < soundList.Count; i++)
		{
			if(!soundList[i].isPlaying)
			{
				soundList[i].Stop();
				GameObject.Destroy(soundList[i]);
				soundList.RemoveAt(i);
			}
		}
	}
	
	public void PlayBKMusic(string name)
	{
		if(bkMusic == null)
		{
			GameObject obj = new GameObject();
			obj.name = "BkMusic";
			bkMusic = obj.AddComponent<AudioSource>();
		}
		
		ResMgr.Instance.LoadAsync<AudioClip>("Audio/BGM/" + name, (clip) =>
		{
			bkMusic.clip = clip;
			bkMusic.loop = true;
			bkMusic.volume = bkValue;
			bkMusic.Play();
		});
	}
	
	public void ChangeBKValue(float v)
	{
		if(bkMusic == null) return;
		bkMusic.volume = v;
	}
	
	public void PauseBKMusic()
	{
		if(bkMusic == null) return;
		bkMusic.Pause();
	}
	
	public void StopBKMusic()
	{
		if(bkMusic == null) return;
		bkMusic.Stop();
	}

	public void PlaySound(string name, bool isLoop = false, UnityAction<AudioSource> callBack = null)
	{
		if(soundObj == null)
		{
			soundObj = new GameObject();
			soundObj.name = "Sound";
		}
		
		ResMgr.Instance.LoadAsync<AudioClip>("Audio/Sound/" + name, (clip) =>
		{
			AudioSource source = soundObj.AddComponent<AudioSource>();
			soundList.Add(source);
			
			source.clip = clip;
			source.loop = isLoop;
			source.volume = soundValue;
			source.Play();
			
			if(callBack != null)
				callBack(source);
		});
	}
	
	public void ChangeSoundVlue(float value)
	{
		soundValue = value;
		foreach(AudioSource a in soundList)
			a.volume = value;
	}
	
	public void StopSound(AudioSource source)
	{
		if(soundList.Contains(source))
		{
			soundList.Remove(source);
			source.Stop();
			GameObject.Destroy(source);
		}
	}
}
