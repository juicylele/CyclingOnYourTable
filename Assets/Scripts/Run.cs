using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Run : MonoBehaviour
{
    public float moveSpeed = 1f;

    private int State;//角色状态

    private int oldState = 0;//前一次角色的状态

    private int UP = 0;//角色状态向前

    private int DOWN = 1;//角色状态向后

    private float accelerate = -5f;

    private float ms = 0;

    public List<Transform> prefebFloor;
    public List<Transform> floors;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        accelerate = -0.5f * moveSpeed;

        if (Input.GetAxis("Vertical") > 0)

        {
            setState(UP);

            CreatDestroyFloor(UP);

            // ms = moveSpeed;

        }

        else

        {

            setState(DOWN);

            // CreatDestroyFloor(DOWN);
        }

    }
  
    void setState(int currState)

    {
        Vector3 transformValue = new Vector3();//定义平移向量

        //int rotateValue = (currState - State) * 180;



        switch (currState)

        {
            case 0://向前移动

                transformValue = Vector3.back * Time.deltaTime * moveSpeed;



                break;

                //case 1://向后移动

                //        ms = ms + accelerate * Time.deltaTime;

                //        if (ms <= 0)
                //    {
                //        ms = 0;

                //    }
                //        else
                //    {
                //        ms = ms;
                //    }
                //    transformValue = Vector3.forward * Time.deltaTime * ms;
                //    break;

        }

        //transform.Rotate(Vector3.up, rotateValue);//旋转

        transform.Translate(transformValue, Space.World);//平移

        oldState = State;//赋值，方便下一次计算

        State = currState;//赋值，方便下一次计算



    }
    void CreatDestroyFloor(int currState)
    {
        Transform lastFloor;
        Transform firstFloor;

        switch (currState)
        {
            case 1://向前生成地面

                lastFloor = floors[floors.Count - 1];
                firstFloor = floors[0];
                GameObject F = GameObject.Find("Bike");
                if (lastFloor.position.z < transform.position.z + 1)
                {
                    Transform prefeb = prefebFloor[Random.Range(0, prefebFloor.Count)];
                    Transform newFloor = Instantiate(prefeb, null);
                    newFloor.position = (lastFloor.position);
                    newFloor.parent = F.transform;
                    floors.Add(newFloor);
                }

                if (firstFloor.position.z < transform.position.z - 1)
                {
                    floors.RemoveAt(0);
                    Destroy(firstFloor.gameObject);
                }
                break;

            case 0://向后生成地面
                lastFloor = floors[0];
                firstFloor = floors[floors.Count - 1];
                GameObject A = GameObject.Find("Bike");
                if (firstFloor.position.z > transform.position.z - 0.1f)
                {
                    Transform prefeb = prefebFloor[Random.Range(0, prefebFloor.Count)];
                    Transform newFloor = Instantiate(prefeb, null);
                    newFloor.position = (firstFloor.position + new Vector3(0, 0, 0.1f));
                    newFloor.parent = A.transform;
                    floors.Add(newFloor);
                }
                if (lastFloor.position.z > transform.position.z + 0.1f)
                {
                    floors.RemoveAt(0);
                    Destroy(lastFloor.gameObject);
                }
                break;
        }

    }
}






