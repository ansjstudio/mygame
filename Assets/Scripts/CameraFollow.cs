using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [HideInInspector] // Ye line Inspector se ball ka slot khatam kar degi
    public Transform ball;
    private Vector3 offset;
    private bool isOffsetSet = false;
    //-0.2
    void LateUpdate()
    {
        // 1. Agar ball nahi mili, toh scene mein dhoondo
        if (ball == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                ball = player.transform;
            }
        }

        // 2. Agar ball mil gayi hai
        if (ball != null)
        {
            if (ball.position.y < -0.2) return;
            // Pehli dafa offset set karein
            if (!isOffsetSet)
            {
                offset = transform.position - ball.position;
                isOffsetSet = true;
            }

            transform.position = ball.position + offset;

            // Ball ko follow karein
        }
    }
}