using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgessBarUI : MonoBehaviour
{

    [SerializeField] private GameObject ProgressGameObject;
    [SerializeField] private Image progressBarImage;
    private IHasProgress HasProgress;


    void Start()
    {
        HasProgress = GetComponentInParent<IHasProgress>();
        if (HasProgress == null)
        {
            Debug.LogError("IHasProgress component not found in parent objects.", this);
            
        }
        
        HasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
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

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }

}
