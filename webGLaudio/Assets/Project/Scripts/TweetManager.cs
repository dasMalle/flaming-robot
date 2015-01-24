using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TweetManager : MonoBehaviour
{
	public Text[] tweets;

	List<string> lastTweets = new List<string>();
	string lastTweet;
	PlayerManager playerManager;
	float checkRate = 3.0f;

	void Start ()
	{
		playerManager = GetComponent<PlayerManager> ();
		StartCoroutine ("UpdateLatestTweets");
	}

	string ParseTweet(string text)
	{
		string parsedTweet = text;
		string[] splittedString = parsedTweet.Split(':');
		parsedTweet = splittedString[2].Substring(3);
		parsedTweet = parsedTweet.Replace("END", "");
		return parsedTweet;
	}

	IEnumerator UpdateLatestTweets()
	{
		while(true)
		{
			if(PlayerManager.isGameRunning)
			{
				string tweet = GetTweets.Get(6);

				Debug.Log(tweet);

				string[] rawTweets = tweet.Split('½');

				for(int i = 0; i < 6; ++i)
				{
					Debug.Log(rawTweets[i]);
					tweets[i].text = ParseTweet( rawTweets[i]);
				}

				lastTweet = GetTweets.GetLastMove();

				if(!lastTweet.Contains("NULL"))
				{
					if(lastTweet.Contains("North"))
					{
						if(lastTweet.Contains("P1"))
						{
							playerManager.MovePlayer(Node.Direction.NORTH, Player.ONE);
						}
						else if(lastTweet.Contains("P2"))
						{
							playerManager.MovePlayer(Node.Direction.NORTH, Player.TWO);
						}
					}
					else if(lastTweet.Contains("South"))
					{
						if(lastTweet.Contains("P1"))
						{
							playerManager.MovePlayer(Node.Direction.SOUTH, Player.ONE);
						}
						else if(lastTweet.Contains("P2"))
						{
							playerManager.MovePlayer(Node.Direction.SOUTH, Player.TWO);
						}
					}
					else if(lastTweet.Contains("East"))
					{
						if(lastTweet.Contains("P1"))
						{
							playerManager.MovePlayer(Node.Direction.EAST, Player.ONE);
						}
						else if(lastTweet.Contains("P2"))
						{
							playerManager.MovePlayer(Node.Direction.EAST, Player.TWO);
						}
					}
					else if(lastTweet.Contains("West"))
					{
						if(lastTweet.Contains("P1"))
						{
							playerManager.MovePlayer(Node.Direction.WEST, Player.ONE);
						}
						else if(lastTweet.Contains("P2"))
						{
							playerManager.MovePlayer(Node.Direction.WEST, Player.TWO);
						}
					}
				}
			}

			yield return new WaitForSeconds(checkRate);
		}
	}

}
