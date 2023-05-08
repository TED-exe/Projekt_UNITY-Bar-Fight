using System.Collections;
using UnityEngine;

public class DestroyFragments : MonoBehaviour
{
    [SerializeField] private float timeToDestroyFragments = 2f;
    
    public void OnEnable()
    {
        StartCoroutine(DestroyObjectFragments());
    }
    
    IEnumerator DestroyObjectFragments()
    {
        yield return new WaitForSeconds(timeToDestroyFragments);
        Destroy(gameObject);
    }
}
