using UnityEngine;

public static class ShaderHash
{
	public static int COLOR = Shader.PropertyToID("_Color");
	public static int EMISSION = Shader.PropertyToID("_EmissionColor");
	public static int SMOOTHNESS = Shader.PropertyToID("_Glossiness");
	public static int METALLIC = Shader.PropertyToID("_Metallic");
}
