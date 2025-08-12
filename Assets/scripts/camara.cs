using UnityEngine;

public class camara: MonoBehaviour
{
    public Transform objetivo;  

    void LateUpdate()
    {
        if (objetivo != null)
        {
            transform.position = new Vector3(objetivo.position.x, objetivo.position.y, transform.position.z);
        }
    }
}
