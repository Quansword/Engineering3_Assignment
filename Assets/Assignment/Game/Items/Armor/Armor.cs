using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item
{
	[SerializeField]
	private string name = "Armor";
	public override string Name { get { return name; } }

	[SerializeField]
	private Sprite icon;
	public override Sprite Icon { get { return icon; } }

	[SerializeField]
	private int itemLevel = 1;
	public override int ItemLevel { get { return itemLevel; } set { itemLevel = value; } }

	public override void OnPurchase(Unit unit)
	{
		switch (itemLevel)
		{
			case 1:
				unit.NormDamageRedMod = 0.1f;
				unit.LavaDamageRedMod = 0;
				break;
			case 2:
				unit.NormDamageRedMod = 0.2f;
				unit.LavaDamageRedMod = 0.1f;
				break;
			case 3:
				unit.NormDamageRedMod = 0.2f;
				unit.LavaDamageRedMod = 0.2f;
				break;
			default:
				break;
		}
	}
}
