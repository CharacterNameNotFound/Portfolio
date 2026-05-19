using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils.UtilityTypes.Splines.CatmullRom;
using Random = UnityEngine.Random;

namespace Game.GameMode.Session.WorldGeneration.RoadGeneration
{
    public class RoadGenerator : IRoadGenerator
    {

        public UniTask Generate(Vector2Int worldSize, Tilemap tilemap, RoadGeneratorConfigs roadGeneratorConfigs,
            CancellationToken cancellationToken)
        {
            Vector3Int[] positions = new Vector3Int[roadGeneratorConfigs.MaxRoadRadius * 4 + 1];
            TileBase[] tiles = new TileBase[roadGeneratorConfigs.MaxRoadRadius * 4 + 1];
            
            GenerateInternalPoint(worldSize, tilemap, roadGeneratorConfigs, positions, tiles);
            
            return UniTask.CompletedTask;
        }
        
        private void GenerateInternalPoint(
            Vector2Int worldSize, 
            Tilemap tilemap,
            RoadGeneratorConfigs roadGeneratorConfigs, 
            Vector3Int[] positions, 
            TileBase[] tiles)
        {
            Vector2[] points = new Vector2[roadGeneratorConfigs.SplineSectionsCount +1];

            Vector2 worldHalfSize = worldSize / 2;
            
            // generating internal points
            for (int i = 0; i < points.Length - 2; i++)
            {
                Vector2 random = Random.insideUnitCircle;
                points[i] = worldHalfSize + new Vector2(random.x * worldHalfSize.x, random.y * worldHalfSize.y);
            }


            // a little bit crude, but works, merging
            List<(Vector2 point, float distance)> externalPoints = new List<(Vector2 point, float distance)>();
            for (int i = 0; i < points.Length - 2; i++)
            {
                externalPoints.Add(NearestPointOnSquare(points[i], worldSize));
            }
            
            externalPoints.Sort((a, b) => a.distance.CompareTo(b.distance));
            points[^2] = externalPoints[0].point;
            points[^1] = externalPoints[1].point;
            

            int edgeCount = (points.Length * (points.Length - 1)) / 2;

            RoadVertex[] vertices = new RoadVertex[points.Length];
            RoadEdge[] edges = new RoadEdge[edgeCount];

            for (int i = 0; i < points.Length; i++)
            {
                vertices[i] = new RoadVertex(points[i], i);
            }
            
            int index = 0;
            for (int i = 0; i < points.Length; i++)
            {
                for (int j = i + 1; j < points.Length; j++)
                {
                    edges[index] = new RoadEdge(i, j, Vector2.Distance(points[i], points[j]), false);
                    index++;
                }
            }
            
            Array.Sort(edges, (a, b) => a.Length.CompareTo(b.Length));

            for (int i = 0; i < edges.Length; i++)
            {
                if (vertices[edges[i].FirstVertexIndex].Group == vertices[edges[i].SecondVertexIndex].Group)
                {
                    continue;
                }

                edges[i].SetDraw();
                vertices[edges[i].FirstVertexIndex].IncrementEdges();
                vertices[edges[i].SecondVertexIndex].IncrementEdges();
                
                ReindexVertGroup(vertices, vertices[edges[i].SecondVertexIndex].Group, vertices[edges[i].FirstVertexIndex].Group);
            }

            CatmullRomModel model = new CatmullRomModel(new Vector2[4], roadGeneratorConfigs.CatmullRomAlpha);

            foreach (RoadEdge edge in edges.Where(item => item.Draw))
            {
                DrawEdge(edge, model, edges, vertices, roadGeneratorConfigs.RoadTile, tilemap, roadGeneratorConfigs, positions, tiles);
            }
        }

        private void DrawEdge(
            RoadEdge edge, 
            CatmullRomModel model, 
            RoadEdge[] edges, 
            RoadVertex[] verts, 
            TileBase tile, 
            Tilemap tilemap, 
            RoadGeneratorConfigs roadGeneratorConfigs, 
            Vector3Int[] positions, 
            TileBase[] tiles)
        {
            Vector2 leftOutside = GetOutsidePoint(edge.FirstVertexIndex, edge, edges, verts);
            Vector2 rightOutside = GetOutsidePoint(edge.SecondVertexIndex, edge, edges, verts);

            model.Points[0] = leftOutside;
            model.Points[1] = verts[edge.FirstVertexIndex].Coords;
            model.Points[2] = verts[edge.SecondVertexIndex].Coords;
            model.Points[3] = rightOutside;


            Vector2Int lastPoint = new Vector2Int(-1, -1);
            for (float i = 0; i < 1; i += roadGeneratorConfigs.RoadResolution)
            {
                Vector2 interpolatedPoint = CatmullRomInterpolator.GetPoint(model, i);

                Vector2Int point = new Vector2Int(Mathf.RoundToInt(interpolatedPoint.x), Mathf.RoundToInt(interpolatedPoint.y));

                if (lastPoint == point)
                {
                    continue;
                }

                lastPoint = point;

                int left = -Random.Range(roadGeneratorConfigs.MinRoadRadius, roadGeneratorConfigs.MaxRoadRadius);
                int right = Random.Range(roadGeneratorConfigs.MinRoadRadius, roadGeneratorConfigs.MaxRoadRadius);
                
                int bottom = -Random.Range(roadGeneratorConfigs.MinRoadRadius, roadGeneratorConfigs.MaxRoadRadius);
                int top = Random.Range(roadGeneratorConfigs.MinRoadRadius, roadGeneratorConfigs.MaxRoadRadius);


                int index = 0;
                for (int j = left; j < right; j++)
                {
                    positions[index] = new Vector3Int(point.x + j, point.y);
                    tiles[index] = tile;
                    index++;
                }

                for (int j = bottom; j < top; j++)
                {
                    positions[index] = new Vector3Int(point.x, point.y + j);
                    tiles[index] = tile;
                    index++;
                }
                
                tilemap.SetTiles(positions, tiles);
            }
            
        }

        private (Vector2 point, float distance) NearestPointOnSquare(Vector2 worldPoint, Vector2Int worldSize)
        {
            Vector2 point = NearestPointToLine(new Vector2(0, 0), new Vector2(0, worldSize.y), worldPoint);
            float distance = Vector2.Distance(point, worldSize);

            Vector2 tempPoint = NearestPointToLine(new Vector2(0, worldSize.y), new Vector2(worldSize.x, worldSize.y), worldPoint);
            float tempDistance = Vector2.Distance(tempPoint, worldSize);
            if (distance > tempDistance)
            {
                point = tempPoint;
                distance = tempDistance;
            }
            
            tempPoint = NearestPointToLine(new Vector2(worldSize.x, worldSize.y), new Vector2(worldSize.x, 0), worldPoint);
            tempDistance = Vector2.Distance(tempPoint, worldSize);
            if (distance > tempDistance)
            {
                point = tempPoint;
                distance = tempDistance;
            }
            
            tempPoint = NearestPointToLine(new Vector2(worldSize.x, 0), new Vector2(0, 0), worldPoint);
            tempDistance = Vector2.Distance(tempPoint, worldSize);
            if (distance > tempDistance)
            {
                point = tempPoint;
                distance = tempDistance;
            }

            return (point, distance);
        }
        

        private Vector2 NearestPointToLine(Vector2 rectPointA, Vector2 rectPointB, Vector2 worldPoint)
        {
            Vector2 ap = worldPoint - rectPointA;
            Vector2 ab = rectPointB - rectPointA;

            float squareLength = ab.sqrMagnitude;
            float dot = Vector2.Dot(ap, ab);
            float distance = dot / squareLength;

            return rectPointA + ab * distance;

        }
        

        private Vector2 GetOutsidePoint(int edgeIndex, RoadEdge connectingEdge, RoadEdge[] edges, RoadVertex[] verts)
        {
            int index = -1;
            
            foreach (RoadEdge edge in edges)
            {
                if (edge.IsConnectedEdge(connectingEdge, edgeIndex, out index))
                {
                    break;
                }
            }

            if (index > -1)
            {
                return verts[index].Coords;
            }

            connectingEdge.GetOppositeVertIndex(edgeIndex, out int oppositeIndex);
            return verts[edgeIndex].Coords * 2 - verts[oppositeIndex].Coords;
        }
            
            

        private void ReindexVertGroup(RoadVertex[] verts, int originalGroup, int newGroup)
        {
            for (int i = 0; i < verts.Length; i++)
            {
                verts[i].Group = verts[i].Group == originalGroup ? newGroup : verts[i].Group;
            }
            
        }
        
    }
}