using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit Database", menuName = "ScriptableObjects/Database/UnitDatabase")]
public class UnitDatabase : ScriptableObject
{
    [SerializeField] private UnitData[] _unitDatas;

    private Dictionary<UnitType, UnitData> _datasDict;

    private bool _isInit = false;

    private void OnEnable()
    {
        DictInit();
    }

    public UnitData GetData(UnitType type)
    {
        if (!_isInit || _datasDict == null)
        {
            Debug.LogError("Unit dictionary not initialized, check unit database, initializing again");
            DictInit();
        }

        if (_datasDict == null)
        {
            Debug.LogError("Failed to initialize unit dictionary");
            return null;
        }

        if (_datasDict.Count == 0)
        {
            Debug.LogWarning("Unit dictionary is empty");
        }

        if (_datasDict.TryGetValue(type, out var data))
        {
            return data;
        }
        else
        {
            Debug.LogWarning("Unit type SO dictionary failed to find requested unit type");
            return null;
        }
    }

    public void DictInit()
    {
        _isInit = false;

        if (_unitDatas == null || _unitDatas.Length == 0)
        {
            Debug.LogError("Unit datas in Unit database not assigned");
            return;
        }

        _datasDict = new Dictionary<UnitType, UnitData>();
        int duplicate = 0;

        foreach (UnitData data in _unitDatas)
        {
            if (!_datasDict.ContainsKey(data.Type))
            {
                _datasDict[data.Type] = data;
            }
            else
            {
                duplicate++;
            }
            
        }

        if (duplicate > 0)
        {
            Debug.LogWarning("In Unit dictionary duplicates were found: " + duplicate);
        }

        _isInit = true;
    }
}
