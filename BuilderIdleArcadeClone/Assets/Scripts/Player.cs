using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : WorkerBase
{
    [SerializeField] protected Rigidbody playerRb;
    void Start()
    {
        for (int i = 0; i < 30; i++)
        {
            var product = Instantiate(boxPrefab).transform;
            product.parent = productMainobjTransform;
            product.localPosition = new Vector3(0, i * .3f, 0);
            collectedProducts.Add(product);
        }
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
}
