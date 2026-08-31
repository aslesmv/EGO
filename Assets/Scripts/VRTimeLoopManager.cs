using System.Collections.Generic;
using UnityEngine;

public class VRTimeLoopManager : MonoBehaviour
{
    [System.Serializable]
    public struct VRFrameState
    {
        public float timeStamp;

        // Tracking the main elements of a VR player (movement, rotation, position)
        public Vector3 headPos;
        public Quaternion headRot;
        public Vector3 leftHandPos;
        public Quaternion leftHandRot;
        public Vector3 rightHandPos;
        public Quaternion rightHandRot;
    }

    [Header("VR Targets")]
    public Transform headTarget;
    public Transform leftHandTarget;
    public Transform rightHandTarget;

    [Header("Loop Settings")]
    public float loopDuration = 15f;

    private List<VRFrameState> recordedFrames = new List<VRFrameState>();
    private float currentTime;
    private bool isReplaying = false;
    private int replayIndex = 0;
    
    void Update()
    {
        if (!isReplaying)
        {
            currentTime += Time.deltaTime;

            // Recording the local positions - in relation to the XR origin
            recordedFrames.Add(new VRFrameState
            {
                timeStamp = currentTime,
                headPos = headTarget.localPosition,
                headRot = headTarget.localRotation,
                leftHandPos = leftHandTarget.localPosition,
                leftHandRot = leftHandTarget.localRotation,
                rightHandPos = rightHandTarget.localPosition,
                rightHandRot = rightHandTarget.localRotation,
            });

            if (currentTime >= loopDuration)
            {
                StartLoopReset();
            }
        }
        else
        {
            PlayBackFrames();
        }
    }

    void StartLoopReset()
    {
        isReplaying = true;
        replayIndex = 0;
        
        // Reminder; Ad in a fade screen to black using XR camera fade before snapping back positions
    }

    void PlayBackFrames()
    {
        // If resetting player back to start:
        if (recordedFrames.Count > 0)
        {
            // Snap the actual player back to the first frame recorded
            VRFrameState initialFrame = recordedFrames[0];

            // Move your root XR origin back to the start point
            transform.position = initialFrame.headPos;

            ResetManager();
        }
    }

    void ResetManager()
    {
        currentTime = 0f;
        recordedFrames.Clear();
        isReplaying = false;
    }
}
