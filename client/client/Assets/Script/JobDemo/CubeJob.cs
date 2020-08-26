using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public struct CubeJob : IJobParallelForTransform
{

    public NativeArray<Vector3> Velocities;
    void IJobParallelForTransform.Execute(int index, TransformAccess transform)
    {
        transform.localPosition += Velocities[index];
    }
}
public struct CubJobParallelJob : IJobParallelFor
{
    //public NativeArray<Transform> Velocities;
    public void Execute(int index)
    {
        //throw new System.NotImplementedException();
    }
}
