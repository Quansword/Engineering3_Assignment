using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown : MonoBehaviour, ICooldown {

    [SerializeField]
	private float duration = 5f;
    public float Duration { get { return duration; } set { duration = value; } }

    [SerializeField]
	private float remaining = 0f;
	public float Remaining { get { return remaining; } }
	public float RemainingNormalized { get { return Mathf.Clamp01(remaining / duration); } }

    public bool Ready { get { return remaining <= 0f; } }

    public virtual void Activate() {
        remaining = duration;
    }

    public virtual void Reset() {
        remaining = 0f;
    }

    void Update() {
		remaining -= Mathf.Min(Time.deltaTime, remaining);
	}

}
