using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCalculator : MonoBehaviour
{
    [SerializeField] GameObject Player1;
    [SerializeField] GameObject Player2;
    public float Distance;
  
    void Update()
    {
        Distance = Player1.transform.position.z - Player2.transform.position.z;
    }
}
