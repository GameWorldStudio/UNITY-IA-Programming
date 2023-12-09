using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKSolverTarget : MonoBehaviour
{

    [SerializeField] LayerMask terrainLayer = default;
    [SerializeField] Transform body = default;
    [SerializeField] IKSolverTarget otherFoot2 = default;

/*    [SerializeField] IKSolverTarget otherFoot4 = default;*/
    [SerializeField] float speed = default;
    [SerializeField] float stepDistance = default;
    [SerializeField] float stepLenght = default;
    [SerializeField] float stepHeight = default;
    [SerializeField] Vector3 footOffset = default;
    float footSpacing;
    Vector3 oldPosition, currentPosition, newPosition;
    Vector3 oldNormal, currentNormal, newNormal;
    public float lerp;

    private void Start()
    {
        footSpacing = transform.localPosition.x;
        Debug.Log(footSpacing.ToString());
        currentPosition = newPosition = oldPosition = transform.position;
        currentNormal = newNormal = oldNormal = transform.up;
        lerp = 0;
    }

    private void Update()
    {
        transform.position = currentPosition;
        transform.up = currentNormal;

        Ray ray = new Ray(body.position + (body.right * footSpacing), Vector3.down);

        Debug.DrawRay(body.position + (body.right * footSpacing) , Vector3.down, Color.red);
        if(Physics.Raycast(ray, out RaycastHit info,10, terrainLayer.value))
        {
            Debug.Log(gameObject.name + " Verifie");
            if (Vector3.Distance(newPosition, info.point) > stepDistance && !otherFoot2.IsMoving()/* && !otherFoot4.IsMoving()*/ && lerp >= 1)
            {
                lerp = 0;
                int direction = body.InverseTransformDirection(info.point).z > body.InverseTransformDirection(newPosition).z ? 1 : -1;
                newPosition = info.point + (body.forward * stepLenght * direction) + footOffset;
                newNormal = info.normal;
            }
        }

        if(lerp < 1)
        {
            Debug.Log(gameObject.name + " Bouge");
            Vector3 tempPosition = Vector3.Lerp(oldPosition, newPosition, lerp);
            tempPosition.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;
            currentPosition = tempPosition;
            currentNormal = Vector3.Lerp(oldNormal, newNormal, lerp);
            lerp += Time.deltaTime * speed;
        }
        else
        {
            oldPosition = newPosition;
            oldNormal = newNormal;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(newPosition, 0.1f);
    }

    public bool IsMoving()
    {
        return lerp < 1;
    }
}
