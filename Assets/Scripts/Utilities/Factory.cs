using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Factory
{
    // Dictionary with unit`s names and prefabs
    private Dictionary<string, GameObject> _unitDictionary = new Dictionary<string, GameObject>();

    /// <summary>
    /// Creates unit link to prefab
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public async UniTask<GameObject> CreateUnit(UnitType type)
    {
        // Converting type to label (label must be with same type name)
        string label = type.ToString();

        GameObject requestedUnit = null;

        // Ff unit exists in dict, assign unit
        if (_unitDictionary.TryGetValue(label, out GameObject unit))
        {
            requestedUnit =  unit;
        }
        else // Load unit prefab from addressables and assign unit, if fail to download from addressables, throw global exception
        {
            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(label);

            try
            {
                // Try to assign unit
                requestedUnit = await handle.ToUniTask();
                // Add unit to dictionary
                _unitDictionary[label] = requestedUnit;
            }
            catch (System.Exception exception)
            {
                Debug.LogError("Addressables failed to load unit by label with following exception: " + exception);
            }

        }

        // If unit not assigned throw logError
        if (requestedUnit == null)
        {
            Debug.LogError("Failed to load Unit prefab by label: " + label);
        }

        return requestedUnit;
    }
}
