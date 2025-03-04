using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coin;
    [SerializeField] private Transform[] _spawnpoints;

    private int _indexPosition = 0;

    private void Start()
    {
        for (int i = 0; i < _spawnpoints.Length; i++)
            CreateCoin();
    }

    private void CreateCoin()
    {
        if (_indexPosition >= _spawnpoints.Length)
            _indexPosition = (_indexPosition + 1) % _spawnpoints.Length;

        Coin coin = Instantiate(_coin);

        coin.transform.position = _spawnpoints[_indexPosition].position;

        _indexPosition++;
    }
}
