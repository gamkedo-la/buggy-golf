﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Leaderboards))]
public class ScoreManager : MonoBehaviour {
    Leaderboards leaderboards;

    public int localScore = 0;


    void Awake()
    {
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

    public static ScoreManager Instance { get; private set; }
	// Use this for initialization
	void Start () {
        //
        // BEGIN Leaderboards Test
        //
        
        Debug.Log("Begin Testing Leaderboards!!!");        

        leaderboards = GetComponent<Leaderboards>();

        // Delete leaderboards file.
        leaderboards.DeleteAllLeaderboards();

        // Add scores to leaderboards.
        leaderboards.AddScore("Might Morphin Rocket Ranger", 10);
        leaderboards.AddScore("Cat Dog Octopus", 99);
        leaderboards.AddScore("See Sharp See Blunt", 99);
        leaderboards.AddScore("Biker Mice From Mars", 15);
        leaderboards.AddScore("Load Runner", 5123);

        /*
        Debug.Log(leaderboards.GetScoreByName("Biker Mice From Mars"));
        Debug.Log(leaderboards.GetScoreByName("Cat Dog Octopus"));
        Debug.Log(leaderboards.GetScoreByName("Does Not Exist!")); // player name doesn't exist

        Debug.Log(leaderboards.GetNameByRank(1) + " has a score of " + leaderboards.GetScoreByRank(1));
        Debug.Log(leaderboards.GetNameByRank(2) + " has a score of " + leaderboards.GetScoreByRank(2));
        Debug.Log(leaderboards.GetNameByRank(3) + " has a score of " + leaderboards.GetScoreByRank(3));
        Debug.Log(leaderboards.GetNameByRank(4) + " has a score of " + leaderboards.GetScoreByRank(4));
        Debug.Log(leaderboards.GetNameByRank(5) + " has a score of " + leaderboards.GetScoreByRank(5));

        // the following ranks don't exist
        Debug.Log(leaderboards.GetNameByRank(6) + " has a score of " + leaderboards.GetScoreByRank(6));
        Debug.Log(leaderboards.GetNameByRank(7) + " has a score of " + leaderboards.GetScoreByRank(7));
        Debug.Log(leaderboards.GetNameByRank(8) + " has a score of " + leaderboards.GetScoreByRank(8));
        Debug.Log(leaderboards.GetNameByRank(9) + " has a score of " + leaderboards.GetScoreByRank(9));
        Debug.Log(leaderboards.GetNameByRank(10) + " has a score of " + leaderboards.GetScoreByRank(10));
                
        Debug.Log("End Testing Leaderboards!!!");
        */
        
        //
        // END Leaderboards Test
        //
    }

    public void SetLocalScore(int setScore) {
        localScore = setScore;
    }

    public void AddLocalScore(int newScore) {
        localScore = (localScore + newScore);
    }

    public int GetLocalScore() {
        return localScore;
    }

}
