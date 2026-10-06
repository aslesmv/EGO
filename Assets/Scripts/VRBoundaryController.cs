using UnityEngine;
using TMPro;

public class VRBoundaryController : MonoBehaviour
{
    // UI Elements
    public GameObject dialogueCanvas;

    // VR Setup
    public Transform xrOrigin; // Assign the XR Origin/Main Camera here
    public Transform safeResetPoint; // A transform just outside the boundary

    private bool playerInside = false;

    void Update()
    {
        if (playerInside && dialogueCanvas.activeSelf)
        {
            // Making the dialogue canvas face player in VR
            Vector3 targetPosition = xrOrigin.position;
            targetPosition.y = dialogueCanvas.transform.position.y; // Keeping UI level
            dialogueCanvas.transform.LookAt(targetPosition);
            dialogueCanvas.transform.Rotate(0, 180, 0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.GetComponentInChildren<Camera>() != null || other.GetComponentInParent<CharacterController>() != null )
        {
            playerInside = false;
            dialogueCanvas.SetActive(false);
        }

        if (xrOrigin)
        {
            playerInside = true;
            TriggerRestriction();
        }
    }

    void TriggerRestriction()
    {
        // Showing the dialogue
        dialogueCanvas.SetActive(true);

        // Positioning the canvas in front of player view
        Vector3 spawnPos = xrOrigin.position + (xrOrigin.forward * 1.5f);
        spawnPos.y = xrOrigin.position.y; // Keeping it at eye level
        dialogueCanvas.transform.position = spawnPos;

        // Teleporting or pushing the origin prefab back to safety
        if (safeResetPoint != null)
        {
            // Temporarily disable character controller
            CharacterController cc = xrOrigin.GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;

            xrOrigin.position = safeResetPoint.position;

            if (cc != null) cc.enabled = true;
        }
    }
}
