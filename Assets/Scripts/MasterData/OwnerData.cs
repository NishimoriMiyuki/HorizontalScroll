using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "MyScriptable/Create OwnerData")]
public class OwnerData : ScriptableObject
{
    public List<Owner> OwnerDataList = new();
}

[Serializable]
public class Owner
{
    public int id;
    public int group_id;
    public int order;
    public float work_time;
    public float waiting_time;
    public float monitor_time;
}