using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour
{
	public enum Direction
	{
		NORTH,
		SOUTH,
		WEST,
		EAST
	}

	public AudioClip ambientClip;

	public static List<Node> nodes = new List<Node>();
	public static List<int> ids = new List<int>();

	public int id;

	public Node northNode;
	public Node southNode;
	public Node westNode;
	public Node eastNode;

	public float radius = 10.0f;

	void Awake ()
	{
		nodes.Add (this);
	}

	public static Node GetNodeById(int id)
	{
		//Debug.Log ("Trying to find id " + id);

		for(int i = 0; i < nodes.Count; ++i)
		{
			if(nodes[i].id == id)
			{
				return nodes[i];
			}
		}

		Debug.LogError ("Couldn't find Node with id " + id);
		return null;
	}

	public void DrawDebugs()
	{
		DebugDraw (northNode, Color.blue);
		DebugDraw (southNode, Color.green);
		DebugDraw (westNode, Color.yellow);
		DebugDraw (eastNode, Color.magenta);

		Debug.DrawLine (transform.position, transform.position + transform.forward * radius, Color.cyan);
	}

	void DebugDraw(Node node, Color color)
	{
		if(node != null)
		{
			Debug.DrawLine(transform.position, node.transform.position, color);
		}
	}

	public int GenerateId()
	{
		bool generated = false;

		while(!generated)
		{
			int newId = Random.Range(0, 999999);

			if(!ids.Contains(newId))
			{
				ids.Add(newId);
				id = newId;
				generated = true;
			}
		}

		return id;
	}

	public void AutoConnectNearest()
	{
		Collider[] hits = Physics.OverlapSphere (transform.position, radius);

		for(int i = 0; i < hits.Length; ++i)
		{
			if(hits[i].gameObject == gameObject)
			{
				continue;
			}

			Vector3 angleVector = hits[i].transform.position - transform.position;
			float angle = Vector3.Angle(Vector2.right, new Vector2(angleVector.x, angleVector.z));

			if(transform.position.x < hits[i].transform.position.x)
			{
				if(angle <= 45.0f)
				{
					northNode = hits[i].GetComponent<Node>();
				}
				else if (angle > 45.0f && angle <= 135.0f)
				{
					eastNode = hits[i].GetComponent<Node>();
				}
				else
				{
					southNode = hits[i].GetComponent<Node>();
				}

				Debug.Log("Left side angle: " + angle);

			}
			else
			{
				if(angle <= 45.0f)
				{
					northNode = hits[i].GetComponent<Node>();
				}
				else if (angle > 45.0f && angle <= 135.0f)
				{
					westNode = hits[i].GetComponent<Node>();
				}
				else
				{
					southNode = hits[i].GetComponent<Node>();
				}

				Debug.Log("Right side angle: " + angle);
			}

		}
	}

	public void UpdateConnected()
	{
		UpdateNodeConnection (northNode, Direction.NORTH);
		UpdateNodeConnection (southNode, Direction.SOUTH);
		UpdateNodeConnection (westNode, Direction.WEST);
		UpdateNodeConnection (eastNode, Direction.EAST);
	}

	void UpdateNodeConnection(Node node, Direction dir)
	{
		if(node != null)
		{
			switch(dir)
			{
			case Direction.NORTH:
				node.southNode = this;
				break;

			case Direction.SOUTH:
				node.northNode = this;
				break;

			case Direction.EAST:
				node.westNode = this;
				break;

			case Direction.WEST:
				node.eastNode = this;
				break;
			}
		}
	}

	public void ClearConnected()
	{
		northNode = null;
		southNode = null;
		eastNode = null;
		westNode = null;
	}
}
