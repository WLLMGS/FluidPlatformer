using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBladeScript : MonoBehaviour {
    void Update () {
        RotateBlade();
	}

    void RotateBlade()
    {
        transform.Rotate(new Vector3(0, 0, 360.0f * Time.deltaTime));
    }
}
