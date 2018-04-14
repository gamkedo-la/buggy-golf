using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Leaderboards : MonoBehaviour {
    [SerializeField] string leaderboardsFileName = "buggy-golf-leaderboards";

    string currentSceneName="";

    Dictionary<string, int> scores = new Dictionary<string, int>();
    Dictionary<string, Dictionary<string, int>> courseScores = new Dictionary<string, Dictionary<string, int>>();

    void Awake() {
        currentSceneName = SceneManager.GetActiveScene().name;
        LoadAllScores();                      
    }

    /// <summary>
    /// Adds a score to the leaderboard of the current scene and saves it to the leaderboards file.
    /// </summary>
    /// <param name="playerName">The player's name.</param>
    /// <param name="playerScore">The player's score.</param>
    public void AddScore(string playerName, int playerScore) {
        LoadAllScores();

        // Keeps track of scores in the current scene's leaderboard.
        if (scores.ContainsKey(playerName)) {
            if (playerScore > scores[playerName]) {
                scores[playerName] = playerScore;
            }
        }
        else {
            scores.Add(playerName, playerScore);
        }

        // Sorts the scores in the current scene's leaderboard in descending order.
        scores = scores.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

        // Keeps track of the current scene's leaderboard.
        if (courseScores.ContainsKey(currentSceneName)) {
            courseScores[currentSceneName] = scores;
        }
        else {
            courseScores.Add(currentSceneName, scores);
        }

        // Saves leaderboards to a file.
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Create(Application.persistentDataPath + "/" + leaderboardsFileName);
        binaryFormatter.Serialize(fileStream, courseScores);
        fileStream.Close();
    }
    
    /// <summary>
    /// Loads and deserializes all scores across all leaderboards.
    /// </summary>
    void LoadAllScores() {
        if (File.Exists(Application.persistentDataPath + "/" + leaderboardsFileName)) {            
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(Application.persistentDataPath + "/" + leaderboardsFileName, FileMode.Open);

            // Deserializes leaderboards from a file.
            courseScores = (Dictionary<string, Dictionary<string, int>>)binaryFormatter.Deserialize(fileStream);

            // Load scores on the current scene's leaderboard.
            if (courseScores.ContainsKey(currentSceneName)) {
                scores = courseScores[currentSceneName];
            }

            fileStream.Close();
        }
    }    

    /// <summary>
    /// Deletes the leaderboards file. This clears all leaderboards across all scenes.
    /// </summary>
    public void DeleteLeaderboards() {
        File.Delete(Application.persistentDataPath + "/" + leaderboardsFileName);
    }

    /// <summary>
    /// Get player's score by the player's name.
    /// </summary>
    /// <param name="playerName">The player's name.</param>
    /// <returns></returns>
    public int GetScoreByName(string playerName) {
        Dictionary<string, int> scrs = courseScores[currentSceneName];

        if (scrs.ContainsKey(playerName)) {
            return scrs[playerName];
        }
        else {
            return -1;
        }
    }

    /// <summary>
    /// Get player's score by the player's rank.
    /// </summary>
    /// <param name="rank">The player's rank.</param>
    /// <returns></returns>
    public int GetScoreByRank(int rank) {
        if (scores.Count > 0) rank -= 1;

        if (rank < scores.Count) {
            return scores.ElementAt(rank).Value;
        }
        else {
            return -1;
        }
    }

    /// <summary>
    /// Get player's name  by the player's rank.
    /// </summary>
    /// <param name="rank">The player's rank.</param>
    /// <returns></returns>
    public string GetNameByRank(int rank) {
        if (scores.Count > 0) rank -= 1;

        if (rank < scores.Count) {
            return scores.ElementAt(rank).Key;
        }
        else {
            return "THIS PLAYER DOES NOT EXIST!!!";
        }
    }
}
