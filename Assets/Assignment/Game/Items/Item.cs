using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
	public abstract string Name { get; }
	public abstract Sprite Icon { get; }
	public abstract int ItemLevel { get; set; }

	public abstract void OnPurchase(Unit unit);
}
