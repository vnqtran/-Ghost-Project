﻿using UnityEngine;
using System.Collections;

public class MainController : MonoBehaviour {

	public float moveSpeed;
	public float rotationSpeed;
	public float jumpSpeed;
	private Vector3 moveDirection = Vector3.zero;
	CharacterController main;

	protected Animator anim;

	// Use this for initialization
	void Start () {
		main = GetComponent<CharacterController>();
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
		main.SimpleMove(Physics.gravity);
		transform.Rotate (new Vector3(0,Input.GetAxis ("Horizontal")*rotationSpeed,0));
		main.SimpleMove(Input.GetAxis("Vertical")*moveSpeed*transform.TransformDirection(Vector3.forward));
		if(main.isGrounded){
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= jumpSpeed;
			if (Input.GetButton("Jump")){
			moveDirection.y = jumpSpeed;
			}
		}
	

		if(anim){
			AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
			
			if(Input.GetButton("Vertical") || Input.GetButton("Horizontal")){
				anim.SetFloat ("Speed",1);
			}
			if(Input.GetButtonDown ("Jump")){
				anim.SetFloat ("Jump",1);
			}
			if(!Input.anyKey){
				anim.SetFloat ("Speed",3);
				anim.SetFloat("Jump",0);
			}
			//if we're in "Run" mode, respond to input for jump, and set the Jump parameter accordingly. 
			/*if(stateInfo.nameHash == Animator.StringToHash("Base Layer.BaseLayer"))
			{
				if(Input.GetButtonDown ("Vertical") || Input.GetButtonDown ("Horizontal")){
					anim.SetFloat ("Speed",1);
				}
				if(Input.anyKey == false){
					anim.SetFloat ("Speed",3);
				}
			}
			if(stateInfo.shortNameHash == Animator.StringToHash("Base Layer.Walk")){
				if(Input.anyKey == false){
					anim.SetFloat ("Speed",3);
				}*/
			}
		}
	}