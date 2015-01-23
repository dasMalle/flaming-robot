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
	public int targetNodeId;
	float movementSpeed = 1.0f;

	void Update ()
	{
		transform.position = Vector3.MoveTowards(transform.position, Node.nodes[targetNodeId].transform.position, Time.deltaTime * movementSpeed);
	}
}
