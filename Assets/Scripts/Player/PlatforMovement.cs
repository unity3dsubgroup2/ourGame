using UnityEngine;
using System.Collections;

public class PlatforMovement : MonoBehaviour
{
	public float speed = 6f;

/*	private ParticleSystem pUp;
	private ParticleSystem pDown;
	private ParticleSystem pLeft;
	private ParticleSystem pRight;*/
	private Rigidbody myRigidbody;
	private Transform turret;
	private Transform weapon;
	private Vector3 turretPos;
	private Vector3 mousePos;
	private LineRenderer lazer;

	void Start ()
	{
		/*
		pUp = transform.Find ("Platform/Jets/Up/pUp").GetComponent<ParticleSystem> ();
		pDown = transform.Find ("Platform/Jets/Down/pDown").GetComponent<ParticleSystem> ();
		pLeft = transform.Find ("Platform/Jets/Left/pLeft").GetComponent<ParticleSystem> ();
		pRight = transform.Find ("Platform/Jets/Right/pRight").GetComponent<ParticleSystem> ();
*/
		myRigidbody = GetComponent<Rigidbody> ();
		turret = transform.Find ("Turret").transform;
		weapon = transform.Find ("Turret/Weapon");
		lazer = transform.Find ("Turret/Weapon").GetComponent<LineRenderer> ();
	}

	void FixedUpdate ()
	{
		float v = Input.GetAxis ("Vertical");
		float h = Input.GetAxis ("Horizontal");
		myRigidbody.velocity = new Vector3 (h * speed, 0f, v * speed);
		transform.rotation = new Quaternion (v / speed, 0f, -h / speed, 1f);

		// rotate turret
		turretPos = Camera.main.WorldToScreenPoint (turret.position);
		turretPos = new Vector3 (turretPos.x, 0f, turretPos.y);
		mousePos = new Vector3 (Input.mousePosition.x, 0f, Input.mousePosition.y);
		turret.rotation = Quaternion.LookRotation (mousePos - turretPos);

		// draw lazer
		lazer.materials [0].mainTextureOffset += new Vector2 (Time.deltaTime * 0.1f, 0.0f);
		lazer.materials [0].SetTextureOffset ("_NoiseTex", new Vector2 (-Time.time, 0.0f));
		RaycastHit hitInfo;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hitInfo, 20f)) {
			if (hitInfo.collider.tag != "Enemy" && hitInfo.collider.tag != "Environment" && hitInfo.collider.tag != "Warrior") {
				Physics.Raycast (weapon.transform.position, weapon.forward, out hitInfo);
			}
		}
		lazer.SetPosition (0, weapon.position);
		lazer.SetPosition (1, hitInfo.point);
	}
}