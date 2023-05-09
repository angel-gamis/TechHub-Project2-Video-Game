using Unity.VisualScripting;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
	[SerializeField] playerController playerController;
	private Transform player;
	private float smoothness = 0.125f;
	private Vector3 offset;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}

	private void FixedUpdate()
	{
		if (playerController.playerStatus)
		{
			offset = new Vector3(0, 0, -10);
			Vector3 desiredPosition = player.position + offset;
			Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothness);
			transform.position = smoothedPosition;
		}
	}
}

