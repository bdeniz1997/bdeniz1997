using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class computeit : MonoBehaviour
{
    // Start is called before the first frame update
    ComputeShader compute;
    ComputeBuffer buffer;
    //ComputeBuffer positionbuffer;
    //Vector3[] data;
    //Rigidbody rb;
    //public float forceAmount;

    //Vector3[] MeshData;
    public Vector4 waveA,waveB,waveC,waveD,waveE,waveF,waveG;
    public Transform positionTransform;
    public GameObject water;
    //Vector3[] positions;
    MeshFilter mesh;
    public Vector3[] MeshData;
    public int xSize=25;
    public int zSize=17;
    public Unity.Collections.NativeArray<Vector3> PublicMesh;
    UnityEngine.Rendering.AsyncGPUReadbackRequest request;

    private void Awake() {
        compute = (ComputeShader)Instantiate(Resources.Load ("ComputeWaveHeight"));
    }
    void Start()
    {
        BoxCollider collider= GetComponent<BoxCollider>();
        xSize= Mathf.RoundToInt(collider.size.x)+(xSize%2==0? 2 : 3);
        zSize= Mathf.RoundToInt(collider.size.z)+(zSize%2==0? 2 : 3);
            
        mesh=water.GetComponent<MeshFilter>();
        MeshData= mesh.mesh.vertices;

        int kernel=compute.FindKernel("CSMain");
        buffer = new ComputeBuffer(MeshData.Length, System.Runtime.InteropServices.Marshal.SizeOf(typeof(Vector3)));
        //positionbuffer = new ComputeBuffer(12, System.Runtime.InteropServices.Marshal.SizeOf(typeof(Vector3)));
        compute.SetBuffer(kernel,"ResultBuffer", buffer);
        //compute.SetBuffer(kernel,"positionBuffer", positionbuffer);
        //rb=transform.parent.GetComponent<Rigidbody>();

        // data= new Vector3[transform.childCount];
        // for(int i=0;i<transform.childCount;i++){
        //     data[i] = new Vector3(0,0,0);
        // }

        //mesh= GetComponent<MeshFilter>();
        //initialize the array here.
        // 50 x 50
        
        //old ONES. doesnt work precisely.
        //SetMeshData();
        //PublicMesh= new Vector3[xSize*zSize];

        

        // positions = new Vector3[12];
        // for(int i=0;i<transform.childCount;i++){
        //     positions[i] = positionTransform.GetChild(i).transform.position;
        // }
        request= UnityEngine.Rendering.AsyncGPUReadback.Request(buffer);
    
    }

    // Update is called once per frame
    void Update()
    {
        //for(int i=0;i<oldMeshData.Length;i++){
            //Vector3[] position = new Vector3[2];
            //position[0]=shipPos.position;
            //Vector3[] positionTemp = new Vector3[12];
            // for(int i=0;i<transform.childCount;i++){
            //     positionTemp[i] =positions[i] + transform.GetChild(i).transform.position;
            // }
            
            MeshData= mesh.mesh.vertices;
            //SetMeshData();

            buffer.SetData(MeshData);
           // positionbuffer.SetData(positions);
        

            //compute.SetFloats("_WaveA", 1,0,0.5f,1f ); // sending data actual data. setting it.
            compute.SetFloat("_Time", Time.timeSinceLevelLoad);
            compute.SetVector("_WaveA",waveA);
            compute.SetVector("_WaveB",waveB);
            compute.SetVector("_WaveC",waveC);
            compute.SetVector("_WaveD",waveD);
            compute.SetVector("_WaveE",waveE);
            compute.SetVector("_WaveF",waveF);
            compute.SetVector("_WaveG",waveG);
            // compute.SetFloats("_WaveB", 1,0,0.5f,1f  ); // sending data actual data. setting it.
            
            compute.Dispatch(0,8,8,1);

            //var data = new Vector3[MeshData.Length];
            // data = new Vector3[transform.childCount];
            // buffer.GetData(data);



            //  for(int i=0;i<12;i++){
            //     Debug.DrawRay(data[i],Vector3.up,Color.red,1f);
            //  }

            //var _meshData = new Vector3[MeshData.Length];
            //buffer.GetData(_meshData);
            //PublicMesh = _meshData;
            if(request.done && !request.hasError){
                PublicMesh = request.GetData<Vector3>();

                request= UnityEngine.Rendering.AsyncGPUReadback.Request(buffer);
            }

            //mesh.mesh.vertices=_meshData;
            //mesh.mesh.RecalculateNormals();
            

            // var posArray = new Vector3[12];
            // positionbuffer.GetData(posArray);
            
            // for(int i=0;i<12;i++){
            //     Debug.DrawRay(positions[i],posArray[i],Color.red,1f);
            // }
            
            
        //}
    }

    private void FixedUpdate() {
        // for(int i=0;i<12;i++){
        //     if(transform.GetChild(i).position.y<data[i].y){
        //         Vector3 force =  data[i] -transform.GetChild(i).position;
        //         rb.AddForceAtPosition(force *forceAmount,transform.GetChild(i).position);
        //     }
        // }

        // for(int i=0;i<PublicMesh.Length;i++){
        //     Debug.DrawRay(PublicMesh[i],Vector3.up,Color.green,2f);
        // }
    }

    private void OnDestroy() 
    {
        buffer.Release();
    }

    public void SetMeshData(){
        int count=0;
        MeshData= new Vector3[xSize*zSize];
        for(int z=-zSize/2; z<(zSize/2) -1;z++){ //z
            for(int x=-xSize/2; x<(xSize/2) -1;x++){ //x
                Vector3 XD = positionTransform.TransformPoint(new Vector3(x,0,z));
                MeshData[count]= (new Vector3(XD.x,0,XD.z));
                //Debug.DrawRay(MeshData[count],Vector3.up,Color.blue,0.1f);
                count++;
            }
        }
    }
}
