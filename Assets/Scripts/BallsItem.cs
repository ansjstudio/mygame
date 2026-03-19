using UnityEngine;

[CreateAssetMenu(fileName = "New Ball", menuName = "Shop/Balls")]
public class BallsItem : ScriptableObject
{
    public string ballName;
    public Sprite ballSprite;
    public int price;
    public bool isPurchased;
    public int id;
}
