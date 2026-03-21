using UnityEngine;
using UnityEngine.EventSystems;

public class TouchDebugger : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("checking");
            Debug.Log("Touch position: " + EventSystem.current.IsPointerOverGameObject());
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Touch hit: " + EventSystem.current.currentSelectedGameObject);
            }
        }
    }
}