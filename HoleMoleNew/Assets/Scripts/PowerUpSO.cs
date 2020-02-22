using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PowerUpSO", menuName = "ScriptableObjects/PowerUpSO")]
public class PowerUpSO : ScriptableObject
{
    public new string name;
    public Sprite spriteImg;
    public Texture2D cursorImg;
    public int weight;
    public int cost;
}
