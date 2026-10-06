using UnityEngine;
using UnityEngine.Events;

public class LockandKey : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The tag assigned to key object ('key')")]
    public GameObject specificKeyObject;

    [Tooltip("The door asset that will be off when unlocked")]
    public GameObject doorAsset;

    [Header("Events")]
    public UnityEvent OnUnlocked;

    // This triggers key when entering door area
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == specificKeyObject)
        {
            UnlockDoor();
        }
    }

    private void UnlockDoor()
    {
        if (doorAsset != null)
        {
            doorAsset.SetActive(false);
        }

        if (specificKeyObject != null)
        {
            Destroy(specificKeyObject);
        }

        OnUnlocked?.Invoke();
    }
}
