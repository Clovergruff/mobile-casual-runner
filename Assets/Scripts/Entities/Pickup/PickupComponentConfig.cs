using Gruffdev.BCS;

public abstract class PickupComponentConfig : ConfigScriptableObject
{
	public abstract void ConstructSystemComponent(Pickup pickup);
}
