using UnityEngine;

public class SelectedCounter : MonoBehaviour
{

    [SerializeField] private BaseCounter BaseCounter;
    [SerializeField] private GameObject[] selectionVisuals;

    void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(BaseCounter obj)
    {
        if (obj != BaseCounter)
        {
            foreach (var visual in selectionVisuals)
            {
                visual.SetActive(false);
            }
        }
        else
        {
            foreach (var visual in selectionVisuals)
            {
                visual.SetActive(true);
            }
        }

    }
}
