using UnityEngine;
using System.Collections;
using System.Net;
using System.IO;
using System.Text;
using System;

public static class DatabaseSaver {

	public static string SaveNodes(int node1, int node2) {
		try {
			// Create a request using a URL that can receive a post. 
			WebRequest request = WebRequest.Create
				("http://malenaklaus.de/tweetlovers/audioGameTweet.php");
			// Set the Method property of the request to POST.
			request.Method = "POST";
			// Create POST data and convert it to a byte array.
			string data = "nodes=" + node1.ToString() + "-" + node2.ToString();
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

				//responseFromServer.IndexOf ("<!--")

				return responseFromServer;
			}
		} catch (Exception e) {
			Debug.Log (e.ToString());
		}
		
		return "no server response was requested";
	}

	public static Vector2 GetNodes() { 
		try {
			// Create a request using a URL that can receive a post. 
			WebRequest request = WebRequest.Create
				("http://malenaklaus.de/tweetlovers/audioGameTweet.php");
			// Set the Method property of the request to POST.
			request.Method = "POST";
			// Create POST data and convert it to a byte array.
			string data = "getnodes=" + "yes";
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
			//if (true) {
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

			//}

			int node1 = 0, node2 = 0;
			int.TryParse (responseFromServer
			             .Substring (0, responseFromServer.IndexOf ("n")), out node1);
			int.TryParse (responseFromServer
			             .Substring (responseFromServer.IndexOf ("n") + 1), out node2);
			return new Vector2(node1, node2);
		} catch (Exception e) {
			Debug.Log (e.ToString());
		}
		return new Vector2(0,0);
	}
}
