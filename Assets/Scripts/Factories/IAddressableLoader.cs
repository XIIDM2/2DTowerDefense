using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IAddressableLoader<T>
{
    UniTask<T> Load(string lable);
}
