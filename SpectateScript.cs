using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectateScript : MonoBehaviour
{
    public Transform PlatformTransform;
    public Transform PlayerTransform;
    public GameObject Platform;
    public GameObject Player;
    public Renderer rend;
    public bool isRising;

    // Start is called before the first frame update
    void Start()
    {
        Platform.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(Spectate());
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            StartCoroutine(SpectateDown());
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Player.GetComponent<Rigidbody>().isKinematic = false;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Player.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    IEnumerator Spectate()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = false;
        Player.GetComponent<Rigidbody>().isKinematic = false;
        isRising = true;
        Platform.transform.SetParent(PlatformTransform);
        PlatformTransform.GetComponent<Animator>().Play("Spectate");
        Platform.SetActive(true);
        yield return new WaitForSeconds(0f);
    }

    IEnumerator SpectateDown()
    {
        Player.GetComponent<Rigidbody>().isKinematic = true;
        isRising = false;
        PlatformTransform.GetComponent<Animator>().Play("New State");
        Platform.transform.SetParent(PlayerTransform);
        yield return new WaitForSeconds(0.1f);
        Platform.SetActive(false);
        PlatformTransform.DetachChildren();
        yield return new WaitForSeconds(0f);
    }
}
