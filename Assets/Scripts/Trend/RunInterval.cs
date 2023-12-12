using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class RunInterval : MonoBehaviour
{
	public PlayableDirector director;
	public IntervalBossDialogue intervalBossDialogue;
	public ChangeBarsValue changeBarsValue;
	public GameObject endCanvas;
	public Canvas commentCanvas;
	
	private void OnEnable() 
	{
		if(intervalBossDialogue.GetDay() >= 5)
		{
			commentCanvas.sortingOrder = 6;
			changeBarsValue.ChangeBarValue();
			Invoke("ActivateEnd", 2f);	
		}
		else
		{
			Invoke("PlayDirector", 0.85f);
		}
	}
	
	public void PlayDirector()
	{
		gameObject.SetActive(false);
		intervalBossDialogue.UpdateBossText();
		director.Play();
	}
	
	public void ActivateEnd()
	{
		endCanvas.SetActive(true);
	}
	
}
