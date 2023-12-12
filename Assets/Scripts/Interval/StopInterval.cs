using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StopInterval : MonoBehaviour
{
	public PlayableDirector playableDirector;
	
	private void OnEnable() 
	{
		playableDirector.Pause();
	}
	
	public void ContinueDirector()
	{
		playableDirector.Play();
	}
}
