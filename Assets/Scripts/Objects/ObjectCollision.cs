using UnityEngine;
using UnityEngine.Events;

public class ObjectCollision : MonoBehaviour
{
    private const int ENVIRONMENT_LAYER_NUMBER = 6;
    private const int THROWED_LAYER_NUMBER = 7;
    private const int PICKABLE_LAYER_NUMBER = 3;

    public UnityEvent onPlayerHit;
    private void OnCollisionEnter(Collision other)
    {
        CheckDestruction(other);
    }

    private void CheckDestruction(Collision other)
    {
        if (other.gameObject.CompareTag(tag: "Player") && gameObject.layer == THROWED_LAYER_NUMBER)
        {
            onPlayerHit.Invoke();
        }
        else if (other.gameObject.layer == ENVIRONMENT_LAYER_NUMBER && gameObject.layer == THROWED_LAYER_NUMBER)
        {
            gameObject.layer = PICKABLE_LAYER_NUMBER;
        }
    }
}
