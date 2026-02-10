using UnityEngine;
using System.Collections;


public class Testing : MonoBehaviour
{
    public Robot rb;
    void Start()
    {

        if (rb != null)
        {
            //StartCoroutine(RunSequence());

        }
        else
        {
            Debug.LogError("No Robot found in the scene!");
        }
    }
    IEnumerator RunSequence()
    {
    
        yield return StartCoroutine(rb.Turn(180));
        yield return StartCoroutine(rb.Move(1));
        yield return StartCoroutine(rb.Place());

        yield return new WaitForSeconds(1f);



        yield return StartCoroutine(rb.Turn(90));
        yield return StartCoroutine(rb.Move(2));
        yield return StartCoroutine(rb.Place());

        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(rb.Turn(90));
        yield return StartCoroutine(rb.Move(1));
        yield return StartCoroutine(rb.Place());
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(rb.Move(2));

        yield return StartCoroutine(rb.Turn(180));

        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(rb.Move(2));
        yield return StartCoroutine(rb.Destroy());
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(rb.Move(2));

    }
}