using System.Collections.Generic;
using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    [SerializeField] private Apple _apple;
    [SerializeField] private Transform[] _spawnpoints;
    [Range(1, 5)][SerializeField] private int _amountSpawnedApples = 2;

    private List<int> _randomNumbers;

    private void Start()
    {
        _randomNumbers = new  List<int>();
        TakeRandomSpawnpoints();
        
        for (int i = 0; i < _amountSpawnedApples; i++)
            CreateCoin(i);
    }

    private void CreateCoin(int index)
    {
        Apple apple = Instantiate(_apple);

        apple.transform.position = _spawnpoints[_randomNumbers[index]].position;
    }

    private void TakeRandomSpawnpoints()
    {
        bool isRandom = false;

        for (int i = 0; i < _amountSpawnedApples; i++)
        {
            while (isRandom == false)
            {
                int randomNumber = Random.Range(0, _spawnpoints.Length);

                if (_randomNumbers.Contains(randomNumber) == false)
                {
                    _randomNumbers.Add(randomNumber);
                    isRandom = true;
                }
            }

            isRandom = false;
        }
    }
}
