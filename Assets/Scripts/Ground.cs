using UnityEngine;

public class Ground : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D coll)
    {
        OnCollisionWithPlayer(ref coll);
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        OnCollisionWithPlayer(ref coll);
    }

    private void OnCollisionWithPlayer(ref Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            coll.gameObject.GetComponent<Player>().ToggleGrounded();
        }
    }
}
