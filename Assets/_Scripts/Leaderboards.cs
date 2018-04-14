using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Leaderboards : MonoBehaviour {
    [SerializeField] string leaderboardsFileName = "leaderboards";

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
    /// <param name="playerScore">The player's score</param>
    public void AddScore(string playerName, int playerScore) {
        LoadAllScores();

        if (scores.ContainsKey(playerName)) {
            if (playerScore > scores[playerName]) {
                scores[playerName] = playerScore;
            }
        }
        else {
            scores.Add(playerName, playerScore);
        }

        scores = scores.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

        if (courseScores.ContainsKey(currentSceneName)) {
            courseScores[currentSceneName] = scores;
        }
        else {
            courseScores.Add(currentSceneName, scores);
        }

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

            Dictionary<string, Dictionary<string, int>> courseScores = 
                    (Dictionary<string, Dictionary<string, int>>)binaryFormatter.Deserialize(fileStream);

            fileStream.Close();

            this.courseScores = courseScores;
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
