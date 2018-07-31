/*
License: BSD
https://raw.githubusercontent.com/samiamlabs/dyno/master/LICENCE
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkGrabPallet : MonoBehaviour {

    private GameObject m_PalletOnForks;
    private Vector3 m_PalletLocalPosition;
    private Quaternion m_PalletLocalRotation;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 && !other.isTrigger)
        {
            GrabPallet(other.gameObject);
        }
    }

    private void GrabPallet(GameObject pallet)
    {

        Rigidbody palletRigidbody = pallet.GetComponent<Rigidbody>();

        pallet.transform.parent = transform;
        m_PalletLocalPosition = pallet.transform.localPosition;
        m_PalletLocalRotation = pallet.transform.localRotation;

        palletRigidbody.isKinematic = true;
        palletRigidbody.useGravity = false;

        m_PalletOnForks = pallet;

    }

    void Update()
    {

        if (m_PalletOnForks != null)
        {

            if (m_PalletOnForks.transform.parent != transform)
            {
                m_PalletOnForks = null;
                return;
            }

            m_PalletOnForks.transform.localPosition = m_PalletLocalPosition;
            m_PalletOnForks.transform.localRotation = m_PalletLocalRotation;
        }
    }


}
