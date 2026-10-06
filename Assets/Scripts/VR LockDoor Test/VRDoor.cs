using UnityEngine;

public class VRDoor : MonoBehaviour
{
    // Settings for the door
    public bool isLocked = true;

    // Animation components here
    // public Animator doorAnimator;
    // public string openAnimationTrigger = "Open";

    // Audio settings
    // public AudioSoource audioSource;
    // public AudioClip unlockSound;
    // public AudioClip lockedSound;

    // This will be called by VR socket
    public void TryOpenDoor()
    {
        if (isLocked)
        {
            //PlaySound(lockedSound);
            Debug.Log("The door is locked!");
            return;
        }

        //if (doorAnimator != null)
        //{
            //doorAnimator.SetTrigger(openAnimationTrigger);
            //PlaySound(unlockSound);
        //}
    }

    public void UnlockDoor()
    {
        isLocked = false;
        //PlaySound(unlockSound);
        Debug.Log("Door is unlocked!");
    }

    //private void PlaySound(AudioClip clip)
    //{
        //if (AudioSource != null && clip != null)
        //{
            //AudioSource.PlayOneShot(clip);
        //}
    //}
}
