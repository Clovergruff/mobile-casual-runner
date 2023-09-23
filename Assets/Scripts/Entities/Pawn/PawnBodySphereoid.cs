using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeauRoutine;
using Random = UnityEngine.Random;

public class PawnBodySphereoid : MonoBehaviour
{
	public Transform graphics;

	private Routine _enablePhysicsRoutine = Routine.Null;
	private Vector3 _physicsScale = Vector3.one;

	private SphereCollider _collider;
	private Rigidbody _rigidbody;

	private Vector3 _forceVelocity = Vector3.zero;

	public void EnableGibPhysics(Vector3 origin, PhysicMaterial physicMaterial)
	{
		transform.SetParent(null);
		var oldScale = transform.localScale;
		var targetScaleSize = oldScale.magnitude * 0.5f;
		transform.localScale = Vector3.one;
		graphics.localScale = oldScale;
		
		_physicsScale = Vector3.one * targetScaleSize;

		_collider = gameObject.AddComponent<SphereCollider>();
		_collider.radius = targetScaleSize * 0.5f;
		_collider.material = physicMaterial;

		_rigidbody = gameObject.AddComponent<Rigidbody>();
		_rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
		_rigidbody.maxAngularVelocity = 15;
		_rigidbody.angularVelocity = Random.insideUnitSphere.normalized * Random.Range(5f, 15f);
		_rigidbody.drag = 0.2f;
		_rigidbody.angularDrag = 2;

		// _forceVelocity.x = Random.Range(-2f, 2f);
		// _forceVelocity.z = Random.Range(-2f, 2f);
		// _forceVelocity.y = Random.Range(4f, 6f);
		_forceVelocity = (transform.position - origin).normalized * Random.Range(2f, 3f);
		_forceVelocity.y += Random.Range(2f, 3f);
		_rigidbody.velocity = _forceVelocity;

		_enablePhysicsRoutine.Stop();
		_enablePhysicsRoutine = Routine.Start(EnablePhysicsCoroutine());
	}

	private IEnumerator EnablePhysicsCoroutine()
	{
		Vector3 oldScale = graphics.localScale;
		float t = 0;
		float speed = Random.Range(1f, 2f);

		while (t < 1)
		{
			t += Time.deltaTime * speed;

			graphics.localScale = Vector3.Lerp(oldScale, _physicsScale, t);

			yield return null;
		}
	}
}