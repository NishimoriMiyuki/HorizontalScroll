using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "MyScriptable/Create StageData")]
public class StageData : ScriptableObject
{
    public List<Stage> StageDataList = new();
}

[Serializable]
public class Stage
{
    public int id;
    public float max_time;
    public int required_number_scratches;
    public string thing_address;
    public string bg_address;
    public string bgm_address;
    public int owner_group_id;
}