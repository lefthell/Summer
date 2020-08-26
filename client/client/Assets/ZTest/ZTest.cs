using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class ZTest : MonoBehaviour
{
    public NativeArray<Vector3> Velocities { get; private set; }
    public Transform[] trans;// = new Transform[ObjCount];

    public const int ObjCount = 200;
    void Start()
    {
        //trans = new Transform[ObjCount];
        //Vector3[] array = new Vector3[ObjCount];
        //for (int i = 0; i < ObjCount; i++)
        //{
        //    array[i] = UnityEngine.Random.Range(-20f, 20f) * Vector3.one;
        //    trans[i] = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        //    trans[i].SetParent(this.transform);
        //}
        //Velocities = new NativeArray<Vector3>(array, Allocator.Persistent);
        //accessArray = new TransformAccessArray(trans);

    }
    TransformAccessArray accessArray;
    private float time;
    private JobHandle jobHandle1;

    public GameObject obj;
    public Transform parent;

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            JobOneStep();
        }
    }
    private void OnGUI()
    {
        if (GUILayout.Button("JobSystem Test"))
        {
            //JobSystemTest();
        }
        if (GUILayout.Button("List Test"))
        {
            //List<int> a = new List<int>(100);
            //Debug.LogError(a.Count);
            //int? a = null;
            //TestA(a);
            var start = DateTime.Now.Millisecond;
            for (int i = 0; i < 100; i++)
            {
                GameObject go = Instantiate(obj);
                go.transform.parent = parent;
            }
            var end = DateTime.Now.Millisecond;
            Debug.LogError(end - start);
            //TestA(null);
        }
    }
    private void TestA(int? a)
    {
        Debug.LogError("TestA int");
    }
    private void TestA(string a)
    {
        Debug.LogError("TestA string");
    }
    class A
    { }
    private void TestA(A a)
    {
        Debug.LogError("TestA string");
    }
    private void JobSystemTest()
    {
        time = 200f;

        //JobHandle.ScheduleBatchedJobs();
    }
    private void JobOneStep()
    {
        CubeJob cubeJob = new CubeJob();
        cubeJob.Velocities = Velocities;
        var jobHandle = cubeJob.Schedule(accessArray);
        jobHandle.Complete();
    }
    private void OnDestroy()
    {
        jobHandle1.Complete();
        //JobHandle.CompleteAll(jobHandle1);
        accessArray.Dispose();
        Velocities.Dispose();
    }
}
