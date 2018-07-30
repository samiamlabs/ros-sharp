using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkliftController : MonoBehaviour
{

    public Rigidbody baseRigidbody;

    public Transform driveWheelTransform;
    public Transform steerTransform;

    public float driveWheelRadius = 0.033f;
    public float wheelBase = 0.2537f;

    public float driveWheelAngularVelocity;
    public float steerAngle;
    public float forkPosition;

    float m_Smooth = 50.0f;

    Vector3 m_Velocity;
    Vector3 m_EulerAngularVelocity;

    float m_DriveWheelAngle = 0.0f;
    Vector3 m_DriveWheelLocalPosition;
    Vector3 m_SteerLocalPosition;
    Vector3 m_ForkLocalPosition;

    void Start()
    {
        Application.runInBackground = true;
        m_DriveWheelLocalPosition = driveWheelTransform.localPosition;
        m_SteerLocalPosition = steerTransform.localPosition;
    }

    void Update()
    {
        m_DriveWheelAngle += Mathf.Rad2Deg * driveWheelAngularVelocity * Time.deltaTime;

        float steerAngleDegrees = Mathf.Rad2Deg * steerAngle;

        Quaternion target = Quaternion.Euler(m_DriveWheelAngle, steerAngleDegrees, 90);
        driveWheelTransform.localPosition = m_DriveWheelLocalPosition;
        driveWheelTransform.localRotation = Quaternion.Slerp(driveWheelTransform.localRotation, target, Time.deltaTime * m_Smooth);

        target = Quaternion.Euler(0, steerAngleDegrees, 0);
        steerTransform.localPosition = m_SteerLocalPosition;
        steerTransform.localRotation = Quaternion.Slerp(steerTransform.localRotation, target, Time.deltaTime * m_Smooth);
    }

    void UpdateMovement()
    {
        float forwardVelocity = driveWheelAngularVelocity * Mathf.Cos(steerAngle) * driveWheelRadius;
        m_Velocity = transform.forward * forwardVelocity;

        float angularVelocityY = (driveWheelAngularVelocity * driveWheelRadius * Mathf.Sin(steerAngle)) / wheelBase;
        m_EulerAngularVelocity = new Vector3(0, Mathf.Rad2Deg * angularVelocityY, 0);
    }

    void FixedUpdate()
    {
        UpdateMovement();

        Vector3 deltaPosition = m_Velocity * Time.deltaTime;
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngularVelocity * Time.deltaTime);

        baseRigidbody.MovePosition(baseRigidbody.position + deltaPosition);
        baseRigidbody.MoveRotation(baseRigidbody.rotation * deltaRotation);

    }
}
