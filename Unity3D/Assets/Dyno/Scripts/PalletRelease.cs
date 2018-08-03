using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalletRelease : MonoBehaviour {

    public GameObject m_SupportingObject;

    public GameObject m_CollidingObject;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
             SetCollidingObject(other);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        //FIXME: handle clipping problem when forks moving to fast
    }

    private void SetCollidingObject(Collider col)
    {
        m_CollidingObject = col.gameObject;
        Vector3 pallet_position = transform.position;
        Quaternion pallet_rotation = transform.rotation;

        m_SupportingObject = m_CollidingObject;

        transform.parent = m_CollidingObject.transform;

        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }

    public void OnTriggerExit(Collider other)
    {
        if (!m_CollidingObject)
        {
            return;
        }

        m_CollidingObject = null;
    }

    private void Update()
    {
        if (m_SupportingObject != null && transform.parent != m_SupportingObject.transform)
        {
            m_SupportingObject = null;
        }
    }

}
