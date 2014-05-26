﻿using UnityEngine;
using System.Collections;

public class CharacterAnimator : MonoBehaviour {

	public enum EDirection
	{
		Invalid = -1,
		North,
		Northeast,
		East,
		Southeast,
		South,
		Southwest,
		West,
		Northwest,
		Directionless // for animations with only one or no apparent direction
	}

	public enum EAnimType
	{
		Invalid = -1,
		Idle,
		Sitting,
		Sleeping,
		Walk,
		Hover,
		Fly,
		Kick,
		Punch,
		Stomp,
		TakingDamage,
		Faint,
		Special
	}

	Animator animator;
	EAnimType currentState = EAnimType.Idle;
	public EAnimType CurrentState
	{
		get 
		{
			return currentState;
		}
		set 
		{
			if (currentState != value)
			{
				currentState = value;
				requireChange = true;
				SetAnimType();
			}
		}
	}
	EDirection direction = EDirection.South;
	public EDirection Direction
	{
		get 
		{
			return direction;
		}
		set 
		{
			lastDirection = direction;
			direction = value;
		}
	}

	EDirection lastDirection = EDirection.Invalid;
	float timeFacingThisDirection =0f;
	const float TIMETOMOVE = 0.5f;
	Vector2 directionValue;
	bool requireChange = false;

	// Use this for initialization
	void Start () 
	{
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
//		if (requireChange)
//		{			
//			animator.SetTrigger("ChangeState");
//			requireChange = false;
//		}

		if (Input.anyKeyDown || Input.anyKey)
		{

			EDirection newDirection = ParseInputDirection();
//			if (newDirection == EDirection.Invalid)
//			{
//				CurrentState = EAnimType.Idle;
//				SetSpeed(0f);
//				return;
//			}

			if (newDirection == Direction)
			{
				timeFacingThisDirection += Time.smoothDeltaTime;
				if (CurrentState == EAnimType.Idle)
				{
					if (timeFacingThisDirection > TIMETOMOVE)
					{
						// set animtype to walking
						CurrentState = EAnimType.Walk;
						SetSpeed(1f);
					}
				}
			}
			else
			{
				Direction = newDirection;
				timeFacingThisDirection = 0f;
			}

			SetDirection();



		}
	}

	/// <summary>
	/// When moving from idle state, stay idle if changing direction, else, move
	/// </summary>
	bool StartWalking(EDirection direction)
	{
		return (direction == Direction);
	}

	EDirection ParseInputDirection()
	{
		Vector2 inputDirection;
		inputDirection.x = Input.GetAxisRaw("Horizontal");
		inputDirection.y = Input.GetAxisRaw("Vertical");

		
		if (inputDirection == Vector2.zero)
		{
			return EDirection.Invalid;
		}

		if (inputDirection.x > 0) // face right
		{
			if (inputDirection.y > 0)
			{
				directionValue.y = 1;
				directionValue.x = 1;
				return EDirection.Northeast;
			}
			else if (inputDirection.y < 0)
			{
				directionValue.y = -1;
				directionValue.x = 1;
				return EDirection.Southeast;
			}
			else
			{
				directionValue.y = 0;
				directionValue.x = 1;
				return EDirection.East;
			}
		}
		else if (inputDirection.x < 0) // face left
		{
			if (inputDirection.y > 0)
			{
				directionValue.y = 1;
				directionValue.x = -1;
				return EDirection.Northwest;
			}
			else if (inputDirection.y < 0)
			{
				directionValue.y = -1;
				directionValue.x = -1;
				return EDirection.Southwest;
			}
			else
			{
				directionValue.y = 0;
				directionValue.x = -1;
				return EDirection.West;
			}			
		}
		else
		{
			if (inputDirection.y > 0)
			{
				directionValue.y = 1;
				directionValue.x = 0;
				return EDirection.North;
			}
			else
			{
				directionValue.y = -1;
				directionValue.x = 0;
				return EDirection.South;
			}
		}
	}

	void SetAnimType()
	{
		Debug.Log(currentState);
		animator.SetInteger("AnimType", (int)currentState);
	}

	void SetSpeed(float _speed)
	{
		animator.SetFloat("Speed", _speed);
	}

	void SetDirection()
	{
		animator.SetFloat("MoveX", directionValue.x);
		animator.SetFloat("MoveY", directionValue.y);
	}
}
