using UnityEngine;

public class RotateItem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Diamond ko Y-axis par ahista ahista ghumao
        transform.Rotate(new Vector3(0, 100 * Time.deltaTime, 0));
    }
}
