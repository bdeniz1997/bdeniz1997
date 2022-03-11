using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closestPoints : MonoBehaviour
{
    // Start is called before the first frame update
    ComputeShader computeShader;
    ComputeBuffer meshBuffer;
    Vector3[] mesh;
    Vector3[,] vertexPoints;
    int[,] index;
    
    public Rigidbody rb;
    public float forceFactor;
    //int size=0;

    private void Awake() {
        computeShader = (ComputeShader)Instantiate(Resources.Load ("closestOne"));        
    }
    void Start()
    {
        int kernel=computeShader.FindKernel("CSMain");
        
        //positions:
   
       meshBuffer = new ComputeBuffer(mesh.Length, System.Runtime.InteropServices.Marshal.SizeOf(typeof(Vector3)));
        
        computeShader.SetBuffer(kernel,"MeshData", meshBuffer);
        
        meshBuffer.SetData(mesh);
        
        computeShader.Dispatch(0,transform.childCount,1,1);
        
        
    }

    // Update is called once per frame
    void Update()
    {
            computeShader.SetFloat("_Time", Time.timeSinceLevelLoad);
            computeShader.SetVector("_WaveA",waveA);

            for(int i=0;i<transform.childCount;i++){ // initialize array.
                positions[i] = transform.GetChild(i).position;
            }

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
