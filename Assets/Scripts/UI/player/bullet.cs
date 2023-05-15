using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class bullet : MonoBehaviour
{
	private Vector3 mousePosition;
	private Camera cam;
	private Rigidbody2D rigid;
	private ParticleSystem particle;
	public Sprite vine;
	private float force = 16f;
	private float SDtimer;
	private float bulletTime = 15f;
	public float timer;

	// Start is called before the first frame update
	void Start()
	{
		// Moves Bullet When Created on Fire
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		rigid = GetComponent<Rigidbody2D>();
		mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
		Vector3 direction = mousePosition - transform.position;
		Vector3 rotation = transform.position - mousePosition;
		rigid.velocity = new Vector2(direction.x, direction.y).normalized * force;
		float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0, 0 , rot + 90);

		particle = gameObject.GetComponent<ParticleSystem>();
	}

	// Destroys Bullet
	private void DestroyBullet(){
		Destroy(gameObject);
	}

	// Update is called once per frame
	void Update()
	{   
		// Destroys bullet after set time
		SDtimer += Time.deltaTime;
		if(SDtimer > bulletTime){
			DestroyBullet();
		}

		timer += Time.deltaTime;
	}

	// When power hits walls or ground
	private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Ground"){
			Destroy(particle);
			SpriteRenderer rend = gameObject.AddComponent<SpriteRenderer>();
			rend.sprite = vine;
			// Create vines
			rigid.velocity = new Vector2(0, 0);
		}
	}


	
}
