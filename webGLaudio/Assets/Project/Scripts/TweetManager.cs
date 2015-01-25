﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TweetManager : MonoBehaviour
{
	public Text[] tweets;
	string lastTweet;
	PlayerManager playerManager;
	float checkRate = 7.0f;
	public AudioSource roarSound;
	public AudioSource meteorSound;

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
			Debug.Log("Co2 start: " + Time.time);

			if(PlayerManager.isGameRunning && DatabaseSaver.GetNodes().x != -1)
			{
				string tweet = GetTweets.Get(6);

				string[] rawTweets = tweet.Split('½');

				for(int i = 0; i < 6; ++i)
				{
					tweets[i].text = ParseTweet( rawTweets[i]);
				}

				lastTweet = GetTweets.GetLastMove();

				if(!lastTweet.Contains("NULL"))
				{
					Debug.Log(lastTweet);

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
					else if(lastTweet.Contains("Roar"))
					{
						if(lastTweet.Contains("P1"))
						{
							roarSound.transform.position = playerManager.players[0].transform.position;
							roarSound.Play();
						}
						else if(lastTweet.Contains("P2"))
						{
							roarSound.transform.position = playerManager.players[1].transform.position;
							roarSound.Play();
						}
					}
					else if(lastTweet.Contains("Meteor"))
					{
						if(lastTweet.Contains("P1"))
						{
							meteorSound.Play();
						}
						else if(lastTweet.Contains("P2"))
						{
							meteorSound.Play();
						}
					}

				}
				else
				{
					Debug.LogWarning(lastTweet);
				}
			}

			yield return new WaitForSeconds(checkRate);
		}
	}

}
