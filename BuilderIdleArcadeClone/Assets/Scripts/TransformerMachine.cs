using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.Core;
using UnityEngine;

public class TransformerMachine : MachineBase
{
    [SerializeField] private InteractableArea spawnedProductArea,collectProductArea;
    [SerializeField] private bool IsPlayerTakeProduct = false,IsPlayerGiveProduct = false;
    [SerializeField] private List<Transform> collectedProducts;
    [SerializeField] private Transform[] collectedProductsLocations;

    [SerializeField] private int totalCollectedProductCount = 0;
    protected override void Start()
    {
        base.Start();
        spawnedProductArea.OnInteracted += SpawnedProductAreaInteracted;
        spawnedProductArea.OnInteractEnd += SpawnedProductAreaInteractEnd;
        collectProductArea.OnInteracted += CollectProductAreaInteracted;
        collectProductArea.OnInteractEnd += CollectProductAreaInteractEnd;
    }
    protected override void SpawnProduct()
    {
        if (spawnProduct != null && spawnPoint != null&&spawnedProducts.Count<36&&collectedProducts.Count!=0)
        {      totalCollectedProductCount--;

            var lastProduct = collectedProducts[collectedProducts.Count - 1];
            lastProduct.DOMove(spawnPoint.position, .5f)
                .SetEase(Ease.Linear) // Hareketin doğrusal olmasını sağlar
                .OnComplete(() =>
                {
                    collectedProducts.Remove(lastProduct);
                                Transform newProduct = Instantiate(spawnProduct, spawnPoint.position, spawnProduct.transform.rotation).transform;
                                MoveToArea(newProduct);
                                lastProduct.gameObject.SetActive(false);
                                Destroy(lastProduct.gameObject,1f);

                });

        }

    }
  private  void MoveToArea(Transform product)
    {
        int totalProductCount = spawnedProducts.Count;
        product.DOMove(spawnedProductsLocations[totalProductCount].position, .5f)
            .SetEase(Ease.Linear) // Hareketin doğrusal olmasını sağlar
            .OnComplete(() =>
            {
                spawnedProducts.Add(product);
                    
            });
    }

  protected override void TakeProductFromWorker()
    {
      var product=  GameManager.Instance.player.GiveProduct();
      if (product==null)
      {
          return;
      }

      Vector3 targetPos = collectedProductsLocations[totalCollectedProductCount].position;
      product.rotation = collectedProductsLocations[totalCollectedProductCount].rotation;
      totalCollectedProductCount++;

      product.DOMove(targetPos, .5f)
          .SetEase(Ease.Linear) // Hareketin doğrusal olmasını sağlar
          .OnComplete(() =>
          {
              collectedProducts.Add(product);
                    
          });
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
    private IEnumerator TimerCoroutineForTakeProduct()
    {
        while (true)
        {
            if (IsPlayerTakeProduct)
            {
                TakeProductFromWorker();


            }
            yield return new WaitForSeconds(.2f);
        }
    }
    private IEnumerator TimerCoroutineForGiveProduct()
    {
        while (true)
        {
            if (IsPlayerGiveProduct)
            {
                GiveProductToWorker();

            }
            yield return new WaitForSeconds(.2f);
        }
    }
   
    private void SpawnedProductAreaInteracted()
    { IsPlayerGiveProduct = true;
        Debug.LogWarning("girdi!");
        StartCoroutine(TimerCoroutineForGiveProduct());

    }

    private void SpawnedProductAreaInteractEnd()
    {
        IsPlayerGiveProduct = false;  
        Debug.LogWarning("cıktı!");
        StopCoroutine(TimerCoroutineForGiveProduct());


    }
    
    private void CollectProductAreaInteracted()
    { IsPlayerTakeProduct = true;
        Debug.LogWarning("girdi!");
        StartCoroutine(TimerCoroutineForTakeProduct());

    }

    private void CollectProductAreaInteractEnd()
    {
        IsPlayerTakeProduct = false;  
        Debug.LogWarning("collectcıktı!");
        StopCoroutine(TimerCoroutineForTakeProduct());


    }
}
