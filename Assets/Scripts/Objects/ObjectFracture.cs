using UnityEngine;

public class ObjectFracture : MonoBehaviour
{
    [SerializeField] private SO_Objects scriptableObject;
    [SerializeField] private float breakForce = 2f;

    public void FractureObject()
    {
        Transform frac = Instantiate(scriptableObject.prefabFractured, transform.position, transform.rotation);
        foreach (Rigidbody rb in frac.GetComponentsInChildren<Rigidbody>())
        {
            Vector3 force = (rb.transform.position - transform.position).normalized * breakForce;
            rb.AddForce(force);
        }
        Destroy(gameObject);
    }
}
