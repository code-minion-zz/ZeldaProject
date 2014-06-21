using UnityEngine;
using System.Collections;

//[RequireComponent (typeof(CharacterAnimator))]
public class ActorController : MonoBehaviour 
{
	public enum EControledBy
	{
		Invalid = -1,
		Human,
		Computer,
		Max
	}
	public EControledBy ControlledBy;

	public enum ECharacterStatus
	{
		Normal = 0,
		Slowed,
		Sitting,
		Stunned,
		Frozen,
		Dead,
	}
	public ECharacterStatus Status;

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
	
	bool CanMove
	{
		get
		{
			if (Status == ECharacterStatus.Normal)	return true;
			else return false;
		}
	}
	
	bool CanChangeDirection
	{
		get
		{
			if (Status == ECharacterStatus.Normal)	return true;
			else return false;
		}
	}

	const float SPEEDSCALE = 300;
	const float TIMETOTOPSPEED = 0.5f;
	const float AXIS_DEADZONE = 0.001f;
	public CharacterAnimator characterAnimator;
	EDirection lastDirection = EDirection.Invalid;
	float timeFacingThisDirection =0f;
	Vector2 directionValue;
	Vector2 inputValue;
	bool requireChange = false;
	float MovementSpeed = 1.5f;
	InputDevice input;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch (ControlledBy)
		{
		case EControledBy.Human:
		{
			if(Input.GetKeyDown(KeyCode.Space)) {
				characterAnimator.callJump();
			}
			if (InputMovement())
			{
				// begin accelerating character
				Vector2 movement = Vector3.zero;
				movement.x = Input.GetAxis("Horizontal");
				movement.y = Input.GetAxis("Vertical");

				Move (movement);

//				if (rigidbody.velocity.magnitude < 1.5f)
//				{
//					rigidbody.AddForce(
//						movement.normalized * SPEEDSCALE * MovementSpeed * Time.smoothDeltaTime, 
//						ForceMode.Force
//						);
//				}

			}
			else
			{
				// no movement this frame
				characterAnimator.UpdateMovementSpeed(0f);
			}
		}
		break;
		case EControledBy.Computer:
		{

		}
		break;

		}
	}
		
	void Move (Vector2 direction)
	{
		if (CanChangeDirection)
		{
			transform.forward = new Vector3(direction.x, 0, direction.y);
				
			// send input to animator
			Vector2 cartesian;
			
			// GetAxis smooths keyboard input, resulting in a delay when inputs go from 1 to 0
			//  this doesn't matter much in gameplay, but looks bad in the animator.
			//  for this reason, we use GetAxisRaw for that instead.
			
			//  TODO : This assumes keyboard input, test and address potential gamepad issues.
			cartesian.x = Input.GetAxisRaw("Horizontal");
			cartesian.y = Input.GetAxisRaw("Vertical");
			characterAnimator.UpdateDirection(cartesian);
		}

		if (CanMove)
		{
			if (rigidbody.velocity.magnitude < 1.5f)
			{
				rigidbody.AddForce(
					transform.forward * SPEEDSCALE * MovementSpeed * Time.smoothDeltaTime, 
					ForceMode.Force
					);
				characterAnimator.UpdateMovementSpeed(1f);
			}
		}
	}

	public static bool InputMovement()
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		horizontal = Mathf.Abs(horizontal) > AXIS_DEADZONE ? horizontal : 0f;
		vertical = Mathf.Abs(vertical) > AXIS_DEADZONE ? vertical : 0f;
		return !((horizontal == 0) && (vertical == 0));
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Default")) return;

		if (other.gameObject.layer != gameObject.layer)
		{
			//if (other.transform.name == "feet")
			if (ControlledBy == EControledBy.Computer)
			{
				// push!
				Vector3 direction = -1 * (other.transform.position-transform.position);

				rigidbody.AddForce(direction * 500);

				Debug.Log(direction);
			}
//			else
//			{
//
//			}
		}
		//Debug.Log("Collided with " + other.ToString());
	}

	void OnDrawGizmos()
	{
//		Gizmos.color = Color.yellow;
//		Gizmos.DrawRay(transform.position, transform.forward*100f);
	}
}
