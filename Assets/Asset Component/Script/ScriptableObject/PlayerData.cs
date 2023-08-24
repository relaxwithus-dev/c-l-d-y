using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Object/Player/New PlayerData", order = 0)]
public class PlayerData : ScriptableObject
{
    [field: SerializeField] public string PlayerName {get; private set;}
    [field: SerializeField] public int PlayerSpeed {get; private set;}
}