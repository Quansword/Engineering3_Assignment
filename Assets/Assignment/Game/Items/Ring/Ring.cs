using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : Item
{
	[SerializeField]
	private string name = "Ring";
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
				unit.HealthRegenMod = 1;
				break;
			case 2:
				unit.HealthRegenMod = 3;
				break;
			case 3:
				unit.HealthRegenMod = 5;
				break;
			default:
				break;
		}
	}
}
