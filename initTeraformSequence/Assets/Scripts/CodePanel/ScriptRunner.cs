using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScriptRunner : MonoBehaviour
{
    [SerializeField] BlockView nodeView;
    [SerializeField] Robot rb;
    [SerializeField] int totalEnergy = 10;
    private int currentEnergy;

    [SerializeField] Image fillImage;

    public void SetFill(float value)
    {
        fillImage.fillAmount = Mathf.Clamp01(value);
    }

    public void RunUI()
    {
        StopAllCoroutines();
        StartCoroutine(Run(nodeView.Node));
        currentEnergy = totalEnergy;
    }

    IEnumerator Run(ScriptNode start)
    {
        ScriptNode current = start;
        if(currentEnergy<=0)
        {
            Debug.Log("Out of energy");
            yield return null;
        }
        else
        {
            while (current != null)
            {
                yield return Execute(current);
                current = current.GetOutput(FlowPort.Next);
            }
        }
    }

    IEnumerator Execute(ScriptNode node)
    {
        switch (node.Type)
        {
            case NodeType.Start:
                yield break;

            case NodeType.Move:
                {
                    int steps = node.GetInt();
                    currentEnergy -= (node.Energy * steps);
                    yield return rb.Move(steps);
                    break;
                }

            case NodeType.Rotate:
                {
                    int angle = node.GetInt();
                    currentEnergy -= node.Energy;
                    yield return rb.Turn(angle);
                    break;
                }
            case NodeType.Plant:
                {
                    yield return rb.Place();
                    currentEnergy -= node.Energy;
                    yield return new WaitForSeconds(1f);
                    break;
                }
            case NodeType.Harvest:
                {
                    yield return rb.Destroy();
                    currentEnergy -= node.Energy;
                    yield return new WaitForSeconds(1f);
                    break;
                }
        }
        SetFill((float)currentEnergy / totalEnergy);
    }
}
