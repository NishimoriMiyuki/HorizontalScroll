using System.Collections.Generic;
using UnityEngine;

public class MasterData : MonoBehaviour
{
    [SerializeField]
    private StageData _stageData;
    public List<Stage> StageData => _stageData.StageDataList;
}
