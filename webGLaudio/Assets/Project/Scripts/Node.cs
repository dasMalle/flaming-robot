using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour
{
	public static List<Node> nodes = new List<Node>();

	public int id;

	public Node northNode;
	public Node southNode;
	public Node westNode;
	public Node eastNode;

	void Awake ()
	{
		id = nodes.Count;
		nodes.Add (this);
	}

}
