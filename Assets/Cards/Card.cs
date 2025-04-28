using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card", order = 1)]
public class Card : ScriptableObject
{
    public string cardName;
    public Sprite cardImage; // For displaying the card

    // Stat Modifiers (use floats for percentages or integers as needed)
    public float movementSpeedModifier;
    public float fireRateModifier;
    public float projectileSizeModifier;
    public float accuracyModifier;
    public float damageModifier;
    public float critChanceModifier;
    public float critMultiplierModifier;
    public float projectileSpeedModifier;
    public float rangeModifier;
    public int maxHealthModifier;
    public float armorModifier;
    public float evasionChanceModifier;

    // ... (Add other stat modifiers as needed)
}