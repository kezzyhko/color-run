using Mechanics.Fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{

    [Header("Level Settings")]
    public float PlatformLength = 100.0f;
    public int EnemiesAmount = 5;

    [Header("Game Objects")]
    public GameObject Player;
    public GameObject Platform;
    public GameObject Finish;
    public GameObject FightTrigger;
    public GameObject EnemyCrowd;
    public GameObject EnemyPrefab;

    // platform constants
    private const float PlatformWidth = 5.0f;
    private const float PlatformHeight = 1.0f;
    private const float PaddingBeforePlayer = 5.0f;
    private const float PaddingAfterEnd = 10.0f;

    // enemies spawn constants
    private const float EnemyOffset = 0.6f;
    private const float RowOffset = 0.6f;
    private const int EnemiesInOneRow = 5;

    private void OnValidate()
    {
        EnemiesAmount = Mathf.Max(EnemiesAmount, 1);
        PlatformLength = Mathf.Max(PlatformLength, 0);
        UpdatePlatformLength();
    }

    private void Start()
    {
        CreateEnemies();
    }

    private void UpdatePlatformLength()
    {
        float fullPlatformLength = PlatformLength + PaddingBeforePlayer + PaddingAfterEnd;
        Platform.transform.localScale = new Vector3(PlatformWidth, PlatformHeight, fullPlatformLength);
        Platform.transform.position = new Vector3(0, -PlatformHeight / 2, fullPlatformLength / 2 - PaddingBeforePlayer);
        Finish.transform.position = new Vector3(0, 0, PlatformLength);
    }

    private void CreateEnemies()
    {
        int fullRowsAmount = EnemiesAmount / EnemiesInOneRow;
        for (int rowNumber = 0; rowNumber < fullRowsAmount; rowNumber++)
        {
            CreateEnemiesRow(EnemiesInOneRow, rowNumber);
        }
        CreateEnemiesRow(EnemiesAmount % EnemiesInOneRow, fullRowsAmount); // last not full row
    }

    private void CreateEnemiesRow(int amountOfEnemies, int rowNumber)
    {
        float firstEnemyPosition = -EnemyOffset * (amountOfEnemies - 1) / 2;
        for (int i = 0; i < amountOfEnemies; i++)
        {
            GameObject enemy = Instantiate(EnemyPrefab, EnemyCrowd.transform);
            enemy.name = EnemyPrefab.name;

            enemy.transform.localPosition = new Vector3(
                firstEnemyPosition + i * EnemyOffset,
                0,
                rowNumber * RowOffset
            );
        }
    }
}