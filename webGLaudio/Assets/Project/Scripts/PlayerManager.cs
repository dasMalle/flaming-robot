using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
	public static bool isGameRunning = false;
	public PlayerPosition[] players;
	public Player activePlayer;
	public GameObject globalListener;
	const float updateRate = 3.0f;

	void Start ()
	{
		StartCoroutine ("UpdatePlayerPositions");
		ActivatePlayer (activePlayer);
		Invoke ("StartGame", 5.0f);
	}

	void StartGame()
	{
		isGameRunning = true;
		SetPlayersToDatabase ();
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

	IEnumerator UpdatePlayerPositions()
	{
		while(true)
		{
			if(isGameRunning)
			{
				Vector2 positions = DatabaseSaver.GetNodes();
				Debug.LogError ("Player positions in database " + positions.x + " and " + positions.y);
				players[0].ChangeTargetNode(Node.GetNodeById((int)positions.x));
				players[1].ChangeTargetNode(Node.GetNodeById((int)positions.y));
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

		ActivatePlayer (activePlayer);
	}

	public void RotatePlayerClockwiseConstantly()
	{
		if(activePlayer == Player.ONE)
		{
			players[0].GetComponent<RotateAround>().isConstantlyRotating = true;
			players[0].GetComponent<RotateAround>().isRotatingDirectionClockwise = true;
		}
		else
		{
			players[1].GetComponent<RotateAround>().isConstantlyRotating = true;
			players[1].GetComponent<RotateAround>().isRotatingDirectionClockwise = true;
		}
	}

	public void RotatePlayerCounterClockwiseConstantly()
	{
		if(activePlayer == Player.ONE)
		{
			players[0].GetComponent<RotateAround>().isConstantlyRotating = true;
			players[0].GetComponent<RotateAround>().isRotatingDirectionClockwise = false;
		}
		else
		{
			players[1].GetComponent<RotateAround>().isConstantlyRotating = true;
			players[1].GetComponent<RotateAround>().isRotatingDirectionClockwise = false;
		}
	}

	public void StopPlayerRotating()
	{
		if(activePlayer == Player.ONE)
		{
			players[0].GetComponent<RotateAround>().isConstantlyRotating = false;
		}
		else
		{
			players[1].GetComponent<RotateAround>().isConstantlyRotating = false;
		}
	}

	public void RotatePlayerClockwise()
	{
		if(activePlayer == Player.ONE)
		{
			players[0].GetComponent<RotateAround>().RotateClockwise();
		}
		else
		{
			players[1].GetComponent<RotateAround>().RotateClockwise();
		}
	}

	public void RotatePlayerCounterClockwise()
	{
		if(activePlayer == Player.ONE)
		{
			players[0].GetComponent<RotateAround>().RotateCounterClockwise();
		}
		else
		{
			players[1].GetComponent<RotateAround>().RotateCounterClockwise();
		}
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Tab))
		{
			ChangePlayer();
		}

		if(Input.GetKeyDown(KeyCode.Return))
		{
			SetPlayersToDatabase();
		}
	}
}
