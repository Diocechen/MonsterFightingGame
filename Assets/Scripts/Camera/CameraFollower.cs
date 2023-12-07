using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���Cinemachine
public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);
    private Vector3 velocity = Vector3.zero; //���Ω�b�禡�����O����e���t�ת��A�A�åB�b�C���I�s�禡�ɧ�s��
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
            //�Ω󥭷ƴ���(Vector3����)���禡�C���i�H�ΨӦb���Vector3�����i�業�ƹL��A
            //�ϱo�@�ӦV�q�v�����ܨ�t�@�ӦV�q�A�åB�i�H�������ܪ��t�סC
        }
    }
}
