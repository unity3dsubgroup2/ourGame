using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	public GameObject bullet;
	public bool isWeapon1Active = true;
	public bool isWeapon2Active = true;

	private int layerMask;		// for Raycast ignoring
	private Transform weapon1;
	private Transform weapon2;
	private float effectsTime = 0.1f;
	private LineRenderer lazer;

	private float shotTimer;
	private int fireCount = 0;
	private float rateFire;


	void Start ()
	{
		layerMask = ~LayerMask.GetMask ("Ignore Raycast");
		weapon1 = transform.Find ("Turret/Gun1/Weapon").transform;
		weapon2 = transform.Find ("Turret/Gun2/Weapon").transform;
		shotTimer = 0;
		rateFire = PlayerHealth.playerHealth.rateFire;
		lazer = transform.Find ("Turret/Laser").GetComponent<LineRenderer> ();
	}
	
	void Update ()
	{
		RaycastHit hitInfo;
		Physics.Raycast (weapon1.transform.position, weapon1.forward, out hitInfo, Mathf.Infinity, layerMask);
		// draw lazer
		if (lazer != null) {
			lazer.materials [0].mainTextureOffset += new Vector2 (Time.deltaTime * 0.1f, 0.0f);
			lazer.materials [0].SetTextureOffset ("_NoiseTex", new Vector2 (-Time.time, 0.0f));
			lazer.SetPosition (0, lazer.transform.position);
			lazer.SetPosition (1, hitInfo.point);
		}
		// shot
		if (Input.GetMouseButton (0)) {
			if (shotTimer > rateFire / 2f) {
				if (hitInfo.collider.tag != "Player") {
					if (fireCount % 2 == 0) {
						if (isWeapon1Active)
							Shot (hitInfo, weapon1);
					} else {
						if (isWeapon2Active)
							Shot (hitInfo, weapon2);
					}
					fireCount++;
					shotTimer = 0;
				}
			}
		}
		shotTimer += Time.deltaTime;
		if (shotTimer > effectsTime) {
			weapon1.GetComponent<Light> ().enabled = false;
			weapon2.GetComponent<Light> ().enabled = false;
		}
	}

	void Shot (RaycastHit hitInfo, Transform weapon)
	{
		GameObject shot = (GameObject)Instantiate (bullet, weapon.position, Quaternion.identity);
		shot.GetComponent<Bullet> ().owner = gameObject;
		shot.GetComponent<Bullet> ().amount = PlayerHealth.playerHealth.damage;
		shot.GetComponent<Rigidbody> ().AddForce ((hitInfo.point - shot.transform.position).normalized * 1000f);
		weapon.GetComponent<ParticleSystem> ().Emit (10);
		weapon.GetComponent<Light> ().enabled = true;
		weapon.GetComponent<AudioSource> ().Play ();
	}
}
