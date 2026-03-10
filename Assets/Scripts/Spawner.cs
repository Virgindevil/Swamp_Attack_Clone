using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<> _waves;
}

public class Wave
{ 
    public GameObject Tamplate;
    public float Delay;
    public int NumberOfWaves;
}