using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;

public class shooting : MonoBehaviour
{
	[SerializeField] playerController playerController;
	private Camera cam;
	private Vector3 mousePosition;

	// Shooting
	
	public GameObject abilityBullet;
	public Transform abilityBulletTrans;
	public GameObject bulletTrans;
	private bool isAiming = false;
	private bool canShoot;

	[SerializeField] Transform player;

	// Start is called before the first frame update
	void Start()
	{
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}

	// Update is called once per frame
	void Update()
	{
		if (playerController.playerStatus)
		{
			Vector2 playerPos = new Vector2(player.position.x, player.position.y);
			// Follow Player
			transform.position = playerPos;

			// Aiming and rotating magic
			if (Input.GetMouseButton(1))
			{
				bulletTrans.SetActive(true);
				isAiming = true;

				mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

				Vector3 rotation = mousePosition - transform.position;

				float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

				transform.rotation = Quaternion.Euler(0, 0, rotZ);
			}

			// Shooting Magic
			if (Input.GetMouseButtonUp(1))
			{
				bulletTrans.SetActive(false);
				isAiming = false;
				Instantiate(abilityBullet, abilityBulletTrans.position, Quaternion.identity);
			}
		}

	}
}
