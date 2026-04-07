using UnityEngine;

[System.Serializable]
public class EnemyGroup
{
    public GameObject enemy;
    public int eCount;
    public float rate;
    public float resistChance = 0f;
    public float delay;
}
