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
    public Vector4 waveA,waveB,waveC,waveD,waveE,waveF,waveG;
    //int size=0;

    private void Awake() {
        computeShader = (ComputeShader)Instantiate(Resources.Load ("closestOne"));
        computeitscript= FindObjectOfType<computeit>();
        // waveA= computeitscript.waveA;
        // waveB= computeitscript.waveB;
        // waveC= computeitscript.waveC;
        // waveD= computeitscript.waveD;
        // waveE= computeitscript.waveE;
        // waveF= computeitscript.waveF;
        // waveG= computeitscript.waveG;
        
    }
    void Start()
    {
        int kernel=computeShader.FindKernel("CSMain");
        //Computeit= transform.parent.GetComponent<computeit>();
        //mesh=computeitscript.PublicMesh;
        waveA= waveManager.Waves[0];
        waveB= waveManager.Waves[1];
        waveC= waveManager.Waves[2];
        waveD= waveManager.Waves[3];
        waveE= waveManager.Waves[4];
        waveF= waveManager.Waves[5];
        waveG= waveManager.Waves[6];
        
        positions= new Vector3[transform.childCount]; 
        for(int i=0;i<transform.childCount;i++){ // initialize array.
            positions[i] = new Vector3(transform.GetChild(i).position.x,0,transform.GetChild(i).position.z);
        }

         
        
        //mesh= water.GetComponent<MeshFilter>().mesh.vertices;
        
        //mesh= new Vector3[size];
        //mesh= computeitscript.PublicMesh;
        //mesh=water.gameObject.GetComponent<MeshFilter>().mesh.vertices;
        
        buffer = new ComputeBuffer(transform.childCount, System.Runtime.InteropServices.Marshal.SizeOf(typeof(Vector3)));
       meshBuffer = new ComputeBuffer(mesh.Length, System.Runtime.InteropServices.Marshal.SizeOf(typeof(Vector3)));
        
        //mesh=meshObj.gameObject.GetComponent<MeshFilter>().mesh.vertices;
        computeShader.SetBuffer(kernel,"positions", buffer);
        computeShader.SetBuffer(kernel,"MeshData", meshBuffer);
        request= UnityEngine.Rendering.AsyncGPUReadback.Request(buffer);
    }

    // Update is called once per frame
    void Update()
    {
        
            //mesh=water.GetComponent<MeshFilter>().mesh.vertices;

            //mesh= computeitscript.PublicMesh;
            //mesh=meshObj.gameObject.GetComponent<MeshFilter>().mesh.vertices;
            meshBuffer.SetData( mesh);

            computeShader.SetFloat("_Time", Time.timeSinceLevelLoad);
            computeShader.SetVector("_WaveA",waveA);
            computeShader.SetVector("_WaveB",waveB);
            computeShader.SetVector("_WaveC",waveC);
            computeShader.SetVector("_WaveD",waveD);
            computeShader.SetVector("_WaveE",waveE);
            computeShader.SetVector("_WaveF",waveF);
            computeShader.SetVector("_WaveG",waveG);

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

            // for(int i=0;i<transform.childCount;i++){
            //     Debug.DrawRay((ClosestPoint[i]),Vector3.up ,Color.red,0.2f);
            // }
    }

    private void OnDestroy() {
        buffer.Release();
        meshBuffer.Release();
    }

    private void FixedUpdate() {
        // if(ClosestPoint!=null){
        //     for(int i=0;i<transform.childCount;i++){
        //         Debug.DrawRay(positions[i],Vector3.up,Color.green,2f);
        //         Debug.DrawRay(ClosestPoint[i],Vector3.up,Color.blue,2f);
        //     }
        // }
        

        // if(ClosestPoint!=null){
        //     for(int i=0;i<transform.childCount;i++){
        //         if(ClosestPoint[i].y> positions[i].y){
        //             Vector3 force = Vector3.up* (ClosestPoint[i].y - positions[i].y);
        //             if(force==Vector3.zero) force += new Vector3(0,0.1f,0);
                    
        //             rb.AddForceAtPosition(force*forceFactor,positions[i],ForceMode.Impulse);
        //             Debug.DrawRay(ClosestPoint[i],Vector3.up,Color.blue,2f);
        //         }
        //     }
        // }
    }
    

}
