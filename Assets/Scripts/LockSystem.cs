using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class LockSystem : MonoBehaviour
{
    private XRSocketInteractor socketInteractor;

    // Objects that will be affected
    public GameObject doorObject;

    void Awake()
    {
        // Automatically grabs the socket interactor component into object
        socketInteractor = GetComponent<XRSocketInteractor>();
    }

    void OnEnable()
    {
        // Listening for the selectEntered event
        socketInteractor.selectEntered.AddListener(OnKeyInserted);
    }

    void OnDisable()
    {
        // Cleaning up the listeners to prevent errors
        socketInteractor.selectEntered.RemoveListener(OnKeyInserted);
    }

    private void OnKeyInserted(SelectEnterEventArgs args)
    {
        // The code in here will fire when the key is socketed
        Debug.Log("Lock Unlocked");

        GameObject keyObject = args.interactableObject.transform.gameObject;
        keyObject.SetActive(false);

        if (doorObject != null)
        {
            doorObject.SetActive(false);
        }
    }
}
