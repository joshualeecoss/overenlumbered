using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the spawning of the Employee objects
/// </summary>
public class SpawnEmployee : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The employee prefab")]
    public GameObject employeePrefab = null;
    [Tooltip("The employee spawn area")]
    public BoxCollider spawnArea = null;

    /// <summary>
    /// Description:
    /// Gets a random location within the spawn area and instantiates a new employee at that position
    /// </summary>
    public void SpawnNewEmployee()
    {
        float spawnX = Random.Range(-(spawnArea.size.x / 2), spawnArea.size.x / 2);
        Vector3 position = new Vector3(spawnX, 2, spawnArea.transform.position.z);
        GameObject employee = Instantiate(employeePrefab, position, Quaternion.identity);
        employee.transform.parent = transform;
    }
}
