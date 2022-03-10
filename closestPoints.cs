using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closestPoints : MonoBehaviour
{
    // Start is called before the first frame update
    ComputeShader computeShader;
    ComputeBuffer buffer;
    ComputeBuffer meshBuffer;
    Vector3[] positions;
    Vector3[] mesh;
    Vector3[] ClosestPoint;
    Unity.Collections.NativeArray<Vector3> array;
    public Rigidbody rb;
    public float forceFactor;
    computeit computeitscript;
    UnityEngine.Rendering.AsyncGPUReadbackRequest request;
    //int size=0;

    private void Awake() {
        computeShader = (ComputeShader)Instantiate(Resources.Load ("closestOne"));
        computeitscript= FindObjectOfType<computeit>();
        
    }
    void Start()
    {
        int kernel=computeShader.FindKernel("CSMain");
        
        positions= new Vector3[transform.childCount]; 
        for(int i=0;i<transform.childCount;i++){ // initialize array.
            positions[i] = new Vector3(transform.GetChild(i).position.x,0,transform.GetChild(i).position.z);
        }
        
        buffer = new ComputeBuffer(transform.childCount, System.Runtime.InteropServices.Marshal.SizeOf(typeof(Vector3)));
       meshBuffer = new ComputeBuffer(mesh.Length, System.Runtime.InteropServices.Marshal.SizeOf(typeof(Vector3)));
        
        computeShader.SetBuffer(kernel,"positions", buffer);
        computeShader.SetBuffer(kernel,"MeshData", meshBuffer);
        request= UnityEngine.Rendering.AsyncGPUReadback.Request(buffer);
    }

    // Update is called once per frame
    void Update()
    {
        
            meshBuffer.SetData( mesh);

            computeShader.SetFloat("_Time", Time.timeSinceLevelLoad);
            computeShader.SetVector("_WaveA",waveA);

            for(int i=0;i<transform.childCount;i++){ // initialize array.
                positions[i] = transform.GetChild(i).position;
            }

            buffer.SetData(positions);
            computeShader.Dispatch(0,transform.childCount,1,1);
            ClosestPoint =  new Vector3[transform.childCount];
            //buffer.GetData(ClosestPoint);
            

             if(request.done && !request.hasError)
        {
            //Readback and show result on texture
            array = request.GetData<Vector3>();

            Debug.Log(array[2]);

            //Update mesh

            //Update to collider

            //Request AsyncReadback again
            request= UnityEngine.Rendering.AsyncGPUReadback.Request(buffer);
        }
    }

    private void OnDestroy() {
        buffer.Release();
        meshBuffer.Release();
    }   

}
