using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineFreeLook))]

public class CameraLook : MonoBehaviour
{

    [SerializeField] private CinemachineFreeLook _cinemachine;
    // Start is called before the first frame update

    [Range(0, 10)]

    [SerializeField] private float lookSpeed;

    // Update is called once per frame
    void Update()
    {
        Vector2 lookValue = InputManager.instance.PlayerInput.Character.Look.ReadValue<Vector2>();
        _cinemachine.m_XAxis.Value += lookValue.x * 200 * lookSpeed * Time.deltaTime;
        _cinemachine.m_YAxis.Value += lookValue.y * lookSpeed * Time.deltaTime;
    }
}
