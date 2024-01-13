using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestMusic : MonoBehaviour
{
	AudioSource source;
	
	void OnGUI()
	{
		if(GUI.Button(new Rect(0,0,100,100), "Play BGM"))
			SoundMgr.Instance.PlayBKMusic("BGM0");
			
		if(GUI.Button(new Rect(0,100,100,100), "Pause BGM"))
			SoundMgr.Instance.PauseBKMusic();
			
		if(GUI.Button(new Rect(0,200,100,100), "Stop BGM"))
			SoundMgr.Instance.StopBKMusic();
		
		if(GUI.Button(new Rect(0,300,100,100), "Play Sound"))
			SoundMgr.Instance.PlaySound("Result", false, (s) =>
			{
				source = s;
			});
		
		//FIXME: only stop the last sound
		if(GUI.Button(new Rect(0,400,100,100), "Stop Sound"))
			SoundMgr.Instance.StopSound(source);
	}
}
