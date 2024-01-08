using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static Node[] nodes;
        static int currentNodeId;

        static Queue<int> allPosibleMoveNode = new Queue<int>();

        static void Main(string[] args)
        {
            CreateData();

            #region 01

            Console.Write("Current node ID = ");
            currentNodeId = int.Parse(Console.ReadLine());

            Console.Write("Roll = ");
            var count = Console.ReadLine();

            if (!int.TryParse(count, out var moveCount))
            {
                Console.WriteLine("Can't try parse 'Roll'");
                goto end;
            }

            FindPath(currentNodeId, moveCount, nodes.ToList());

            return;

            var listPossible = new List<int>();
            var listParent = new List<int>();
            var pathList = new List<NodePath>();

            var nodeList = new List<Node>(nodes); //แปลง nodes เป็น List
            var nodeIndex = nodeList.FindIndex(x => x.nodeId == currentNodeId); //เอา List มาหา Index ของ nodes ที่เลือกไว้

            var currentNode = nodes[nodeIndex];
            allPosibleMoveNode.Enqueue(currentNode.nodeId); //Node ทั้งหมดที่สามารถเดินไปได้

            listPossible.Add(currentNode.nodeId);

            var parentNode = new Queue<int>();
            parentNode.Enqueue(-1); //Node ก่อนหน้านี้ (parent node) ที่ใส่ -1 ตอนแรกเพราะ node มันไม่ได้เอามาจากใคร (currentNode ตอนนี้เป็นตัวแรก)

            var distanceOfEachNode = new Queue<int>();
            distanceOfEachNode.Enqueue(moveCount); //บอกจำนวนที่จะต้องเดินใน Queue ของ allPosileMoveNode ว่าเหลืออีกกี่รอบ

            var path = new List<NodePath>();

            for (int i = 0; i < currentNode.connectedNode.Length; i++)
            {
                var tempPath = new NodePath(currentNode.nodeId);
                path.Add(tempPath);
            }

            var tempNodeIndex = 0;
            var tempNode = new int[0];

            #region 01
            /*
            while (moveCount > 0)
            {
                while (distanceOfEachNode.First() == moveCount) //check ว่าจำนวนที่จะต้องเดินของ Node ตัวแรก = moveCount หรือไม่
                {
                    tempNodeIndex = nodeList.FindIndex(x => x.nodeId == allPosibleMoveNode.First());
                    tempNode = nodes[tempNodeIndex].connectedNode; //หา Connect node ของ Node ตัวแรกใน Queue ว่า Connect กับ Node ไหนบ้าง

                    for (int i = 0; i < tempNode.Length; i++) //loop Connect node ทั้งหมด
                    {
                        if (tempNode[i] == parentNode.First()) //check ว่า connect node ตอนนี้เป็น node parent หรือไม่
                        {
                            continue;
                        }

                        //if (allPosibleMoveNode.Contains(tempNode[i]))
                        //{
                        //    continue;
                        //}

                        allPosibleMoveNode.Enqueue(tempNode[i]); //แล้วเอามาใส่ใน allPosibleMoveNode
                        distanceOfEachNode.Enqueue(moveCount - 1); //ใส่จำนวนการเดินที่เหลือของ Node
                        parentNode.Enqueue(allPosibleMoveNode.First()); //ใส่ parent node ว่าเอามาจาก node ไหน
                    }

                    allPosibleMoveNode.Dequeue(); //เอา node ตัวแรกที่ได้ connect node มาแล้วออก (เพราะได้ข้อมูลครบแล้ว)
                    distanceOfEachNode.Dequeue(); //เอาจำนวนการเดินของ node ตัวแรกที่ได้ connect node มาแล้วออก (เพราะได้ข้อมูลครบแล้ว)
                    parentNode.Dequeue(); //เอา parent node ตัวแรกออก (เพราะได้ข้อมูลครบแล้ว)
                }
                moveCount--;
            }
            */
            #endregion


            var nodeAdd = 0;
            var listPosibleIndex = 0;

            while (moveCount > 0)
            {
                nodeAdd = 0;
                while (distanceOfEachNode.First() == moveCount) //check ว่าจำนวนที่จะต้องเดินของ Node ตัวแรก = moveCount หรือไม่
                {
                    tempNodeIndex = nodeList.FindIndex(x => x.nodeId == allPosibleMoveNode.First());
                    tempNode = nodes[tempNodeIndex].connectedNode; //หา Connect node ของ Node ตัวแรกใน Queue ว่า Connect กับ Node ไหนบ้าง

                    for (int i = 0; i < tempNode.Length; i++) //loop Connect node ทั้งหมด
                    {
                        if (tempNode[i] == parentNode.First()) //check ว่า connect node ตอนนี้เป็น node parent หรือไม่
                        {
                            continue;
                        }

                        allPosibleMoveNode.Enqueue(tempNode[i]); //แล้วเอามาใส่ใน allPosibleMoveNode
                        distanceOfEachNode.Enqueue(moveCount - 1); //ใส่จำนวนการเดินที่เหลือของ Node
                        parentNode.Enqueue(allPosibleMoveNode.First()); //ใส่ parent node ว่าเอามาจาก node ไหน

                        listPossible.Add(tempNode[i]);
                        listParent.Add(allPosibleMoveNode.First());
                        nodeAdd++;
                    }

                    var node = allPosibleMoveNode.Dequeue(); //เอา node ตัวแรกที่ได้ connect node มาแล้วออก (เพราะได้ข้อมูลครบแล้ว)
                    distanceOfEachNode.Dequeue(); //เอาจำนวนการเดินของ node ตัวแรกที่ได้ connect node มาแล้วออก (เพราะได้ข้อมูลครบแล้ว)
                    var parent = parentNode.Dequeue(); //เอา parent node ตัวแรกออก (เพราะได้ข้อมูลครบแล้ว)
                }
                moveCount--;
            }

            path.Count();

            var tempCount = allPosibleMoveNode.Count;
            Console.Write($"All posible move node = ");

            var ListOfPosibleNode = new List<int>();

            for (int i = 0; i < tempCount; i++)
            {
                while (ListOfPosibleNode.Contains(allPosibleMoveNode.First()) || allPosibleMoveNode.First().Equals(currentNodeId))
                {
                    allPosibleMoveNode.Dequeue();

                    if (allPosibleMoveNode.Count <= 0)
                    {
                        break;
                    }
                }

                if (allPosibleMoveNode.Count <= 0)
                {
                    break;
                }

                ListOfPosibleNode.Add(allPosibleMoveNode.Dequeue());

                if (allPosibleMoveNode.Count <= 0)
                {
                    break;
                }
            }

            for (int i = 0; i < ListOfPosibleNode.Count; i++)
            {
                Console.Write($"{ListOfPosibleNode[i]}, ");
            }

            var tempPathCount = path.Count;

            var ListOfPath = new List<int>();

            Console.WriteLine();
            Console.Write("Path = ");

            for (int i = 0; i < tempPathCount; i++)
            {
                for (int x = 0; x < path[i].path.Count; x++)
                {
                    Console.Write($"{path[i].path[x]}, ");
                }
                Console.WriteLine();
            }
        #endregion

        end:
            Console.WriteLine();
            Console.WriteLine("-- End --");
            Console.ReadLine();
        }

        private static void CreateData()
        {
            nodes = new Node[9];
            nodes[0] = new Node
            {
                nodeId = 0,
                connectedNode = new int[] { 1, 4, 2 }
            };
            nodes[1] = new Node
            {
                nodeId = 1,
                connectedNode = new int[] { 0, 3, 4 }
            };
            nodes[2] = new Node
            {
                nodeId = 2,
                connectedNode = new int[] { 0, 3, 5, 7, 8 }
            };
            nodes[3] = new Node
            {
                nodeId = 3,
                connectedNode = new int[] { 1, 2, 6, 8 }
            };
            nodes[4] = new Node
            {
                nodeId = 4,
                connectedNode = new int[] { 0, 1 }
            };
            nodes[5] = new Node
            {
                nodeId = 5,
                connectedNode = new int[] { 2, 7 }
            };
            nodes[6] = new Node
            {
                nodeId = 6,
                connectedNode = new int[] { 3, 8 }
            };
            nodes[7] = new Node
            {
                nodeId = 7,
                connectedNode = new int[] { 2, 5, 8 }
            };
            nodes[8] = new Node
            {
                nodeId = 8,
                connectedNode = new int[] { 2, 3, 7 }
            };

        }

        private const bool NO_REPEAT = true;
        private static void FindPath(int startId, int roll, List<Node> masterdata)
        {
            var rollcount = 1;
            var outputPath = new List<List<int>>();
            var tempPath = new List<int>();

            //init first loop rollcount = 0
            {
                var firstConnectedNodes = masterdata.Find(node => node.nodeId == startId).connectedNode;
                for (var i = 0; i < firstConnectedNodes.Length; i++)
                {
                    outputPath.Add(new List<int>() { startId, firstConnectedNodes[i] });
                }
            }

            for (var i = 0; i < outputPath.Count; i++)
                Console.WriteLine($"init first step: {outputPath[i].ToStringList()}");

            //rollcount >= 1
            while (rollcount < roll)
            {
                Console.WriteLine($"\n@ rollCount: {rollcount}");

                for (var i = 0; i < masterdata.Count; i++) //loop masterdata to add connected node after
                {
                    var data = masterdata[i];
                    var parentId = data.nodeId;
                    var copyOutput = new List<List<int>>(outputPath);
                    var tempPaths = copyOutput.FindAll(nodes => nodes[rollcount] == parentId);

                    if (tempPaths.Count == 0)
                        continue;

                    Console.WriteLine($"---- (loop masters for {parentId}) adding children");

                    //try adding data.connectedNode after last element of those match parent
                    for (var j = 0; j < tempPaths.Count; j++)
                    {
                        var newPaths = new List<List<int>>();
                        for (var k = 0; k < data.connectedNode.Length; k++)
                        {
                            var newNode = data.connectedNode[k];

                            if (NO_REPEAT && tempPaths[j].Contains(newNode)) //do not add if path has repeated id
                                continue;

                            var newPath = new List<int>(tempPaths[j]);
                            newPath.Add(newNode);
                            newPaths.Add(newPath);
                            //Console.WriteLine($"found new path: {newPath.ToStringList()}");
                        }
                        if (newPaths.Count > 0)
                        {
                            var insertIndex = copyOutput.FindIndex(nodes => nodes[rollcount] == parentId);
                            copyOutput.InsertRange(insertIndex, newPaths);
                        }
                    }
                    copyOutput.RemoveAll(nodes => nodes.Count - 1 == rollcount && nodes[rollcount] == parentId);
                    outputPath = copyOutput;

                    for (var z = 0; z < outputPath.Count; z++)
                        Console.WriteLine($"Output[{z}]: {outputPath[z].ToStringList()}");
                    Console.WriteLine($"---- (end loop masters for {parentId}) -----\n\n");
                }
                rollcount++;
            }

            Console.WriteLine($"\n\nEND >>> count all path: {outputPath.Count}");
            for (var z = 0; z < outputPath.Count; z++)
                Console.WriteLine($"Output[{z}]: {outputPath[z].ToStringList()}");

            Console.ReadLine(); //?
        }
    }
}