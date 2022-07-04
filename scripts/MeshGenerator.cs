using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(MeshFilter))]
public static class MeshGenerator
{
    public static MeshData GenerateTerrainMesh(float[,] heightMap,float heightMultiplier,AnimationCurve _heightCurve)
    {
        AnimationCurve heightCurve = new AnimationCurve(_heightCurve.keys);
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        float topLeftX = (width - 1) / -2f;
        float topLeftZ = (height - 1) / 2f;

        int meshSimplificationIncrement = 1;
        int verticesPerLine = (width - 1) / meshSimplificationIncrement + 1;

        MeshData meshData = new MeshData(verticesPerLine, verticesPerLine);
        int vertexIndex = 0;

        for (int y = 0; y < height; y+= meshSimplificationIncrement)
        {
            for (int x = 0; x < width; x+= meshSimplificationIncrement)
            {
                meshData.vertices[vertexIndex] = new Vector3(topLeftX+x, heightCurve.Evaluate(heightMap[x,y])*heightMultiplier ,topLeftZ-y);
                meshData.uvs[vertexIndex] = new Vector2(x / (float)width, y / (float)height);

                if(x<width-1 && y< height - 1)
                {
                    meshData.AddTriangle(vertexIndex, vertexIndex + verticesPerLine + 1, vertexIndex + verticesPerLine);
                    meshData.AddTriangle(vertexIndex+ verticesPerLine + 1,vertexIndex,vertexIndex+1);
                }

                vertexIndex++;
            }
        }
        return meshData;
    }
}

public class MeshData
{
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;


    int triangleindex;

    public MeshData(int meshWidth, int meshHeight)
    {
        vertices = new Vector3[meshWidth * meshHeight];
        uvs = new Vector2[meshWidth* meshHeight];
        triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];
    }

    public void AddTriangle(int a,int b,int c)
    {
        triangles[triangleindex] = a;
        triangles[triangleindex+1] = b;
        triangles[triangleindex+2] = c;
        triangleindex += 3;
    }

    public Mesh createMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }
}

   // Mesh mesh;

   // Vector3[] vertices;
   // int[] triangles;
   // Color[] colors;

   // public int xSize = 20;
   // public int zSize = 20;

   // //public int textureWidth = 1024;
   // //public int textureHeight = 1024;

   // //public float noise01Scale = 2f;
   // //public float noise01Amp = 2f;

   // //public float noise02Scale = 4f;
   // //public float noise02Amp = 4f;

   // public Gradient gradient;

   // float minTerrainHeight;
   // float maxTerrainHeight;
   // // Start is called before the first frame update
   // void Start()
   // {
   //     mesh = new Mesh();
   //     GetComponent<MeshFilter>().mesh = mesh;

   //     CreateShape();
   //     UpdateMesh();
   // }
   // private void Update()
   // {
   //     //UpdateMesh();
   // }

   // void CreateShape()
   // {

   //     vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        
   //     for (int i = 0, z = 0; z <= zSize; z++)
   //     {
   //         for(int x = 0; x <= xSize; x++)
   //         {
   //             float y = Mathf.PerlinNoise(x*.3f, z*.3f) * 2f;
   //             //float y = GetNoiseSample(x, z);
   //             vertices[i] = new Vector3(x, y, z);

   //             if (y > maxTerrainHeight)
   //             {
   //                 maxTerrainHeight = y;
   //             }
   //             if (y < minTerrainHeight)
   //             {
   //                 minTerrainHeight = y;
   //             }
   //             i++;
   //         }
   //     }


   //     triangles = new int[xSize*zSize*6];
   //     int vert = 0;
   //     int tris = 0;
   //     for(int z = 0; z < zSize; z++)
   //     {
   //         for (int x = 0; x < xSize; x++)
   //         {
   //             triangles[tris + 0] = vert + 0;
   //             triangles[tris + 1] = vert + xSize + 1;
   //             triangles[tris + 2] = vert + 1;
   //             triangles[tris + 3] = vert + 1;
   //             triangles[tris + 4] = vert + xSize + 1;
   //             triangles[tris + 5] = vert + xSize + 2;

   //             vert++;
   //             tris += 6;
   //            // yield return new WaitForSeconds(.01f);
   //         }
   //         vert++;
   //     }

   //     colors = new Color[vertices.Length];

   //     for (int i = 0, z = 0; z <= zSize; z++)
   //     {
   //         for (int x = 0; x <= xSize; x++)
   //         {
   //             float height = Mathf.InverseLerp(minTerrainHeight,maxTerrainHeight, vertices[i].y);
   //             colors[i] = gradient.Evaluate(height);
   //             i++;
   //         }
   //     }
   // }
   //void UpdateMesh()
   // {
   //     mesh.Clear();
   //     mesh.vertices = vertices;
   //     mesh.triangles = triangles;
   //     mesh.colors = colors;

   //     mesh.RecalculateNormals();
   // }

   
//}
