using System.Collections;
using UnityEngine;
using VContainer;

public class WavesManager : MonoBehaviour, IWavesService
{
    [Inject] private WavesData _wavesData;

    private void Start()
    {
        StartCoroutine(SpawnWavesRoutine(_wavesData.GetWaves()));
    }

    // Coroutine which broadcasts event for spawner to spawn units in each waves with timers and pauses
    private IEnumerator SpawnWavesRoutine(WavesData.Wave[] waves)
    {
        foreach (WavesData.Wave wave in waves)
        {
            yield return new WaitForSeconds(_wavesData.PauseBetweenWaves);

            foreach (WavesData.Wave.WaveUnit units in wave.GetWaveUnits())
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

        Messenger.Broadcast(GameEvents.AllWavesFinished);
    }

}
