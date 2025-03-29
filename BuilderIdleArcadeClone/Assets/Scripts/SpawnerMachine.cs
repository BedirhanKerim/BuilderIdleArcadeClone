using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpawnerMachine : MachineBase
{
    protected override void SpawnProduct()
    {
        if (spawnProduct != null && spawnPoint != null&&spawnedProducts.Count<36)
        {
            Transform newProduct = Instantiate(spawnProduct, spawnPoint.position, spawnProduct.transform.rotation).transform;
            MoveToArea(newProduct);
        }
        void MoveToArea(Transform product)
        {
            int totalProductCount = spawnedProducts.Count;
            product.DOMove(spawnedProductsLocations[totalProductCount].position, .5f)
                .SetEase(Ease.Linear) // Hareketin doğrusal olmasını sağlar
                .OnComplete(() =>
                {
                    spawnedProducts.Add(product);
                    
                });
        }
    }
}
