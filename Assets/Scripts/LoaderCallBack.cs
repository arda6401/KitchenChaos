using UnityEngine;

public class LoaderCallBack : MonoBehaviour
{
    private bool isCalled = false;

    // Update is called once per frame
    void Update()
    {
        if (!isCalled)
        {
            isCalled = true;
            Loader.CallBack();
        }
    }
}
