using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region variables for game management
    public GameData gameData;
    public GameObject levels;
    [Header("-1 for normal level initialization")]
    public int LevelNumber;
    #endregion

    private void OnEnable()
    {
        EventManager.getGameData += GetGameData;
    }

    private void OnDisable()
    {
        EventManager.getGameData -= GetGameData;
    }

    private void Start()
    {
        // load data
        SaveManager.LoadData(gameData);

        // manuel level start
        if (LevelNumber != -1)
        {
            gameData.Level = LevelNumber - 1;
            gameData.LevelText = LevelNumber - 1;
        }

        // open correct level
        levels.transform.GetChild(gameData.Level).gameObject.SetActive(true);
    }

    private void Update()
    {
        DeleteDatas();
    }

    public void Replay()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Next()
    {
        // set gamedata
        gameData.Level += 1;
        gameData.LevelText += 1;

        if (gameData.Level >= levels.transform.childCount)
        {
            gameData.Level = 0;
            gameData.LevelText = 0;
        }

        SaveManager.SaveData(gameData);
        SceneManager.LoadScene("GameScene");
    }

    private void DeleteDatas()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            gameData.Level = 0;
            gameData.LevelText = 0;

            Debug.Log("Playerprefs Deleted");
            PlayerPrefs.DeleteAll();
        }
    }

    private GameData GetGameData()
    {
        return gameData;
    }
}