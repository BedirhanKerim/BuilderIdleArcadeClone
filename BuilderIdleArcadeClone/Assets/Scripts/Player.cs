using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : WorkerBase
{
    [SerializeField] protected Rigidbody playerRb;
    void Start()
    {
      /*  for (int i = 0; i < 1; i++)
        {
            var product = Instantiate(boxPrefab).transform;
            product.parent = productMainobjTransform;
            product.localPosition = new Vector3(0, i * .3f, 0);
            collectedProducts.Add(product);
        }*/
    }

    // Update is called once per frame
    void  Update()
    {
        ProductCarryingSwing();

        if (playerRb.velocity.magnitude>.1f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;

        }
    }

    public void TakeProduct(Transform productTransform)
    {
        CollectProduct();
        productTransform.parent = productMainobjTransform;
        //productTransform.localPosition = new Vector3(0, collectedProducts.Count-1 * .3f, 0);
        if (collectedProducts.Count==0)
        {
            productTransform.localPosition = Vector3.zero;
            productTransform.localRotation = Quaternion.identity;


        }
        collectedProducts.Add(productTransform);
        
    }
    public Transform GiveProduct()
    {
        if (collectedProducts.Count==0)
        {        return null;

        }
        var product=collectedProducts[collectedProducts.Count - 1];
        product.parent = null;
        collectedProducts.Remove(product);
        return product;
    }
    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();

        if (interactable != null)
        {
            interactable.Interact();
        }
    }
    private void OnTriggerExit(Collider other)
    {Debug.Log("exit");
        IInteractable interactable = other.GetComponent<IInteractable>();

        if (interactable != null)
        {
            interactable.InteractEnd();
        }
    }
}
