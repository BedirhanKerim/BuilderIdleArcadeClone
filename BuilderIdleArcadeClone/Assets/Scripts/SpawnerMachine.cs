using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.Core;
using UnityEngine;

public class SpawnerMachine : MachineBase
{
    [SerializeField] private InteractableArea spawnedProductArea;
    [SerializeField] private bool IsPlayerTakeProduct = false;
    protected override void Start()
    {
        base.Start();
        spawnedProductArea.OnInteracted += SpawnedProductAreaInteracted;
        spawnedProductArea.OnInteractEnd += SpawnedProductAreaInteractEnd;
    }

 

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
    private IEnumerator TimerCoroutine()
    {
        while (true)
        {
            if (IsPlayerTakeProduct)
            {
                GiveProductToWorker();
            }
            yield return new WaitForSeconds(.2f);
        }
    }
    protected override void GiveProductToWorker()
    {
       
        if (spawnedProducts.Count > 0)
        {
            var lastProduct = spawnedProducts[spawnedProducts.Count - 1];
            // Burada lastProduct ile işlemlerinizi yapabilirsiniz.
            GameManager.Instance.player.TakeProduct(lastProduct);
            spawnedProducts.Remove(lastProduct);

        }
        else
        {
            Debug.LogWarning("Liste boş!");
        }
    }
    private void SpawnedProductAreaInteracted()
    { IsPlayerTakeProduct = true;
        StartCoroutine(TimerCoroutine());
        Debug.LogWarning("girdi!");

    }

    private void SpawnedProductAreaInteractEnd()
    {
        IsPlayerTakeProduct = false;  
        StopCoroutine(TimerCoroutine());
        Debug.LogWarning("cıktı!");


    }
}
