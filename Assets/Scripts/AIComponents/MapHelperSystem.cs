using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHelperSystem : MonoBehaviour
{
    [SerializeField] private List<Transform> _enemySearchPoint;
    private int iCount = 0;

    public Transform NextEnemySearchPoint()
    {
        var heading = _enemySearchPoint[iCount].transform.position - gameObject.transform.position;
        var distance = heading.magnitude;
        if (distance < 0.5)
        {            
            iCount++;
            if (iCount == _enemySearchPoint.Count)
            {
                iCount = 0;
            }
        }
        
        //Debug.Log($"DISTANCE : {distance}");
        return _enemySearchPoint[iCount];
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
