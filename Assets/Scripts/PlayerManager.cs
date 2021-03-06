﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	// Saving this here for possible hotseat multiplayer
	// public Dictionary<int, int> playerHandicapDictionary = new Dictionary<int, int>();
	int currentPlayer = 1;
	public int testLevel = 0;
	public int playerHandicap = 0;
    public int localScoreHolder;


	// Use this for initialization
	void Awake () {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
	}

    public static PlayerManager Instance { get; private set; }

	// Update is called once per frame
	void Update () {

	}

	public void addPlayerHandicap(int handicapLevel) {
		// Saving this here for possible hotseat multiplayer
		// playerHandicapDictionary.Add (currentPlayer, handicapLevel);
		playerHandicap = handicapLevel;
		testLevel = handicapLevel; // Debug makings sure level set is coming through correctly
	}

	void resetPlayerNumberer() {
		currentPlayer = 1;
	}

    public void HoldScore(int score) {
        localScoreHolder = score;
    }
}
