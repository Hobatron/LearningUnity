using UnityEngine;

[CreateAssetMenu(menuName = "EnemyData", fileName = "Enemy ")]
public class EnemySO : ScriptableObject
{
    [SerializeField] public int points = 0;
    [SerializeField] public int health = 15;
}
