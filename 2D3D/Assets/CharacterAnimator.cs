using UnityEngine;
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
			if (direction != value)
			{
				direction = value;
				requireChange = true;
				SetDirection();
			}
		}
	}

	bool requireChange = false;

	// Use this for initialization
	void Start () 
	{
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (requireChange)
		{			
			animator.SetTrigger("ChangeState");
			requireChange = false;
		}

		if (Input.anyKeyDown || Input.anyKey)
		{
			EDirection newDirection = ParseInputDirection();
			Direction = newDirection;

			if (CurrentState == EAnimType.Idle)
			{
				if (StartWalking(newDirection))
				{
					// set animtype to walking
					CurrentState = EAnimType.Walk;
				}
			}
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
		inputDirection.x = Input.GetAxis("Horizontal");
		inputDirection.y = Input.GetAxis("Vertical");
		
		if (inputDirection.x > 0) // face right
		{
			if (inputDirection.y > 0)
			{
				return EDirection.Northeast;
			}
			else if (inputDirection.y < 0)
			{
				return EDirection.Southeast;
			}
			else
			{
				return EDirection.East;
			}
		}
		else if (inputDirection.x < 0) // face left
		{
			if (inputDirection.y > 0)
			{
				return EDirection.Northwest;
			}
			else if (inputDirection.y < 0)
			{
				return EDirection.Southwest;
			}
			else
			{
				return EDirection.West;
			}			
		}
		else
		{
			if (inputDirection.y > 0)
			{
				return EDirection.North;
			}
			else
			{
				return EDirection.South;
			}
		}
	}

	void SetAnimType()
	{
		Debug.Log(currentState);
		animator.SetInteger("AnimType", (int)currentState);
	}

	void SetDirection()
	{
		Debug.Log(direction);
		animator.SetInteger("Direction", (int)direction);
	}
}
