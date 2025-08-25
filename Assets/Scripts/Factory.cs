using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Factory
{
    // Dictionary with unit`s names and prefabs
    private Dictionary<string, GameObject> _unitDictionary = new Dictionary<string, GameObject>();

    // creating unit link to prefab
    public async UniTask<GameObject> CreateUnit(UnitType type)
    {
        // converting type to label (label must be with same type name)
        string label = type.ToString();

        GameObject requestedUnit = null;

        // if unit exists in dict, assign unit
        if (_unitDictionary.TryGetValue(label, out GameObject unit))
        {
            requestedUnit =  unit;
        }
        else // load unit prefab from addressables and assign unit, if fail to download from addressables, throw global exception
        {
            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(label);

            try
            {
                // try to assign unit
                requestedUnit = await handle.ToUniTask();
                //add unit to dictionary
                _unitDictionary[label] = requestedUnit;
            }
            catch (System.Exception exception)
            {
                Debug.LogError("Addressables failed to load unit by label with following exception: " + exception);
            }

        }

        // if unit not assigned throw logError
        if (requestedUnit == null)
        {
            Debug.LogError("Failed to load Unit prefab by label: " + label);
        }

        return requestedUnit;
    }
}
