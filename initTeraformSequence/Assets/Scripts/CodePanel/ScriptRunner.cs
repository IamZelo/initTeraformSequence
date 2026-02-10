using System.Collections;
using UnityEngine;

public class ScriptRunner : MonoBehaviour
{
    [SerializeField] BlockView nodeView;
    [SerializeField] Robot rb;

    public void RunUI()
    {
        StopAllCoroutines();
        StartCoroutine(Run(nodeView.Node));
    }

    IEnumerator Run(ScriptNode start)
    {
        ScriptNode current = start;

        while (current != null)
        {
            yield return Execute(current);
            current = current.GetOutput(FlowPort.Next);
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
                    yield return rb.Move(steps);
                    break;
                }

            case NodeType.Rotate:
                {
                    int angle = node.GetInt();
                    yield return rb.Turn(angle);
                    break;
                }
            case NodeType.Plant:
                {
                    yield return rb.Place();
                    yield return new WaitForSeconds(1f);
                    break;
                }
            case NodeType.Harvest:
                {
                    yield return rb.Destroy();
                    yield return new WaitForSeconds(1f);
                    break;
                }
        }
    }
}
