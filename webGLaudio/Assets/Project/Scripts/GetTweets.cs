using UnityEngine;
using System.Collections;
using System.Net;
using System.IO;
using System.Text;

public static class GetTweets {

	public static string GetAll() {
		return Get ("all");
	}
	public static string GetOne() {
		return Get ("one");
	}
	public static string Get(int amountOfTweets) {
		return Get (amountOfTweets.ToString ());
	}

	// howmany = "all" or "one"
	private static string Get(string howmany) {
		try {
			// Create a request using a URL that can receive a post. 
			WebRequest request = WebRequest.Create
				("http://malenaklaus.de/tweetlovers/audioGameTweet.php");
			// Set the Method property of the request to POST.
			request.Method = "POST";
			// Create POST data and convert it to a byte array.
			string data = "gettweets=" + howmany;
			byte[] byteArray = Encoding.UTF8.GetBytes(data);
			// Set the ContentType property of the WebRequest.
			request.ContentType = "application/x-www-form-urlencoded";
			// Set the ContentLength property of the WebRequest.
			request.ContentLength = byteArray.Length;
			// Get the request stream.
			System.IO.Stream dataStream = request.GetRequestStream();
			// Write the data to the request stream.
			dataStream.Write(byteArray, 0, byteArray.Length);
			// Close the Stream object.
			dataStream.Close();
			
			// optional response, not needed right now.
			if (true) {
				// Get the response.
				WebResponse response = request.GetResponse();
				// Display the status.
				//Console.WriteLine(((HttpWebResponse)response).StatusDescription);
				// Get the stream containing content returned by the server.
				dataStream = response.GetResponseStream();
				// Open the stream using a StreamReader for easy access.
				StreamReader reader = new StreamReader(dataStream);
				// Read the content.
				string responseFromServer = reader.ReadToEnd();
				// Display the content.
				//Console.WriteLine(responseFromServer);
				// Clean up the streams.
				reader.Close();
				dataStream.Close();
				response.Close();

				return responseFromServer;
			}

		} catch (System.Exception e ){
			Debug.Log (e.ToString());
		}
		return "no server response was requested";
	}

	public static string GetLastMove() {
		try {
			// Create a request using a URL that can receive a post. 
			WebRequest request = WebRequest.Create
				("http://malenaklaus.de/tweetlovers/audioGameTweet.php");
			// Set the Method property of the request to POST.
			request.Method = "POST";
			// Create POST data and convert it to a byte array.
			string data = "getLastMove=yes";
			byte[] byteArray = Encoding.UTF8.GetBytes(data);
			// Set the ContentType property of the WebRequest.
			request.ContentType = "application/x-www-form-urlencoded";
			// Set the ContentLength property of the WebRequest.
			request.ContentLength = byteArray.Length;
			// Get the request stream.
			System.IO.Stream dataStream = request.GetRequestStream();
			// Write the data to the request stream.
			dataStream.Write(byteArray, 0, byteArray.Length);
			// Close the Stream object.
			dataStream.Close();
			
			// optional response, not needed right now.
			if (true) {
				// Get the response.
				WebResponse response = request.GetResponse();
				// Display the status.
				//Console.WriteLine(((HttpWebResponse)response).StatusDescription);
				// Get the stream containing content returned by the server.
				dataStream = response.GetResponseStream();
				// Open the stream using a StreamReader for easy access.
				StreamReader reader = new StreamReader(dataStream);
				// Read the content.
				string responseFromServer = reader.ReadToEnd();
				// Display the content.
				//Console.WriteLine(responseFromServer);
				// Clean up the streams.
				reader.Close();
				dataStream.Close();
				response.Close();

				return responseFromServer;
			}
			
		} catch (System.Exception e ){
			Debug.Log (e.ToString());
		}
		return "hubahubahub";
	}

}
