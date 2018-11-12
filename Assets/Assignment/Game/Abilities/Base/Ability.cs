using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Ability : MonoBehaviour {

    [SerializeField] private int id = 0;
    public int Id { get { return id; } }

    [SerializeField] private string abilityName = null;
    public string Name { get { return abilityName; } }

    [SerializeField] private Sprite icon = null;
    public Sprite Icon { get { return icon; } }

    public abstract void Cast();
    public abstract void StopCast();
    public abstract bool CanCast(out string message);

    public virtual void Reset() {}

}
