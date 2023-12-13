using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CSVReader
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };


    // List<Dictionary<string, object>>
    // 받은 값쓸때 List[i][딕셔너리키] 에서 i는 세로줄 순서, 딕셔너리 키는 가로줄 칸 이름


    public static List<Dictionary<string, object>> Read(string file) //csv파일명적고 호출하면 List<Dictionary<string, object>> 뱉음
    {

        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>(); // 호출하면 배출될 데이터

        TextAsset data = Resources.Load(file) as TextAsset; //리소스폴더에서 저 이름 csv파일 통짜로 글씨받는거

        string[] lines = Regex.Split(data.text, LINE_SPLIT_RE); // csv한줄로 받은걸 엑셀상의 가로로만 잘라서 string[]에 넣기


        if (lines.Length <= 1)
        {
            return list;
        }

        string[] header = Regex.Split(lines[0], SPLIT_RE); //0번째 가로줄은 헤더(데이터이름)부분이니까 그거 쪼개 넣기


        for (int i = 1; i < lines.Length; i++) // 0번줄은 헤더니까 두번쨰 가로줄부터 시작
        {

            string[] values = Regex.Split(lines[i], SPLIT_RE); // 가로1줄을 1칸씩으로 쪼개서 string[]에 넣기


            if (values.Length == 0 || values[0] == "") // i번 가로줄이 빈칸이거나 첫번째 칸이 비어있으면 스킵하고 다음줄 읽음. (두번째칸에 글있어도 안읽음)
            {
                continue;
            }

            Dictionary<string, object> entry = new Dictionary<string, object>(); //최종으로 배출될값 임시로 담는거


            for (int j = 0; j < header.Length && j < values.Length; j++) //가로줄 한칸씩 쪼갠거 1번해더에 1번 2번해더에 2번 일케 넣어주는건듯?
            {
                string value = values[j];

                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", ""); // 맨앞,맨뒤에 "붙어있는거 전부 제거. 중간에있는 "는 제거안됨

                value = value.Replace("\"\"", "\""); //계속 따옴표가 ""로 나와서 추가...


                object finalvalue = value; //여기까지되면 값은 string형식으로 볼수있음(자료형이 오브젝트라..)

                int n;
                float f;

                if (int.TryParse(value, out n)) //혹시 값이 int나 float일 경우에 해당 자료형으로 바꿔넣기
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }

                entry[header[j]] = finalvalue; // 딕셔너리에 추가. 키 값 = 헤드명, 값은 파이널벨류;

            }

            list.Add(entry);
        }

        return list;

    }
}

