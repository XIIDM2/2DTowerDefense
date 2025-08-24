using UnityEngine;

public class Path : MonoBehaviour
{
    // Array of pathpoints for a path;
    [SerializeField] private Transform[] _pathpoints;

    private void Start()
    {
        SetPathPoints();
    }

    /// <summary>
    /// Collects all direct child transforms as path points.
    /// </summary>
    private void SetPathPoints()
    {
        _pathpoints = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            _pathpoints[i] = transform.GetChild(i);
        }
    }

    /// <summary>
    /// Returns Array of pathpoints
    /// </summary>
    public Transform[] GetPathPoints()
    {   
        if (_pathpoints == null || _pathpoints.Length == 0)
        {
            Debug.LogError("Path is not set");
            return null;
        }

        return _pathpoints;
    }

    private void OnDrawGizmosSelected()
    {
        SetPathPoints();

        if (_pathpoints == null || _pathpoints.Length == 0) return;

        Gizmos.color = new Color(Random.Range(0, 1.0f), Random.Range(0, 1.0f), Random.Range(0, 1.0f), 0.8f);

        for (int i = 0; i < _pathpoints.Length; i++)
        {
            Gizmos.DrawSphere(_pathpoints[i].position, 0.5f);

            if (i < _pathpoints.Length - 1)
            {
                Gizmos.DrawLine(_pathpoints[i].position, _pathpoints[i + 1].position);
            }
        }
    }
}
