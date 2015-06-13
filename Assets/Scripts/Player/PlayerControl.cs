using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	public GameObject bullet;
	public GameObject missile;
	public GameObject mainMenu;
	public bool isWeapon1Active = true;
	public bool isWeapon2Active = true;
	public bool isMissileActive = true;

	private int layerMask;		// for Raycast ignoring
	private Transform weapon1;
	private Transform weapon2;
	private Transform weaponMissile;
	private float effectsTime = 0.1f;
	private LineRenderer lazer;
	private bool raycastResult = false;

	private float shotTimer;
	private float missileTimer;
	private Light missileIndicator;
	private float rateFire;
	private float rateMissile;
	private bool isMissileReady = false;
	private int fireCount = 0;
	private float defaultPitch;


	void Start ()
	{
		layerMask = ~(LayerMask.GetMask ("Ignore Raycast") | LayerMask.GetMask ("Player"));
		weapon1 = transform.Find ("Turret/Gun1/Weapon").transform;
		weapon2 = transform.Find ("Turret/Gun2/Weapon").transform;
		weaponMissile = transform.Find ("Turret/MissileSystem/Weapon").transform;
		ReactivateWeapons ();
		missileIndicator = transform.Find ("Turret/MissileSystem/ReadyIndicator").GetComponent<Light> ();
		shotTimer = 0;
		rateFire = PlayerHealth.playerHealth.rateFire;
		rateMissile = PlayerHealth.playerHealth.rateMissile;
		lazer = transform.Find ("Turret/Laser").GetComponent<LineRenderer> ();
		lazer.SetVertexCount (2);
		defaultPitch = GetComponent<AudioSource> ().pitch;
	}

	void FixedUpdate ()
	{
		lazer.materials [0].mainTextureOffset += new Vector2 (Time.deltaTime * 0.1f, 0.0f);
		lazer.materials [0].SetTextureOffset ("_NoiseTex", new Vector2 (-Time.time, 0.0f));
	}

	void Update ()
	{
		if (PlayerHealth.playerHealth.isAlive) {
			RaycastHit hitInfo;
			raycastResult = Physics.Raycast (lazer.transform.position, weapon1.forward, out hitInfo, 100f, layerMask);
			// draw lazer
			if (lazer != null && raycastResult) {
				lazer.SetPosition (0, lazer.transform.position);
				lazer.SetPosition (1, hitInfo.point);
			} else {
				lazer.SetPosition (0, lazer.transform.position);
				lazer.SetPosition (1, lazer.transform.position + weapon1.forward * 100f);
			}
			// shot
			if (Input.GetMouseButton (0)) {
				if (shotTimer > rateFire / 2f) {
					if (raycastResult && hitInfo.collider.tag != "Player") {
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
			// lunch missile
			if (isMissileActive) {
				if (!isMissileReady) {
					missileTimer += Time.deltaTime;
					missileIndicator.intensity = missileTimer;
					if (missileTimer > rateMissile) {
						isMissileReady = true;
					}
				} else { // blink ready indicator
					missileIndicator.intensity = ((int)(Time.time * 2f) % 2 == 0) ? 0 : missileTimer;
				}
				if (Input.GetMouseButtonDown (1) && isMissileReady && (hitInfo.collider.tag == "Enemy" || hitInfo.collider.tag == "Respawn")) {
					GameObject missileObj = (GameObject)Instantiate (missile, weaponMissile.position, new Quaternion (0.7f, 0, 0, -0.7f));
					missileObj.GetComponent<Missile> ().target = hitInfo.collider.gameObject;
					missileObj.GetComponent<Missile> ().owner = gameObject;
					missileObj.GetComponent<Missile> ().amount = 200f;
					missileTimer = 0;
					missileIndicator.intensity = 0f;
					isMissileReady = false;
				}
			}
		} else {
			mainMenu.GetComponent<mnuMain> ().GameOver (false);
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

	public void ReactivateWeapons ()
	{
		if (isWeapon1Active) {
			transform.Find ("Turret/Gun1").gameObject.SetActive (true);
			mainMenu.GetComponent<mnuMain> ().imgGun1.color = new Color (0.863f, 0.51f, 0.275f, 1);
		} else {
			transform.Find ("Turret/Gun1").gameObject.SetActive (false);
			mainMenu.GetComponent<mnuMain> ().imgGun1.color = new Color (1, 1, 1, 0.294f);
		}
		if (isWeapon2Active) {
			transform.Find ("Turret/Gun2").gameObject.SetActive (true);
			mainMenu.GetComponent<mnuMain> ().imgGun2.color = new Color (0.863f, 0.51f, 0.275f, 1);
		} else {
			transform.Find ("Turret/Gun2").gameObject.SetActive (false);
			mainMenu.GetComponent<mnuMain> ().imgGun2.color = new Color (1, 1, 1, 0.294f);
			
		}
		if (isMissileActive) {
			transform.Find ("Turret/MissileSystem").gameObject.SetActive (true);
			mainMenu.GetComponent<mnuMain> ().imgGun3.color = new Color (0.863f, 0.51f, 0.275f, 1);
		} else {
			transform.Find ("Turret/MissileSystem").gameObject.SetActive (false);
			mainMenu.GetComponent<mnuMain> ().imgGun3.color = new Color (1, 1, 1, 0.294f);
		}
	}

	void OnCollisionEnter (Collision collision)
	{
		if (collision.gameObject.tag == "Untagged" && !GetComponent<AudioSource> ().isPlaying) {
			GetComponent<AudioSource> ().pitch = defaultPitch + Random.Range (-0.1f, 0.1f);
			GetComponent<AudioSource> ().Play ();
		}
	}
}
