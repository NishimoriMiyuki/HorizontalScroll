using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public List<PlayerStageData> stages = new();

    public PlayerStageData NextStage => stages.Last();

    public void AddNextStage()
    {
        var stageData = MainSystem.Instance.MasterData.StageData;
        var orderStageData = stageData.OrderBy(stage => stage.order).ToList();

        // 現在のDataとMasterDataの最後のDataが一致していたら何もしない
        if (NextStage.MasterStage == orderStageData.Last())
        {
            Debug.Log("最後が一致してた");
            return;
        }

        // 現在のDataのindex取得
        int index = stageData.IndexOf(orderStageData.FirstOrDefault(stage => stage == NextStage.MasterStage));

        stages.Add(new PlayerStageData { stage_id = orderStageData[index + 1].id });
        Debug.Log("Addした");
    }
}

[Serializable]
public class PlayerStageData
{
    public int stage_id;

    public Stage MasterStage => MainSystem.Instance.MasterData.StageData.FirstOrDefault(stage => stage.id == stage_id);
}

public class SaveDataManager
{
    private const string PLAYER_DATA_KEY = "PlayerSaveData";

    public void Save()
    {
        var json = JsonUtility.ToJson(MainSystem.Instance.PlayerData);
        PlayerPrefs.SetString(PLAYER_DATA_KEY, json);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (!PlayerPrefs.HasKey(PLAYER_DATA_KEY))
        {
            CreateInitData();
            Save();
        }

        var playerData = PlayerPrefs.GetString(PLAYER_DATA_KEY);
        MainSystem.Instance.PlayerData = JsonUtility.FromJson<PlayerData>(playerData);
    }

    /// <summary>
    /// プレイヤーの初期データ
    /// </summary>
    private void CreateInitData()
    {
        MainSystem.Instance.PlayerData.stages.Add(new PlayerStageData { stage_id = 1001001 });
    }
}