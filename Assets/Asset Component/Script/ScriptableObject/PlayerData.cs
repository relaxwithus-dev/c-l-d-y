using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Object/Player/New PlayerData", order = 0)]
public class PlayerData : ScriptableObject
{
    public string playerName;
    public int playerSpeed;
}