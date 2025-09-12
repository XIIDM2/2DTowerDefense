using UnityEngine;

[CreateAssetMenu(fileName = "Waves", menuName = "ScriptableObjects/Waves")]
public class WavesData : ScriptableObject
{
    public float PauseBetweenWaves => _pauseBetweenWaves;

    // Pause between waves
    [SerializeField] private float _pauseBetweenWaves = 5.0f;

    [SerializeField] private Wave[] _waves;

    // Wave class with information about wave units
    [System.Serializable]
    public class Wave
    {
        [SerializeField] private WaveUnit[] _waveUnits;

        // Return`s array of wave`s units
        public WaveUnit[] GetWaveUnits()
        {
            return _waveUnits;
        }

        // Wave unit class with information about unit type, path type, units amount, spawn interval and spawn timer
        [System.Serializable]
        public class WaveUnit
        {
            public UnitType Type => _type;
            public PathType PathType => _pathType;
            public int SpawnAmount => _spawnAmount;
            public float SpawnTimer => _spawnTimer;
            public float SpawnIntervalBetweenUnits => _spawnIntervalBetweenUnits;

            [SerializeField] private float _spawnTimer;

            [SerializeField] private float _spawnIntervalBetweenUnits;

            [SerializeField] private UnitType _type;
            [SerializeField] private PathType _pathType;

            [SerializeField] private int _spawnAmount;
        }
    }

    public Wave[] GetWaves()
    {
        return _waves;
    }
}
