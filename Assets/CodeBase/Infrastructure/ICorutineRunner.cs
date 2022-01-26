using System.Collections;
using UnityEngine;

namespace CodeBase.Services.Input.LoadScene
{
    public interface ICorutineRunner
    {
        Coroutine StartCoroutine(IEnumerator enumerator);
    }
}