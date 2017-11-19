using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class buttonScript : MonoBehaviour, IPointerDownHandler {


	void Start()
	{
		addPhysicsRaycaster();
	}

	void addPhysicsRaycaster()
	{
		PhysicsRaycaster physicsRaycaster = GameObject.FindObjectOfType<PhysicsRaycaster>();
		if (physicsRaycaster == null)
		{
			Camera.main.gameObject.AddComponent<PhysicsRaycaster>();
			Debug.Log ("Added ray Caster");
		}
	}
	void Update()
	{
		Debug.Log ("update");
	}
	public void OnPointerDown(PointerEventData eventData)
	{
		Debug.Log ("Clciked " + eventData.pointerCurrentRaycast.gameObject.name);
	}
}
