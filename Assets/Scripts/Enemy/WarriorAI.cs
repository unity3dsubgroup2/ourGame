﻿using UnityEngine;
using System.Collections;

public class WarriorAI : MonoBehaviour
{
	public GameObject bullet;

	private Transform player;
	private Animator myAnim;
	private Transform jet;
	private NavMeshAgent navMeshAgent;
	private Enemy myHealth;
	private float speed = 3f;
	private Transform weapon;
	private bool isActive = true;
	
	private float shotTimer = 0;
	
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		weapon = transform.Find ("Weapon").transform;
		myAnim = GetComponent<Animator> ();
		navMeshAgent = GetComponent<NavMeshAgent> ();
		myHealth = GetComponent<Enemy> ();
		jet = transform.Find ("Jet");
	}
	
	void Update ()
	{
		if (isActive) {
			if (myHealth.isAlive) {
				navMeshAgent.speed = speed;
		
				float angle = Vector3.Angle (transform.forward, (player.position - transform.position));
				if (angle < 15f) {
					if (shotTimer > myHealth.shotRate && myHealth.isAlive) {
						if (myAnim != null) {
							myAnim.SetBool ("EnemyInSight", true);
						}
						Shot ();
						shotTimer = 0;
					}
				} else {
					if (myAnim != null) {
						myAnim.SetBool ("EnemyInSight", false);
					}
				}
				shotTimer += Time.deltaTime;
				if (myAnim != null) {
					myAnim.SetFloat ("Forward", 0.5f, 0.1f, Time.deltaTime);
				}
				navMeshAgent.SetDestination (player.position);
			} else {
				isActive = false;
				navMeshAgent.Stop ();
				if (jet != null) {
					jet.GetComponent<ParticleSystem> ().enableEmission = false;
				}
				if (myAnim != null) {
					myAnim.SetBool ("EnemyInSight", false);
				}
			}
		}
	}

	void OnAnimatorMove ()
	{
		speed = myAnim.GetFloat ("ForwardSpeed");
	}

	void Shot ()
	{
		GameObject shot = (GameObject)Instantiate (bullet, weapon.position, Quaternion.identity);
		shot.GetComponent<Bullet> ().amount = myHealth.damage;
		shot.GetComponent<Bullet> ().owner = gameObject;
		Vector3 playerBody = new Vector3 (player.position.x, player.position.y, player.position.z);
		shot.GetComponent<Rigidbody> ().AddForce ((playerBody - shot.transform.position).normalized * 1000f);
	}


}
