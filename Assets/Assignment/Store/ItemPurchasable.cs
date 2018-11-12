using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Purchasable", menuName = "Warlocks/Item Purchasable")]
public class ItemPurchasable : Purchasable
{
	[SerializeField] private Unit target = null;
	public override Unit Target
	{
		get { return target; }
		set
		{
			target = value;
			UpdateCurrentUpgrade();
		}
	}

	[SerializeField] private Game game = null;
	public override Game Game { get { return game; } set { game = value; } }

	[SerializeField] private UpgradeCell cellPrefab = null;
	public override PurchasableCell CellPrefab { get { return cellPrefab; } }

	[SerializeField] private Item itemPrefab = null;
	[SerializeField] private List<int> upgradeCosts = new List<int>();

	public int UpgradeCount { get { return upgradeCosts.Count; } }

	[SerializeField] private int currentUpgrade = 0;
	public int CurrentUpgrade { get { return currentUpgrade; } }

	public override string Name { get { return itemPrefab.Name; } }
	public override Sprite Icon { get { return itemPrefab.Icon; } }
	public override int Cost { get { return upgradeCosts[Mathf.Clamp(currentUpgrade, 0, UpgradeCount - 1)]; } }

	public override bool CanPurchase()
	{
		if (currentUpgrade > 0)
			return currentUpgrade < UpgradeCount;
		else
		{
			return target.Items.Count < target.MaxItems;
		}
	}

	public override void OnPurchased()
	{
		currentUpgrade++;

		// upgrade unit ability
		Item targetCurrentItem = target.Items.Find(a => a.name == itemPrefab.name);
		if (!targetCurrentItem)
		{
			// unit doesn't have this item yet, add it
			targetCurrentItem = Instantiate<Item>(itemPrefab, target.AbilitiesParent);
			target.Items.Add(targetCurrentItem);
		}
		targetCurrentItem.ItemLevel = currentUpgrade;

		target.Items[target.Items.Count - 1].OnPurchase(target);
	}

	private void UpdateCurrentUpgrade()
	{
		// update currentUpgrade to match what unit has
		Item targetCurrentItem = target.Items.Find(a => a.Name == Name);
		if (!targetCurrentItem)
		{
			currentUpgrade = 0;
			return;
		}

		Level level = targetCurrentItem.GetComponent<Level>();
		currentUpgrade = level.Current;
	}
}
