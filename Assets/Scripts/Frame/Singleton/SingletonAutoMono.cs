using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Don't need to drag script ot add by API
//Getinstance() directly to use
public class SingletonAutoMono<T> : MonoBehaviour where T: MonoBehaviour
{
	private static T instance;
	public static T Instance
	{
		get
		{
			if(instance == null)
			{
				GameObject obj = new GameObject();
				//Set object's name as this script's name : Reflection
				obj.name = typeof(T).ToString();
				DontDestroyOnLoad(obj);
				
				instance = obj.AddComponent<T>();
			}
			return instance;
		}
	}
}
