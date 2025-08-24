using UnityEngine;

public class PathsManager : MonoBehaviour
{
    public static PathsManager Instance; // Remake with singleton class

    // Paths information
    [System.Serializable]
    private struct PathInfo
    {
        [SerializeField] private PathTypes _type;
        [SerializeField] private Path _path;

        public PathTypes Type => _type;
        public Path Path => _path;
    }

    [SerializeField] private PathInfo[] _pathsInfo;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    /// <summary>
    /// Return requested path
    /// </summary>
    /// <param name="pathType"></param>
    /// <returns></returns>
    public Path GetPath(PathTypes pathType)
    {
        Path requestedPath = null;

        // Trying to find a requested path
        foreach (PathInfo pathEntry in _pathsInfo)
        {
            if (pathEntry.Type == pathType)
            {
                requestedPath = pathEntry.Path;
                break;
            }
        }

        // if didnt find a path - logerror (later do with try - catch when we set path to default if failed to find requested)
        if (requestedPath == null)
        {
            Debug.LogError("Coudnt find a path for unity" + pathType);
        }

        return requestedPath;
    }


}
