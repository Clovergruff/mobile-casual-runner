using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Surface", menuName = "Data/Surfaces/Surface", order = 3)]
public class SurfaceMaterial : ScriptableObject
{
	[SerializeField] private Material gfxMaterial;
	[SerializeField] private SurfaceMaterialProperties properties;

	public Material GfxMaterial { get => gfxMaterial; set => gfxMaterial = value; }
	public SurfaceMaterialProperties Properties { get => properties; set => properties = value; }
}
