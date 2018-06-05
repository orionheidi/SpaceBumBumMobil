using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player
{

	private int individualScore = 0;

	public string playerName = "";

	public Player ()
	{
       
	}

	public Player (int individualScore, string playerName)
	{
		this.individualScore = individualScore;
		this.playerName = PlayerPrefs.GetString ("playerName");
       

	}


	public int IndividualScore {
		get { return individualScore; }
		set { this.individualScore = value; }
	}


	public string PlayerName {
		get { return playerName; }
		set { this.playerName = value; }
	}

   
  

    
}
