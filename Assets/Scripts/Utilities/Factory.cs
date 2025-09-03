using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Factory
{
    // Dictionary with unit`s names and prefabs
    private Dictionary<string, GameObject> _unitDictionary = new Dictionary<string, GameObject>();

    string label = string.Empty;

    /// <summary>
    /// Creates unit link to prefab
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public async UniTask<GameObject> CreateUnit(UnitType type)
    {
        // Converting type to label (label must be with same type name)
        label = type.ToString();
        GameObject requestedUnit = null;

        // If unit exists in dict, assign unit
        if (_unitDictionary.TryGetValue(label, out GameObject unit))
        {
            requestedUnit = unit;
        }
        // Load unit prefab from addressables and assign unit, if fail to download from addressables, throw global exception 
        else
        {
            try
            {
                requestedUnit = await Addressables.LoadAssetAsync<GameObject>(label).ToUniTask();
                _unitDictionary[label] = requestedUnit;
            }
            catch (System.Exception exception)
            {
                Debug.LogError("Failed to download unit with following exception: " + exception);
                return null;
            }
        }

        return requestedUnit;
    }
}