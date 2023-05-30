using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    [SerializeField] private Transform enemy;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = enemy.position;
    }
}
