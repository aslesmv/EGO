using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VRPhysicalSnapSystem : MonoBehaviour
{
    [Header("VR Rig Setup")]
    public GameObject xrOrigin;

    [Header("Fade UI Elements")]
    public Image fadeImage;
    public float fadeDuration = 0.5f;

    [Header("Loop Settings")]
    public float loopDuration = 15f;
    private float timer;

    private Vector3 startingPosition;
    private Quaternion startingRotation;
    private bool isTransitioning = false;

    void Start()
    {
        if (xrOrigin == null) xrOrigin = gameObject;

        // Here to ensure that screen starts clear
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = 0f;
            fadeImage.color = c;
        }

        SaveStartingPosition();
    }

    void Update()
    {
        if (isTransitioning) return;

        timer += Time.deltaTime;

        if (timer >= loopDuration)
        {
            StartCoroutine(ExecuteLoopSequence());
        }
    }

    public void SaveStartingPosition()
    {
        startingPosition = xrOrigin.transform.position;
        startingRotation = xrOrigin.transform.rotation;
        timer = 0f;
    }

    private IEnumerator ExecuteLoopSequence()
    {
        isTransitioning = true;

        yield return StartCoroutine(Fade(0f, 1f));

        xrOrigin.transform.position = startingPosition;
        xrOrigin.transform.rotation = startingRotation;

        yield return new WaitForSeconds(0.1f);

        timer = 0f;

        yield return StartCoroutine(Fade(1f, 0f));

        isTransitioning = false;
    }

    private IEnumerator Fade(float startAlpha, float targetAlpha)
    {
        if (fadeImage == null) yield break;

        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = targetAlpha;
        fadeImage.color = color;
    }
}
