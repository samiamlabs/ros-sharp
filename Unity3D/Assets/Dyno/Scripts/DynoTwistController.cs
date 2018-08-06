using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynoTwistController : MonoBehaviour {

    public Rigidbody baseRigidbody;

    private float linearVelocityX = 0.0f;
    private float linearVelocityY = 0.0f;
    private float angularVelocity = 0.0f;

	public void UpdateMovevent (float linX, float linY, float ang) {
        linearVelocityX = linX;
        linearVelocityY = linY;
        angularVelocity = ang;
	}

    void FixedUpdate()
    {
        Vector3 deltaPosition = (transform.forward * linearVelocityX - transform.right * linearVelocityY) * Time.fixedDeltaTime;
        baseRigidbody.MovePosition(this.baseRigidbody.position + deltaPosition);

        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0.0f, -angularVelocity * Mathf.Rad2Deg, 0.0f) * Time.fixedDeltaTime);
        this.baseRigidbody.MoveRotation(this.baseRigidbody.rotation * deltaRotation);
    }
}
