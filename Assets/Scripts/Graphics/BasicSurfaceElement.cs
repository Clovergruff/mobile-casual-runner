using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering;

[ExecuteAlways]
public class BasicSurfaceElement : MonoBehaviour
{
	public new Renderer renderer;

	[Space]
	public Color color = Color.white;
	public Color emmission = Color.black;

	[Space]
	[Range(0, 1)] public float smoothness = 0;
	[Range(0, 1)] public float metallic = 0;

	private MaterialPropertyBlock _materialPropertyBlock;

	private void Awake()
	{
		_materialPropertyBlock = new MaterialPropertyBlock();
		ApplyPropertyBlock();
	}

	private void Reset()
	{
		renderer = GetComponentInChildren<Renderer>();
	}

	private void OnValidate()
	{
		ApplyPropertyBlock();
	}
	
	private void ApplyPropertyBlock()
	{
		if (_materialPropertyBlock == null)
			_materialPropertyBlock = new MaterialPropertyBlock();

		_materialPropertyBlock.SetColor(ShaderHash.COLOR, color);
		_materialPropertyBlock.SetColor(ShaderHash.EMISSION, emmission);
		_materialPropertyBlock.SetFloat(ShaderHash.SMOOTHNESS, smoothness);
		_materialPropertyBlock.SetFloat(ShaderHash.METALLIC, metallic);
		renderer.SetPropertyBlock(_materialPropertyBlock);
	}
}
