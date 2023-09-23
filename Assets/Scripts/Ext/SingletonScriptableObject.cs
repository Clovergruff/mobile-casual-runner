using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonScriptableObject<T> : ScriptableObject where T : SingletonScriptableObject<T>
{
	private static bool instanceFound;
	private static T instance;

	public static bool Exists
	{
		get
		{
			FindInstance();
			return instanceFound;
		}
	}

	public static T I
	{
		get
		{
			FindInstance();
			return instance;
		}
	}

	private static void FindInstance()
	{
		if (instanceFound)
		{
			return;
		}

		T[] assets = Resources.LoadAll<T>("");
		if (assets == null || assets.Length < 1)
		{
			throw new System.Exception($"No {typeof(T)} instances exist!");
		}
		else if (assets.Length > 1)
		{
			throw new System.Exception($"There are multiple {typeof(T)} instances!");
		}

		instanceFound = true;
		instance = assets[0];
	}
}
