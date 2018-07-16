using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Leaderboards : MonoBehaviour {
    [SerializeField] string leaderboardsFileName = "buggy-golf.leaderboards";
    
    string currentSceneName = "";

    // Holds current scene's leaderboard scores.
    Dictionary<string, int> leaderboardScores = new Dictionary<string, int>();

    // Holds all leaderboards across all scenes with ScoreManager.
    Dictionary<string, Dictionary<string, int>> leaderboardsAcrossScenes = new Dictionary<string, Dictionary<string, int>>();


    void Awake() {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        currentSceneName = SceneManager.GetActiveScene().name;
        LoadLeaderboard();                      
    }

    public static Leaderboards Instance { get; private set; }

    /// <summary>
    /// Adds a score to the leaderboard of the current scene and saves it to the leaderboards file. 
    /// Score is added only if it didn't exist or is greater than the current score.
    /// </summary>
    /// <param name="playerName">The player's name.</param>
    /// <param name="playerScore">The player's score.</param>
    public void AddScore(string playerName, int playerScore) {
        LoadLeaderboard();

        // Keeps track of scores in the current scene's leaderboard.
        if (leaderboardScores.ContainsKey(playerName)) {
            if (playerScore > leaderboardScores[playerName]) {
                leaderboardScores[playerName] = playerScore;
            }
        }
        else {
            leaderboardScores.Add(playerName, playerScore);
        }

        SaveLeaderboard();
    }
    
    /// <summary>
    /// Loads and deserializes all scores across all leaderboards.
    /// </summary>
    void LoadLeaderboard() {
        if (File.Exists(Application.persistentDataPath + "/" + leaderboardsFileName)) {            
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(Application.persistentDataPath + "/" + leaderboardsFileName, FileMode.Open);

            // Deserializes leaderboards from a file.
            leaderboardsAcrossScenes = (Dictionary<string, Dictionary<string, int>>)binaryFormatter.Deserialize(fileStream);

            // Load scores on the current scene's leaderboard.
            if (leaderboardsAcrossScenes.ContainsKey(currentSceneName)) {
                leaderboardScores = leaderboardsAcrossScenes[currentSceneName];
            }

            fileStream.Close();
        }
    }    

    /// <summary>
    /// Sorts and saves current scene's leaderboard to file.
    /// </summary>
    void SaveLeaderboard() {
        // Sorts the scores in the current scene's leaderboard in descending order.
        leaderboardScores = leaderboardScores.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

        // Keeps track of the current scene's leaderboard.
        if (leaderboardsAcrossScenes.ContainsKey(currentSceneName)) {
            leaderboardsAcrossScenes[currentSceneName] = leaderboardScores;
        }
        else {
            leaderboardsAcrossScenes.Add(currentSceneName, leaderboardScores);
        }

        // Saves leaderboards to a file.
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Create(Application.persistentDataPath + "/" + leaderboardsFileName);
        binaryFormatter.Serialize(fileStream, leaderboardsAcrossScenes);
        fileStream.Close();
    }

    /// <summary>
    /// Deletes the leaderboards file. This clears all leaderboards across all scenes.
    /// </summary>
    public void DeleteAllLeaderboards() {
        leaderboardScores = new Dictionary<string, int>();
        leaderboardsAcrossScenes = new Dictionary<string, Dictionary<string, int>>();
        File.Delete(Application.persistentDataPath + "/" + leaderboardsFileName);
    }

    /// <summary>
    /// Get player's score by the player's name.
    /// </summary>
    /// <param name="playerName">The player's name.</param>
    /// <returns></returns>
    public int GetScoreByName(string playerName) {
        Dictionary<string, int> scrs;

        if (leaderboardsAcrossScenes.ContainsKey(currentSceneName)) {
            scrs = leaderboardsAcrossScenes[currentSceneName];
        }
        else {
            return -1;
        }

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
        if (leaderboardScores.Count > 0) rank -= 1;

        if (rank < leaderboardScores.Count) {
            return leaderboardScores.ElementAt(rank).Value;
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
        if (leaderboardScores.Count > 0) rank -= 1;

        if (rank < leaderboardScores.Count) {
            return leaderboardScores.ElementAt(rank).Key;
        }
        else {
            return "THIS PLAYER DOES NOT EXIST!!!";
        }
    }
}
