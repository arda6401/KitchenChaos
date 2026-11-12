using UnityEngine;

public class IStoveProggessBarUI : MonoBehaviour
{
    [SerializeField] private float blinkInterval = 0.25f;

    private StoveCounter stoveCounter;
    private ProgessBarUI progressBarUI;

    void Start()
    {
        stoveCounter = GetComponentInParent<StoveCounter>();
        progressBarUI = GetComponent<ProgessBarUI>();

        if (stoveCounter == null)
        {
            Debug.LogError("StoveCounter component not found in parent objects.", this);
            return;
        }

        if (progressBarUI == null)
        {
            Debug.LogError("ProgessBarUI component not found on this object.", this);
            return;
        }

        stoveCounter.OnProgressChanged += HasProgress_OnProgressChanged;
    }

    private void HasProgress_OnProgressChanged(float progressNormalized)
    {
        if (progressNormalized > 0.5f && stoveCounter.IsFried())
        {
            float interval = Mathf.Max(blinkInterval, 0.01f);
            bool showRed = Mathf.FloorToInt(Time.time / interval) % 2 == 0;
            Color color = showRed ? Color.red : new Color(1f, 0.5f, 0f);
            progressBarUI.SetProgressBarColor(color);
        }
        else
        {
            progressBarUI.ResetProgressBarColor();
        }
    }

    private void OnDestroy()
    {
        if (stoveCounter != null)
        {
            stoveCounter.OnProgressChanged -= HasProgress_OnProgressChanged;
        }
    }
}
