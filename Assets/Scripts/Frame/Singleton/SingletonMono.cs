using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T: SingletonMono<T>
{
	private static T instance;
	public static T Instance{get{return instance;}}
	
	protected virtual void Awake() 
	{
		if(instance == null)
			instance = this as T;
		else
			Destroy(gameObject);
		
		//DontDestroyOnLoad(this); //If do not need, delete this line
	}
	
	public static bool IsInitialized
	{
		get{ return instance != null; }
	}
	
	protected virtual void OnDestroy() 
	{
		if(instance == this)
		{
			instance = null;
		}
	}
}
