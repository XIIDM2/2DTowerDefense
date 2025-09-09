using UnityEngine;

public class PathsManager : MonoBehaviour, IPathController
{
    // Paths information
    [System.Serializable]
    private struct PathInfo
    {
        public PathType Type => _type;
        public Path Path => _path;

        [SerializeField] private PathType _type;
        [SerializeField] private Path _path;
    }

    [SerializeField] private PathInfo[] _pathsInfo;

    /// <summary>
    /// Return requested path
    /// </summary>
    /// <param name="pathType"></param>
    /// <returns></returns>
    public Path GetPath(PathType pathType)
    {
        if (_pathsInfo == null || _pathsInfo.Length == 0)
        {
            Debug.LogError("Failed to find any path to work with");
            return null;
        }

        Path requestedPath = null;
        // First path to backup if requestedPath was not found
        Path firstFoundPath = null;

        // Trying to find a requested path
        foreach (PathInfo pathEntry in _pathsInfo)
        {
            if (firstFoundPath == null)
            {
                firstFoundPath = pathEntry.Path;
            }

            if (pathEntry.Type == pathType)
            {
                requestedPath = pathEntry.Path;
                break;
            }
        }

        // if did not find a path - logWarning and setting path to first found one
        if (requestedPath == null)
        {
            Debug.LogWarning("Could not find a path for unit, setting first found path" + pathType);
            requestedPath = firstFoundPath;
        }

        return requestedPath;
    }
}
