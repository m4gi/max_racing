using UnityEngine;
using System.Collections;

public class UIAnimatedScale : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private float duration = 0.25f;
    [SerializeField] private Vector3 startScale = Vector3.zero;
    [SerializeField] private Vector3 endScale = Vector3.one;
    [SerializeField] private AnimationCurve ease = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Coroutine currentRoutine;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
            rectTransform = gameObject.AddComponent<RectTransform>();
    }

    private void OnEnable()
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(ScaleRoutine(startScale, endScale));
    }

    private void OnDisable()
    {
        rectTransform.localScale = startScale;
    }

    public void HideWithAnimation()
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(ScaleOutAndDisable());
    }

    private IEnumerator ScaleRoutine(Vector3 from, Vector3 to)
    {
        float time = 0f;
        rectTransform.localScale = from;

        while (time < duration)
        {
            time += Time.unscaledDeltaTime;
            float t = ease.Evaluate(time / duration);
            rectTransform.localScale = Vector3.LerpUnclamped(from, to, t);
            yield return null;
        }

        rectTransform.localScale = to;
    }

    private IEnumerator ScaleOutAndDisable()
    {
        yield return ScaleRoutine(endScale, startScale);
        gameObject.SetActive(false);
    }
}