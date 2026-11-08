using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
   
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject particlesOnGameObject;
    [SerializeField] private GameObject stoveOnGameObject;

   
    void Start()
    {
       stoveCounter.OnStateChanged += StoveCounter_OnPlayerGrabbedObject;
    }

    private void StoveCounter_OnPlayerGrabbedObject(StoveCounter counter, StoveCounter.State state)
    {
        if (stoveCounter == counter)
        {
            bool showFryingAnimation = state == StoveCounter.State.Frying || state == StoveCounter.State.Fried;

            if(showFryingAnimation)
            {
                particlesOnGameObject.gameObject.SetActive(true);
                stoveOnGameObject.gameObject.SetActive(true);
            }
            else
            {
                particlesOnGameObject.gameObject.SetActive(false);
                stoveOnGameObject.gameObject.SetActive(false);
            }
        }
    }
}
