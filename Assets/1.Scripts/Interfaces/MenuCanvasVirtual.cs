using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvasVirtual : MonoBehaviour, IMenuCanvas
{
    [SerializeField] protected float _transitionTime;
    public virtual IEnumerator OnOpen()
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(_transitionTime);
    }
    public virtual IEnumerator OnClose()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(_transitionTime);
    }

}
