using System.IO;
using UnityEngine;

namespace Scoreboard
{
    public class Scoreboard : MonoBehaviour
    {
        [SerializeField] private int maxScoreboardEntries = 5;
        [SerializeField] private Transform victoryPointsHolderTransform = null;
        [SerializeField] private GameObject scoreboardEntryObject = null;

        [Header("Test")]
        [SerializeField] private string testEntryName = "New Name";
        [SerializeField] private int testEntryScore = 0;

        private string SavePath => $"{Application.persistentDataPath}/victoryPoints.json";

        private void Start()
        {
            ScoreboardSaveData savedScores = GetSavedScores();

            UpdateUI(savedScores);

            SaveScores(savedScores);
        }

        [ContextMenu("Add Test Entry")]
        public void AddTestEntry()
        {
            AddEntry(new ScoreboardEntryData()
            {
                entryName = testEntryName,
                entryScore = testEntryScore
            });
        }

        public void AddEntry(ScoreboardEntryData scoreboardEntryData)
        {
            ScoreboardSaveData savedScores = GetSavedScores();

            bool scoreAdded = true;

            //No need for highscores for now

            /*bool scoreAdded = false;

            //Check if the score is high enough to be added.
            for (int i = 0; i < savedScores.victoryPoints.Count; i++)
            {
                if (testEntryScore > savedScores.victoryPoints[i].entryScore)
                {
                    savedScores.victoryPoints.Insert(i, scoreboardEntryData);
                    scoreAdded = true;
                    break;
                }
            }*/

            //Check if the score can be added to the end of the list.
            if (!scoreAdded && savedScores.victoryPoints.Count < maxScoreboardEntries)
            {
                savedScores.victoryPoints.Add(scoreboardEntryData);
            }

            //Remove any scores past the limit.
            if (savedScores.victoryPoints.Count > maxScoreboardEntries)
            {
                savedScores.victoryPoints.RemoveRange(maxScoreboardEntries, savedScores.victoryPoints.Count - maxScoreboardEntries);
            }

            UpdateUI(savedScores);

            SaveScores(savedScores);
        }

        private void UpdateUI(ScoreboardSaveData savedScores)
        {
            foreach (Transform child in victoryPointsHolderTransform)
            {
                Destroy(child.gameObject);
            }

            foreach (ScoreboardEntryData highscore in savedScores.victoryPoints)
            {
                Instantiate(scoreboardEntryObject, victoryPointsHolderTransform).GetComponent<ScoreboardEntryUI>().Initialise(highscore);
            }
        }

        private ScoreboardSaveData GetSavedScores()
        {
            if (!File.Exists(SavePath))
            {
                File.Create(SavePath).Dispose();
                return new ScoreboardSaveData();
            }

            using (StreamReader stream = new StreamReader(SavePath))
            {
                string json = stream.ReadToEnd();

                return JsonUtility.FromJson<ScoreboardSaveData>(json);
            }
        }

        private void SaveScores(ScoreboardSaveData scoreboardSaveData)
        {
            using (StreamWriter stream = new StreamWriter(SavePath))
            {
                string json = JsonUtility.ToJson(scoreboardSaveData, true);
                stream.Write(json);
            }
        }
    }
}
