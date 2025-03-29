using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class WorkerBase : MonoBehaviour
{
    [SerializeField] protected Transform productMainobjTransform;
    [SerializeField] protected List<Transform> collectedProducts;
    public GameObject boxPrefab; // Kutunun prefab'ı
    [SerializeField] protected float rotationDamping = 10f; 
    [SerializeField] protected float positionDamping = 10f; 
    [SerializeField] protected bool isWalking = false;




    protected void CollectProduct()
    {
        
    }
    protected void RemoveProduct()
    {
        
    }

    protected void ProductCarryingSwing()
    {
        if (!isWalking)
        {
            
        
            for (int i = 1; i < collectedProducts.Count; i++)
            {
                Vector3 targetPos = new Vector3(collectedProducts[i - 1].localPosition.x, i*.3f, collectedProducts[i - 1].localPosition.z);
                collectedProducts[i].localPosition = Vector3.Lerp(collectedProducts[i].localPosition, targetPos, Time.deltaTime * positionDamping);
                collectedProducts[i].rotation = Quaternion.Slerp(collectedProducts[i].rotation, collectedProducts[i - 1].rotation, Time.deltaTime * rotationDamping);
            }
        }
        else
        {
            
            for (int i = 1; i < collectedProducts.Count; i++)
            {     

                Vector3 targetPos = new Vector3(collectedProducts[i - 1].GetChild(1).position.x, collectedProducts[i - 1].GetChild(1).position.y, collectedProducts[i - 1].GetChild(1).position.z);
                collectedProducts[i].position = Vector3.Lerp(collectedProducts[i].position, targetPos, Time.deltaTime * positionDamping);
                collectedProducts[i].rotation = Quaternion.Slerp(collectedProducts[i].rotation, collectedProducts[i - 1].GetChild(1).rotation, Time.deltaTime * rotationDamping);
            }
        }
    }
}
