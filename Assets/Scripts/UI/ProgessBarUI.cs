using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgessBarUI : MonoBehaviour
{

    [SerializeField] private GameObject ProgressGameObject;
    [SerializeField] private Image progressBarImage;
    private IHasProgress HasProgress;
    private Color defaultProgressBarColor;


    void Start()
    {
        HasProgress = GetComponentInParent<IHasProgress>();
        if (HasProgress == null)
        {
            Debug.LogError("IHasProgress component not found in parent objects.", this);
            
        }
        
        HasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
        defaultProgressBarColor = progressBarImage.color;
        progressBarImage.fillAmount = 0f;
        Hide();

    }

    private void HasProgress_OnProgressChanged(float progressNormalized)
    {
        progressBarImage.fillAmount = progressNormalized;
        if (progressNormalized > 0f && progressNormalized < 1f)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    public void SetProgressBarColor(Color color)
    {
        progressBarImage.color = color;
    }

    public void ResetProgressBarColor()
    {
        progressBarImage.color = defaultProgressBarColor;
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }

}
