using UnityEngine;

// scene controller script which changed states of the game
public class SceneManager : Singleton<SceneManager>
{
    [SerializeField] private bool _allWavesFinished = false;
    [SerializeField] private int _currentGlobalUnitsAmount = 0;

    private void OnEnable()
    {
        // check amount of units
        Messenger<int>.AddListener(GameEvents.GlobalUnitsAmountChanged, OnGlobalUnitsAmountChanged);
        // checks when all waves finished 
        Messenger.AddListener(GameEvents.AllWavesFinished, OnAllWavesFinished);
        Messenger.AddListener(GameEvents.PlayerDead, Defeat);

    }

    private void OnDisable()
    {
        Messenger<int>.RemoveListener(GameEvents.GlobalUnitsAmountChanged, OnGlobalUnitsAmountChanged);
        Messenger.RemoveListener(GameEvents.AllWavesFinished, OnAllWavesFinished);
        Messenger.RemoveListener(GameEvents.PlayerDead, Defeat);
    }

    private void OnGlobalUnitsAmountChanged(int value)
    {
        _currentGlobalUnitsAmount += value;
        CheckVictory();
    }

    private void OnAllWavesFinished()
    {
        _allWavesFinished = true;
    }

    // if all units dead and all waves finished = victory
    private void CheckVictory()
    {
        if (_allWavesFinished && _currentGlobalUnitsAmount == 0)
        {
            //Messenger.Broadcast(GameEvents.Victory); // add when add gui
            testVictory = true;
        }
    }

    // if player`s health is 0 = defeat
    private void Defeat()
    {
        //Messenger.Broadcast(GameEvents.Defeat);
        testDefeat = true;
    }

    private bool testVictory = false;
    private bool testDefeat = false;


    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 540, 100, 50), "Kill All Units"))
        {
            var units = FindObjectsByType<Health>(FindObjectsSortMode.None);

            foreach (var unit in units)
            {
                unit.TakeDamage(100);
            }
        }

        if (testVictory)
        {
            GUI.Label(new Rect(960, 590, 200, 100), "Victory");
        }
        else if (testDefeat)
        {
            GUI.Label(new Rect(960, 590, 200, 100), "Defeat");
        }
    }


}
