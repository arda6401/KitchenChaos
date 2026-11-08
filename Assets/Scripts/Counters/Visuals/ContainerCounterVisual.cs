using System;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private Animator animator;
    private const string OPEN_ANIMATION_TRIGGER = "OpenClose";
    [SerializeField] private ContainerCounter containerCounter;
    void Awake()
    {
        animator = GetComponent<Animator>();

    }
    void Start()
    {
        containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
    }

    private void ContainerCounter_OnPlayerGrabbedObject(ContainerCounter counter)
    {
        if (containerCounter == counter)
        {
            animator.SetTrigger(OPEN_ANIMATION_TRIGGER);
        }
    }
}
