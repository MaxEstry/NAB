using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class samopin : MonoBehaviour
{
    void Start()
    {

        FixedJoint2D _fjoint = gameObject.GetComponent<FixedJoint2D>();

        if (_fjoint != null)
        {
            Transform pin = gameObject.transform.Find("pin");
            
            if (pin != null && pin.gameObject.activeSelf)
            {
                int layerMask = 1 << 10;
                
                RaycastHit2D hit = Physics2D.Raycast(pin.position, Vector2.up, 0.1f, layerMask, -2f, 2f);
                /*Debug.DrawRay(new Vector3(pin.position.x, pin.position.y, 0), Vector3.forward, Color.green, 2f);*/
                if (hit.collider != null)
                {
                    _fjoint.connectedBody = hit.transform.GetComponent<Rigidbody2D>();
                    Debug.Log("Hit :: " + hit.transform);
                }
                else { Debug.Log("Did not Hit"); }

                
                _fjoint.anchor = pin.localPosition;
                _fjoint.connectedAnchor = pin.localPosition;
                _fjoint.enabled = true;
            }
        }
    }
}
