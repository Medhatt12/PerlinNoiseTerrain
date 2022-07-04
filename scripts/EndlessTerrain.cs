//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EndlessTerrain : MonoBehaviour
//{
//    public const float maxViewDst = 450;
//    public Transform viewer;
//    public Material mapMaterial;

//    static MapGenerator mapGenerator;
//    public static Vector2 viewerPosition;
//    int chunkSize;
//    int chuncksVisibleInViewDst;

//    Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();
//    List<TerrainChunk> terrainChunksVisibleLastUpdate = new List<TerrainChunk>();

//    void Start()
//    {
//        mapGenerator = FindObjectOfType<MapGenerator>();
//        chunkSize = MapGenerator.mapChunkSize - 1;
//        chuncksVisibleInViewDst = Mathf.RoundToInt(maxViewDst / chunkSize);

//    }

//    void Update()
//    {
//        viewerPosition = new Vector2(viewer.position.x, viewer.position.z);
//        UpdateVisibleChunks();
//    }

//    void UpdateVisibleChunks()
//    {

//        for(int i = 0; i < terrainChunksVisibleLastUpdate.Count; i++)
//        {
//            terrainChunksVisibleLastUpdate[i].SetVisible(false);
//        }
//        terrainChunksVisibleLastUpdate.Clear();

//        int currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x / chunkSize);
//        int currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y / chunkSize);

//        for(int yOffset = -chuncksVisibleInViewDst;yOffset<= chuncksVisibleInViewDst; yOffset++)
//        {
//            for (int xOffset = -chuncksVisibleInViewDst;xOffset<= chuncksVisibleInViewDst; xOffset++)
//            {
//                Vector2 viewedChunkCoord = new Vector2(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);

//                if (terrainChunkDictionary.ContainsKey(viewedChunkCoord))
//                {
//                    terrainChunkDictionary[viewedChunkCoord].UpdateTerrainChunk();
//                    if (terrainChunkDictionary[viewedChunkCoord].isVisible())
//                    {
//                        terrainChunksVisibleLastUpdate.Add(terrainChunkDictionary[viewedChunkCoord]);
//                    }
//                }
//                else
//                {
//                    terrainChunkDictionary.Add(viewedChunkCoord, new TerrainChunk(viewedChunkCoord,chunkSize,transform,mapMaterial));
//                }

//            }
//            {

//            }
//        }
//    }

//    public class TerrainChunk
//    {
//        GameObject meshObject;
//        Vector2 position;
//        Bounds bounds;

//        MapData mapData;

//        MeshRenderer meshRenderer;
//        MeshFilter meshFilter;

//        public TerrainChunk(Vector2 coord, int size, Transform parent, Material material)
//        {
//            position = coord * size;
//            bounds = new Bounds(position, Vector2.one * size);
//            Vector3 positionV3 = new Vector3(position.x, 0, position.y);

//            meshObject = new GameObject("Terrain Chunk");
//            meshRenderer = meshObject.AddComponent<MeshRenderer>();
//            meshFilter = meshObject.AddComponent<MeshFilter>();
//            meshObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
//            meshObject.transform.position = positionV3;
//            meshObject.transform.localScale = Vector3.one * size / 10f;
//            meshObject.transform.parent = parent;
//            SetVisible(false);

//            mapGenerator.requestMapData(OnMapDataReceived);
//        }

//        void OnMapDataReceived(MapData mapData)
//        {
//            mapGenerator.RequestMeshData(mapData, onMeshDataReceived);
//        }

//        void onMeshDataReceived(MeshData meshData)
//        {
//            meshFilter.mesh = meshData.createMesh();
//        }

//        public void UpdateTerrainChunk()
//        {
//            float viewerDstFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(viewerPosition));
//            bool visible = viewerDstFromNearestEdge <= maxViewDst;
//            SetVisible(visible);
//        }
//        public void SetVisible(bool visible)
//        {
//            meshObject.SetActive(visible);
//        }
//        public bool isVisible()
//        {
//            return meshObject.activeSelf;
//        }
//    }

//}
