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

	IEnumerator UpdateLatestTweets()
	{
		while(true)
		{
			if(PlayerManager.isGameRunning)
			{
				string tweet = GetTweets.GetOne();

				if(lastTweet != tweet)
				{
					Debug.LogError("The most recent tweet: " + tweet);

					string parsedTweet = tweet;
					string[] splittedString = parsedTweet.Split(':');
					parsedTweet = splittedString[2].Substring(3);
					parsedTweet = parsedTweet.Replace("END", "");

					tweets[0].text = parsedTweet;

					lastTweet = tweet;
					
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
