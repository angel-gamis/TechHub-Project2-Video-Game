using UnityEngine;

public class door : MonoBehaviour
{

	private void OnTriggerEnter2D(Collider2D collision)
	{
        scenesManager.Instance.LoadNextScene();
	}
}
