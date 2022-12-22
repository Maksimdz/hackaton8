using UnityEngine;

[CreateAssetMenu(fileName = "HeroData", menuName = "ScriptableObjects/HeroData", order = 1)]
public class HeroData : ScriptableObject
{
    public HeroMediator hero;
    public Sprite sprite;
    public float moveSpeed = 7f;
    public float jumpForce = 14f;
}