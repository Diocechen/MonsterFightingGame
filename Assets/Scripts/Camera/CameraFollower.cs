using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//改用Cinemachine
public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);
    private Vector3 velocity = Vector3.zero; //它用於在函式內部保持當前的速度狀態，並且在每次呼叫函式時更新它
    private float smoothTime = 0.25f;

    [SerializeField] private Transform target;
    [SerializeField] private float limitR;
    [SerializeField] private float limitL;

    void Update()
    {
        if (target.position.x >= limitL && target.position.x <= limitR)
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            //用於平滑插值(Vector3插值)的函式。它可以用來在兩個Vector3之間進行平滑過渡，
            //使得一個向量逐漸轉變到另一個向量，並且可以控制轉變的速度。
        }
    }
}
