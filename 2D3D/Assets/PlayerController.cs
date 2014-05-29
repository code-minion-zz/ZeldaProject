using UnityEngine;
using System.Collections;

//[RequireComponent (typeof(CharacterAnimator))]
public class PlayerController : MonoBehaviour 
{
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

	const float SPEEDSCALE = 300;
	const float TIMETOTOPSPEED = 0.5f;
	const float AXIS_DEADZONE = 0.001f;
	CharacterAnimator characterAnimator;
	EDirection lastDirection = EDirection.Invalid;
	float timeFacingThisDirection =0f;
	Vector2 directionValue;
	Vector2 inputValue;
	bool requireChange = false;
	float MovementSpeed = 1.5f;
	
	// Use this for initialization
	void Start () 
	{
		characterAnimator = GetComponent<CharacterAnimator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (InputMovement())
		{
			// begin accelerating character
			Vector3 movement = Vector3.zero;
			movement.x = Input.GetAxis("Horizontal");
			movement.z = Input.GetAxis("Vertical");
			rigidbody.AddForce(movement.normalized * SPEEDSCALE * MovementSpeed * Time.smoothDeltaTime, ForceMode.Force);
//			Debug.Log("Velocity " + rigidbody.velocity);
		}
		else
		{
			// no movement this frame
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

	void OnDrawGizmos()
	{
//		Gizmos.color = Color.yellow;
//		Gizmos.DrawRay(transform.position, transform.forward*100f);
	}
}
