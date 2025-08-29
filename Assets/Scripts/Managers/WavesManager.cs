using System.Collections;
using UnityEngine;

public class WavesManager : Singleton<WavesManager>
{
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

    // Pause between waves
    [SerializeField] private float _pauseBetweenWaves = 5.0f;
    [SerializeField] private Wave[] _waves;

    private void Start()
    {
        StartCoroutine(SpawnWavesRoutine(_waves));
    }

    // Coroutine which broadcasts event for spawner to spawn units in each waves with timers and pauses
    private IEnumerator SpawnWavesRoutine(Wave[] waves)
    {
        foreach (Wave wave in waves)
        {
            yield return new WaitForSeconds(_pauseBetweenWaves);

            foreach (Wave.WaveUnit units in wave.GetWaveUnits())
            {
                yield return new WaitForSeconds(units.SpawnTimer);

                for (int i = 0; i < units.SpawnAmount; i++)
                {
                    // event for spawner with information about unit, position, pathtype and pathpointposition (from start so null and 0)
                    Messenger<UnitType, Vector2?, (PathType, int)>.Broadcast(GameEvents.SpawnUnit, units.Type, null, (units.PathType, 0));

                    yield return new WaitForSeconds(units.SpawnIntervalBetweenUnits);
                }
            }
        }

        Debug.Log("All Waves Finished"); // TODO Global event of all waves ended
    }

}
