using UnityEngine;

public class CutingCounterVisual : MonoBehaviour
{
    private const string CUT_ANIMATION_TRIGGER = "Cut";
    private Animator animator;
    [SerializeField] private CuttingCounter cuttingCounter;
    void Awake()
    {
        animator = GetComponent<Animator>();

    }
    void Start()
    {
        cuttingCounter.OnCut += CuttingCounter_OnPlayerGrabbedObject;
    }

    private void CuttingCounter_OnPlayerGrabbedObject(CuttingCounter counter)
    {
        if (cuttingCounter == counter)
        {
            animator.SetTrigger(CUT_ANIMATION_TRIGGER);
        }
    }
}
