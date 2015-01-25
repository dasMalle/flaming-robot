using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
	public static bool isGameRunning = false;
	public PlayerPosition[] players;
	public Player activePlayer;
	public GameObject globalListener;
	const float updateRate = 7.0f;
	public SwapButtonSprite swap;

	void Start ()
	{
		StartCoroutine ("UpdatePlayerPositions");
		ActivatePlayer (activePlayer);
	}

	void StartGame()
	{
		isGameRunning = true;
	}

	void StopGame()
	{
		isGameRunning = false;
	}

	void ActivatePlayer(Player newPlayer)
	{
		for(int i = 0; i < players.Length; ++i)
		{
			if(players[i].player == newPlayer)
			{
				globalListener.transform.parent = players[i].transform;
				globalListener.transform.localPosition = Vector3.zero;
				globalListener.transform.localRotation = Quaternion.identity;
			}
		}
	}

	public void MovePlayer(Node.Direction dir, Player player)
	{
		if(player == Player.ONE)
		{
			if(dir == Node.Direction.NORTH)
			{
				players[0].MoveNorth();
			}
			if(dir == Node.Direction.SOUTH)
			{
				players[0].MoveSouth();
			}
			if(dir == Node.Direction.WEST)
			{
				players[0].MoveWest();
			}
			if(dir == Node.Direction.EAST)
			{
				players[0].MoveEast();
			}
		}
		else
		{
			if(dir == Node.Direction.NORTH)
			{
				players[1].MoveNorth();
			}
			if(dir == Node.Direction.SOUTH)
			{
				players[1].MoveSouth();
			}
			if(dir == Node.Direction.WEST)
			{
				players[1].MoveWest();
			}
			if(dir == Node.Direction.EAST)
			{
				players[1].MoveEast();
			}
		}

		SetPlayersToDatabase ();
	}

	bool firstTimeGetPositions = true;

	IEnumerator UpdatePlayerPositions()
	{
		while(true)
		{
			Debug.Log("Co1 start: " + Time.time);

			if(isGameRunning)
			{
				Vector2 positions = DatabaseSaver.GetNodes();

				if (positions.x != -1)
				{
					Debug.Log("Node positions: " + positions);

					if(positions.x == positions.y)
					{
						Debug.Log("You found each other yai! <3");
						isGameRunning = false;
						GetComponent<AudioSource>().Play();
						yield return new WaitForSeconds(10.0f);
						positions = DatabaseSaver.GetNodes();

						if(positions.x == positions.y)
						{
							players[0].RandomSpawn();
							players[1].RandomSpawn();
							SetPlayersToDatabase ();
						}

						isGameRunning = true;
					}
					else
					{
						//Debug.LogError ("Player positions in database " + positions.x + " and " + positions.y);
						players[0].ChangeTargetNode(Node.GetNodeById((int)positions.x));
						players[1].ChangeTargetNode(Node.GetNodeById((int)positions.y));
					}

					if(firstTimeGetPositions)
					{
						firstTimeGetPositions = false;
						players[0].TeleportToTarget();
						players[1].TeleportToTarget();
					}
				}
			}

			yield return new WaitForSeconds(updateRate);
		}
	}

	public void SetPlayersToDatabase()
	{
		Debug.Log("Saving nodes to database: " + players[0].GetTargetNodeId() + ", " + players[1].GetTargetNodeId());
		DatabaseSaver.SaveNodes (players [0].GetTargetNodeId(),
		                         players [1].GetTargetNodeId());
	}

	public void ChangePlayer()
	{
		if(activePlayer == Player.ONE)
		{
			activePlayer = Player.TWO;
		}
		else
		{
			activePlayer = Player.ONE;
		}

		swap.ToggleSprite ();
		ActivatePlayer (activePlayer);
	}

	public void RotatePlayerClockwiseConstantly()
	{
		players[0].GetComponent<RotateAround>().isConstantlyRotating = true;
		players[0].GetComponent<RotateAround>().isRotatingDirectionClockwise = true;
		players[1].GetComponent<RotateAround>().isConstantlyRotating = true;
		players[1].GetComponent<RotateAround>().isRotatingDirectionClockwise = true;
	}

	public void RotatePlayerCounterClockwiseConstantly()
	{
		players[0].GetComponent<RotateAround>().isConstantlyRotating = true;
		players[0].GetComponent<RotateAround>().isRotatingDirectionClockwise = false;
		players[1].GetComponent<RotateAround>().isConstantlyRotating = true;
		players[1].GetComponent<RotateAround>().isRotatingDirectionClockwise = false;
	}

	public void StopPlayerRotating()
	{
		players[0].GetComponent<RotateAround>().isConstantlyRotating = false;
		players[1].GetComponent<RotateAround>().isConstantlyRotating = false;
	}

	public void RotatePlayerClockwise()
	{
		players[0].GetComponent<RotateAround>().RotateClockwise();
		players[1].GetComponent<RotateAround>().RotateClockwise();
	}

	public void RotatePlayerCounterClockwise()
	{
		players[0].GetComponent<RotateAround>().RotateCounterClockwise();
		players[1].GetComponent<RotateAround>().RotateCounterClockwise();
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}

		if(Input.GetKeyDown(KeyCode.Tab))
		{
			ChangePlayer();
		}
	}
}
