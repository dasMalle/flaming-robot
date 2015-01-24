using UnityEngine;
using System.Collections;

public enum Player
{
	ONE,
	TWO
}

public class PlayerPosition : MonoBehaviour
{
	public Player player = Player.ONE;
	public string spawnTag;
	Node targetNode;
	int targetNodeId;
	Vector3 moveTarget;
	float movementSpeed = 1.0f;
	AudioSource ambientSource;
	string lastAudioName = "";

	void Start()
	{
		ambientSource = GetComponent<AudioSource> ();

		GameObject[] possibleSpawns = GameObject.FindGameObjectsWithTag (spawnTag);

		ChangeTargetNode(possibleSpawns[Random.Range(0, possibleSpawns.Length)].GetComponent<Node>());
		TeleportToTarget ();
	}

	void Update ()
	{
		transform.position = Vector3.MoveTowards(transform.position,
		                                         moveTarget,
		                                         Time.deltaTime * movementSpeed);
		DebugMovement ();
	}

	public int GetTargetNodeId()
	{
		return targetNodeId;
	}

	public void ChangeTargetNode(Node newNode)
	{
		if(newNode != null)
		{
			targetNode = newNode;
			targetNodeId = newNode.id;
			moveTarget = newNode.transform.position;

			Debug.LogError("New target node: " + targetNodeId);

			if(newNode.ambientClip != null)
			{
				if(newNode.ambientClip.name != lastAudioName)
				{
					ambientSource.clip = newNode.ambientClip;
					ambientSource.loop = true;
					ambientSource.Play();
				}
			}
			else
			{
				ambientSource.Stop();
			}

			lastAudioName = targetNode.ambientClip.name;
		}
	}

	void TeleportToTarget()
	{
		transform.position = moveTarget;
	}

	public void MoveNorth()
	{
		ChangeTargetNode (targetNode.northNode);
	}

	public void MoveSouth()
	{
		ChangeTargetNode (targetNode.southNode);
	}

	public void MoveWest()
	{
		ChangeTargetNode (targetNode.westNode);
	}

	public void MoveEast()
	{
		ChangeTargetNode (targetNode.eastNode);
	}

	void DebugMovement()
	{
		//TODO: Remove debug movement

		if(player == Player.ONE)
		{
			if(Input.GetKeyDown(KeyCode.W))
			{
				MoveNorth();
			}
			else if(Input.GetKeyDown(KeyCode.S))
			{
				MoveSouth();
			}
			else if(Input.GetKeyDown(KeyCode.A))
			{
				MoveWest();
			}
			else if(Input.GetKeyDown(KeyCode.D))
			{
				MoveEast();
			}
		}
		else
		{
			if(Input.GetKeyDown(KeyCode.I))
			{
				MoveNorth();
			}
			else if(Input.GetKeyDown(KeyCode.K))
			{
				MoveSouth();
			}
			else if(Input.GetKeyDown(KeyCode.J))
			{
				MoveWest();
			}
			else if(Input.GetKeyDown(KeyCode.L))
			{
				MoveEast();
			}
		}
	}
}
