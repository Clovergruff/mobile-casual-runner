using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CommonAssets", menuName = "Data/Common Assets", order = 1)]
public class CommonAssetsData : SingletonScriptableObject<CommonAssetsData>
{
	public CommonLayerMasks layerMasks;
	public CommonAudio audio;
	public CommonMaterials materials;
	public CommonPhysicMaterials physicMaterials;
	public LevelInstance[] levelPrefabs;

	[System.Serializable]
	public class CommonLayerMasks
	{
		public LayerMask solid;
		public LayerMask ground;
		public LayerMask pawns;
		public LayerMask triggers;
	}
	
	[System.Serializable]
	public class CommonMaterials
	{
		public Material hitFlash;
	}
	
	[System.Serializable]
	public class CommonPhysicMaterials
	{
		public PhysicMaterial gibs;
	}
	
	[System.Serializable]
	public class CommonAudio
	{
		public AudioClip hit;
		public AudioClip swingWeapon;
		public AudioClip jump;
		public AudioClip land;

		[Header("UI")]
		public AudioClip menuSelect;
		public AudioClip menuAccept;
		public AudioClip menuDecline;
	}
}