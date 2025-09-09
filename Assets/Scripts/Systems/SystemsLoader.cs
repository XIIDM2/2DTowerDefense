using UnityEngine;
using UnityEngine.AddressableAssets;

// load systems before any scene
public static class SystemsLoader
{
    //private const string SYSTEMS_LABEL = "Systems";

    //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    //private static void LoadSystemsPrefabAsync()
    //{
    //    // sync system prefab load (WaitForCompletion() blocks main flow untill operation is finished, guarantee us loaded systems at start)
    //    GameObject prefab = Addressables.LoadAssetAsync<GameObject>(SYSTEMS_LABEL).WaitForCompletion();

    //    if (prefab == null)
    //    {
    //        Debug.LogError("Failed to download systems prefab");
    //        return;
    //    }

    //    GameObject.DontDestroyOnLoad(GameObject.Instantiate(prefab));
    //}
}
