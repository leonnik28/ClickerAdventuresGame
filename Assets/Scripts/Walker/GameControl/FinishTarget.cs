using System;
using UnityEngine;

public class FinishTarget : MonoBehaviour
{
    public static Action OnFinishTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterController controller))
        {
            OnFinishTrigger?.Invoke();
        }
    }
}
