using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMenuCanvas 
{
    IEnumerator OnOpen();
    IEnumerator OnClose();
}