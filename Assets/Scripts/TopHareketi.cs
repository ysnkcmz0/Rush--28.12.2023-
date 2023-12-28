using System.Collections;
using UnityEngine;

public class TopHareketi : MonoBehaviour
{
    Vector3 yön;
    public float hız;
    public ZeminSpawner zeminspawnerscripti;
    public static bool düştü_mü;
    public float eklenecekhız;
    
    public GameObject GameOverScreen;

    
    void Start()
    {
       yön = Vector3.forward;
       düştü_mü = false; 
       
    }

    
    void Update()
    {
        if(transform.position.y<= 0.5f)
        {
            düştü_mü = true;
        }
        if(düştü_mü == true)
        {
            GameOverScreen.SetActive(true);
            Debug.Log("düştüm");
            return;

            
        }
        if(Input.GetMouseButtonDown(0))
        {
            if(yön.x == 0)
            {
                yön = Vector3.left;
            }
            else
            {
                yön = Vector3.forward;
            }
            hız = hız + eklenecekhız * Time.deltaTime;

            
        }
    }

    private void FixedUpdate()
    {
        Vector3 hareket = yön * Time.deltaTime * hız;
        transform.position += hareket;
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "zemin")
        {
            GameManager.Instance.Skor++;
            collision.gameObject.AddComponent<Rigidbody>();
            zeminspawnerscripti.zemin_oluştur();
            StartCoroutine(ZeminiSil(collision.gameObject));

        }
    }


    IEnumerator ZeminiSil(GameObject SilinecekZemin)
    {
        yield return new WaitForSeconds(3f);
        Destroy(SilinecekZemin);
    }
}
