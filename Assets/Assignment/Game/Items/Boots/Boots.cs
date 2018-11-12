using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boots : Item
{
	[SerializeField]
	private string name = "Boots";
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
				unit.MoveSpeedMod = 3;
				unit.TurnSpeedMod = 60;
				break;
			case 2:
				unit.MoveSpeedMod = 6;
				unit.TurnSpeedMod = 120;
				break;
			case 3:
				unit.MoveSpeedMod = 9;
				unit.TurnSpeedMod = 180;
				break;
			default:
				break;
		}
	}
}
