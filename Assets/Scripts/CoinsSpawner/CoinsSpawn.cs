using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsSpawn : MonoBehaviour
{
    [SerializeField] private int _minCoinsCount = 3;
    [SerializeField] private int _maxCoinsCount = 5;
    [SerializeField] private GameObject _coinsPrefab;

    
    public int MinimunCount
    {
        get { return this._minCoinsCount; }
        set { this._minCoinsCount = value;  }
    }

    public int MaximumCount
    {
        get { return this._maxCoinsCount;  }
        set { this._maxCoinsCount = value; }
    }

    public GameObject Prefab
    {
        get { return this.Prefab; }
        set { this.Prefab = value; }
    }

    public void Spawn()
    {
        int count = Random.Range(this._minCoinsCount, this.MaximumCount);
        // Spawn them!
        for (int i = 0; i < count; ++i)
        {
            Instantiate(this.Prefab, this.transform.position, Quaternion.identity);
        }
    }
}
