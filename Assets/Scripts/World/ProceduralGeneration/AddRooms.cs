using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// to add NavMeshSurface to teh proyect you have to add by name the package "com.unity.ai.navigation" on unity

public class AddRooms : MonoBehaviour
{
	private RoomTemplates templates;

	void Start()
	{
		templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
		templates.rooms.Add(this.gameObject);
		templates.waitOtherRoom = 0f;
	}
}
