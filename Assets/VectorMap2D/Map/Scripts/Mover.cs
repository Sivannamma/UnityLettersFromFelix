using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
	[SerializeField] GameObject GoodJobDialog;
	[SerializeField][Tooltip("The speed of the player")]
	float speed = 50f;
	[SerializeField]
	[Tooltip("delay of the coroutine")]
	float timeBetweenSteps = 0.1f;
	List<Coroutine> list = new List<Coroutine>();
	public bool isMoving = false;
	Animator anim;
	public static bool isCorrect;
	public bool isArrived;
	// Start is called before the first frame update


	// vector3 move torwards (check later)
	void Start()
    {
		anim = GetComponent<Animator>();
        
    }

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (this.GetComponent<checkCollisionWithPanel>().checkCollision(Input.mousePosition))
			{
				// do nothing
			}
			else
			{
				if (isArrived)
				{
					isArrived = false;
					anim.SetBool("Dance", false);
				}

				Vector3 currentPosition = transform.position;
				if (list.Count > 0) // if there are coroutine, then we want to stop them
					stopCoroutine();
				Vector3 mouse = Input.mousePosition;
				Ray castPoint = Camera.main.ScreenPointToRay(mouse);
				RaycastHit hit;
				if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
				{
					mouse.z = 0f;
					MoveTowardsTheTarget(currentPosition, hit.point);
				}
				isMoving = false;
			}

		}
	}
	public void MoveTowardsTheTarget(Vector3 currentPosition, Vector3 destination)
	{
		// in order to determine in which position the player needs to walk .
		if (destination.x > currentPosition.x)
			list.Add(StartCoroutine(MoveTowardsTheTargetHelp(destination.x, currentPosition.x, speed, true)));
		else if (destination.x < currentPosition.x)
			list.Add(StartCoroutine(MoveTowardsTheTargetHelp(currentPosition.x, destination.x, -speed, true)));
		if (destination.y > currentPosition.y)
			list.Add(StartCoroutine(MoveTowardsTheTargetHelp(destination.y, currentPosition.y, speed, false)));
		else if (destination.y < currentPosition.y)
			list.Add(StartCoroutine(MoveTowardsTheTargetHelp(currentPosition.y, destination.y, -speed, false)));

	}
	IEnumerator MoveTowardsTheTargetHelp(float big, float small, float offSet, bool axis)
	{
		while (big > small)
		{
			setAnimationFalse();
			if (axis)
			{
				transform.position += new Vector3(offSet, 0, 0);
				anim.SetBool(getState(offSet,true), true);
			}
			else
			{
				transform.position += new Vector3(0, offSet, 0);
				anim.SetBool(getState(offSet, false), true);
			}
			if(offSet<0)
				big += offSet;
			else
				small += offSet;
			yield return new WaitForSeconds(timeBetweenSteps);

		}
		setAnimationFalse();
        if (axis == true || list.Count == 1)
        {
			list.RemoveAt(0);
        }
        else
        {
			list.RemoveAt(1);
		}
		if (isCorrect && list.Count ==0)
		{
			anim.SetBool("Dance", true);
			GoodJobDialog.SetActive(true);
			isArrived = true;
			isCorrect = false;
		}
	}

	// get the state for the animation, which animation we want to set to true
	private string getState(float offSet, bool axis)
    {
		string ans;
		if (offSet < 0)
			ans =  axis == true ? "WalkingLeft" : "WalkingDown" ;
		else
			ans= axis == true ? "WalkingRight" : "WalkingUp";
		if (ans.Equals("WalkingUp"))
			this.transform.rotation= new Quaternion(0, -180, 0, 0);
		else
			this.transform.rotation= new Quaternion(0,0,0,0);

		return ans;
	}

	// function that is responsible for turning all the animations of the player to false
	private void setAnimationFalse()
    {
		anim.SetBool("WalkingLeft", false);
		anim.SetBool("WalkingRight", false);
		anim.SetBool("WalkingDown", false);
		anim.SetBool("WalkingUp", false);		
	}

	// function that is responsible for stopping all of the coroutines (if the player decides to click other place, we want to stop moving, and set the new destination)
	private void stopCoroutine()
    {
		foreach(Coroutine c in list){
			StopCoroutine(c);
		}
		// clearing the list because we stopped all the coroutine
		list.Clear();
    }

	// setting the dance to a current state, from the camera ctrl class
	public static void setDance(bool state)
    {
		isCorrect = state;
    }
}
