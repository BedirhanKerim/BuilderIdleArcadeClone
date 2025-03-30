using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineBase : MonoBehaviour
{
    [SerializeField] protected GameObject spawnProduct; // Üretilecek ürün prefabı
    [SerializeField] protected Transform spawnPoint; // Ürünün doğacağı nokta
    [SerializeField] protected float productionInterval = 1f; // Üretim süresi
    [SerializeField] protected List<Transform> spawnedProducts;
    [SerializeField] protected Transform[] spawnedProductsLocations;
    protected bool isProducing = false; // Makine çalışıyor mu?
    protected Coroutine productionCoroutine;

    protected virtual void Start()
    {
        StartProduction(); // Varsayılan olarak üretime başlasın
    }

    public virtual void StartProduction()
    {
        if (!isProducing)
        {
            isProducing = true;
            productionCoroutine = StartCoroutine(Produce());
        }
    }

    public virtual void StopProduction()
    {
        if (isProducing)
        {
            isProducing = false;
            if (productionCoroutine != null)
            {
                StopCoroutine(productionCoroutine);
            }
        }
    }

    protected virtual IEnumerator Produce()
    {
        while (isProducing)
        {
            SpawnProduct();
            yield return new WaitForSeconds(productionInterval);
        }
    }

    protected virtual void SpawnProduct()
    {
       /* if (spawnProduct != null && spawnPoint != null)
        {
            Instantiate(spawnProduct, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("SpawnProduct veya SpawnPoint atanmamış!");
        }*/
    }
    protected virtual void GiveProductToWorker()
    {
        
    }
    protected virtual void TakeProductFromWorker()
    {
        
    }
}
