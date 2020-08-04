using UnityEngine;

/* An Item that can be equipped to increase armor/damage. */

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Equipment")]
public class Equipment : Item {

	public EquipmentSlot equipSlot;		// What slot to equip it in
    public int damageModifier;
    public int armorModifier;
    public int maxHealthModifier;
    public int maxManaModifier;
    public int manaRegenModifier;
    public int dodgeChanceModifier;
    public int movementSpeedModifier;
    public int attackSpeedModifier;

    public int agilityModifier;
    public int intelligenceModifier;
    public int strengthModifier;
	public SkinnedMeshRenderer prefab;

	// Called when pressed in the inventory
	public override void Use ()
	{
		EquipmentManager.instance.Equip(this);	// Equip
		RemoveFromInventory();	// Remove from inventory
	}

}

public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet}