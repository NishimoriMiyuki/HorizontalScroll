using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "MyScriptable/Create TitleData")]
public class TitleData : ScriptableObject
{
    public List<Title> TitleDataList = new();
}

[Serializable]
public class Title
{
    public int id;
    public int order;
    public string name_address;
    public int need_score;
}