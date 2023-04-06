using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    private Vector2[] dotes = new Vector2[10]; // массив с координатами точек (по оси x и y)
    private GameObject[] lines = new GameObject[10]; // массив линий

    private void Curves(GameObject obj) // в данном случае первым объектом является тот, на который висит этот скрипт. Второй объект это obj
    {
        Vector2 dot1 = new Vector2(transform.position.x, transform.position.y); // начальная точка
        Vector2 dot3 = new Vector2(obj.transform.position.x, obj.transform.position.y); // конечная точка
        Vector2 dot2 = new Vector2((dot1.x+dot3.x)/2, dot3.y+2f); // точка между начальной и конечной. Находится посередине по оси x и выше на 2f по оси y
        if (dot1.y>dot3.y)
        {
            dot2 = new Vector2((dot1.x+dot3.x)/2, dot1.y+2f); // если начальная точка выше конечной, меняем
        }

           
        
        int kD= 0; 

        for (int i=0; i<lines.Length; i++) // удаляем предыдущие линии
        {
            if (lines[i])
            {
                Destroy(lines[i]);
            }
        }
        float deltaZ = (obj.transform.position.z - transform.position.z)/10; /** находим изменение по оси Z за один шаг. Всего мы определили 10 шагов, далее точки
        будут находиться тоже для этого количества шагов **/




        for (float i=0; i<1; i+=0.1f) //находим точки по параметру i для десяти состояний
        {   
            float tempX = (1-i)*(1-i)*dot1.x+2*(1-i)*i*dot2.x+i*i*dot3.x;
            float tempY = (1-i)*(1-i)*dot1.y+2*(1-i)*i*dot2.y+i*i*dot3.y;
            dotes[kD] = new Vector2(tempX, tempY);
            kD++;

        }

        for (int i=0; i<9; i++ ) // вызываем метод для отрисовки всей кривой по частям
        {   
            Vector3 startD = new Vector3(dotes[i].x, dotes[i].y, transform.position.z+deltaZ*i);
            Vector3 endD = new Vector3(dotes[i+1].x, dotes[i+1].y, transform.position.z+deltaZ*(i+1));
            DrawLine(startD, endD, new Vector4(0,0,1,1), i);
        }



    }

    void DrawLine(Vector3 start, Vector3 end, Color color, int k, float duration = 0.01f) // метод для отрисовки линий
    {
         lines[k] = new GameObject();
         lines[k].transform.position = start;
         lines[k].AddComponent<LineRenderer>();
         LineRenderer lr = lines[k].GetComponent<LineRenderer>();
         lr.material = powersMaterial[1];
         lr.SetColors(color, color);
         lr.SetWidth(0.1f, 0.1f);
         lr.SetPosition(0, start);
         lr.SetPosition(1, end);        
    }

    void Update()
    {
        //в нужный момент вызываем Curves()
        //не заюываем удалять объекты из массива lines, когда они уже не нужны
    }


}


