using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using Lean.Pool;
using UniRx;
public class SpaceRoom : MonoBehaviour
{
    public GameObject roomBorderTrack;

    public Vector2 roomCenter;
    public Vector2[] maximumBorder;
    public List<RoomBorder> roomBorders;


    // Start is called before the first frame update
    void Start()
    {
        roomBorders = new List<RoomBorder>();
        maximumBorder = new Vector2[4] { new Vector2(-50, -50), new Vector2(-50, 50), new Vector2(50, 50), new Vector2(50, -50) };
        roomBorders.Add(new RoomBorder(new List<Vector2>() { new Vector2(-50, -50), new Vector2(-50, 50), new Vector2(50, 50), new Vector2(50, -50) }, RoomBorder.TrackShapeType.CatmullRom, true));

        Observable.Timer(System.TimeSpan.FromSeconds(3)).Subscribe(_ =>
        {
            GenerateSpaceRoom();
        });
    }

    public void GenerateSpaceRoom()
    {
        for(int i = 0; i < roomBorders.Count; i++)
        {
            SplineComputer rbt = Instantiate(roomBorderTrack, transform).GetComponent<SplineComputer>();
            for (int j = 0; j < roomBorders[i].nodeList.Count; j++)
            {
                rbt.SetPoint(j, new SplinePoint(roomCenter + roomBorders[i].nodeList[j]));
            }

            if (roomBorders[i].isClosed)
            {
                rbt.Close();
            }
            rbt.RebuildImmediate();
        }
    }
}

[System.Serializable]
public class RoomBorder
{
    public enum TrackShapeType { Linear, CatmullRom };

    public List<Vector2> nodeList;
    public TrackShapeType trackShapeType;
    public bool isClosed;

    public RoomBorder(List<Vector2> nodeList, TrackShapeType trackShapeType, bool isClosed)
    {
        this.nodeList = nodeList;
        this.trackShapeType = trackShapeType;
        this.isClosed = isClosed;
    }
}
