/*
License: BSD
https://raw.githubusercontent.com/samiamlabs/dyno/master/LICENCE
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkHeightWriter : MonoBehaviour {

    public ForkliftController forkliftController;

    private float m_Initial_y;

	void Start () {
        m_Initial_y = transform.localPosition.y;
	}
	
	void Update () {

        Vector3 forkModifiedLocalPosition = transform.localPosition;
        forkModifiedLocalPosition.y = m_Initial_y + forkliftController.forkPosition;

        transform.localPosition = forkModifiedLocalPosition;
	}
}
