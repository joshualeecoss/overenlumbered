using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class to handle the spawning of trees on the play field
/// </summary>
public class SpawnTrees : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The prefab to spawn in the spawn area")]
    public GameObject treePrefab = null;
    [Tooltip("The tree spawn area")]
    public BoxCollider spawnArea = null;
    [Tooltip("Number of trees to spawn initially")]
    public int numTrees = 25;
    [Tooltip("The speed at which the initial trees spawn (in seconds)")]
    public float initialSpawnDelay = 0.5f;


    // radius around which trees cannot spawn near each other
    private float spawnCollisionCheckRadius = 0.5f;
    // size of region that contains tree spawn points
    private Vector2 regionSize;
    // number of times to look for a valid point before moving on
    private int rejectionSamples = 30;
    // bool to start the ongoing tree spawn (after the initial trees are instantiated)
    private bool ongoingSpawn = false;

    // List of spawn points on the play field
    private List<Vector3> points = new List<Vector3>();
    // List of generated spawn points in 2D space
    private List<Vector2> generatedPoints;
    // List of indexes of occupied spawn points
    private List<int> occupiedPoints = new List<int>();

    /// <summary>
    /// Description: 
    /// Standard Unity function that's called before start when this instance 
    /// is first loaded
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    private void Awake()
    {
        CalculateSpawnPoints();
    }

    /// <summary>
    /// Description:
    /// Standard Unity function called once before the first Update call
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    private void Start()
    {
        InitialTreeSpawner();
    }


    /// <summary>
    /// Description:
    /// Standard Unity function called once every frame
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    private void Update()
    {
        if (ongoingSpawn)
        {
            OngoingTreeSpawner();
        }
    }

    /// <summary>
    /// Description:
    /// Determines the size of the spawn area and instantiates the initial
    /// trees inside that area
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    private void InitialTreeSpawner()
    {
        spawnArea.enabled = false;
        // Instantiate given number of trees in random positions in the spawn area
        StartCoroutine(SpawnTree(initialSpawnDelay, numTrees));
    }

    /// <summary>
    /// Description:
    /// Spawns a tree using the current tree spawn rate from the GameManager
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    private void OngoingTreeSpawner()
    {
        StartCoroutine(SpawnTree(GameManager.treeSpawnRate));
        ongoingSpawn = false;
    }

    /// <summary>
    /// Description:
    /// Spawns count number of trees every delay amount of time. 
    /// If two trees will overlap, this function will not place it until it finds
    /// a space with no collision. 
    /// Input: float delay, int count
    /// Return: IEnumerator
    /// </summary>
    /// <param name="delay">amount of time (in seconds) between trees spawned</param>
    /// <param name="count">number of trees spawned in this coroutine</param>
    /// <returns>IEnumerator: Allows this to function as a coroutine</returns>
    private IEnumerator SpawnTree(float delay, int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(delay);
            // Gets a random spawn point from the list of generated spawn points
            int pointIndex = Random.Range(0, points.Count - 1);
            Vector3 point = new Vector3(points[pointIndex].x, points[pointIndex].y, points[pointIndex].z);
            // As long as this index is not occupied, instantiates a new tree at that position
            if (IsThisSpotEmpty(pointIndex))
            {
                occupiedPoints.Add(pointIndex);
                GameObject tree = Instantiate(treePrefab, point, Quaternion.identity);
                // set the treeNum to the point index
                tree.GetComponent<TreeBehaviour>().SetTreeNum(pointIndex);
                // each tree will be a parent of the TreeSpawnArea just to reduce clutter in the editor
                tree.transform.parent = transform;
                tree.transform.localPosition = point;
            }
            // try again next time
            else
            {
                i -= 1;
            }
        }
        ongoingSpawn = true;
    }

    /// <summary>
    /// Description:
    /// Calculates a list of points that do not overlap based on the PoissonDiscSampling algorithm.
    /// Converts the list of 2D points into 3D space in the play area.
    /// Input: none
    /// Return: void (no return)
    /// </summary>
    private void CalculateSpawnPoints()
    {
        regionSize = new Vector2(spawnArea.size.x, spawnArea.size.z);
        generatedPoints = PoissonDiscSampling.GeneratePoints(spawnCollisionCheckRadius, regionSize, rejectionSamples);
        for (int i = 0; i < generatedPoints.Count; i++)
        {
            points.Add(new Vector3(generatedPoints[i].x - (spawnArea.size.x / 2), 0, generatedPoints[i].y - (spawnArea.size.z / 2) + spawnArea.center.z));
        }
    }

    /// <summary>
    /// Description:
    /// Removes the int from the list of occupied points
    /// Input: int index
    /// Return: void (no return)
    /// </summary>
    /// <param name="index"></param>
    public void RemoveOccupiedPoint(int index)
    {
        occupiedPoints.Remove(index);
    }

    /// <summary>
    /// Description: 
    /// Checks to see if pointIndex already exists in the list of occupied points
    /// Input: int pointIndex
    /// return: bool
    /// </summary>
    /// <param name="pointIndex">int to check against the list of ints (occupiedPoints)</param>
    /// <returns>bool: if the int exists in the list or not</returns>
    public bool IsThisSpotEmpty(int pointIndex)
    {
        if (occupiedPoints.Count == 0)
        {
            return true;
        }
        else
        {
            foreach (int point in occupiedPoints)
            {
                if (point.Equals(pointIndex))
                {
                    return false;
                }
            }
        }
        return true;
    }
}
