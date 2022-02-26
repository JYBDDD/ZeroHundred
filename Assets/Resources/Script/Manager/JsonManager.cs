using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class JsonManager
{
    public string ObjectToJson(object obj)
    {
        return JsonUtility.ToJson(obj);
    }

    public T JsonToObject<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }

    public void CreateJsonFile(string createPath,string fileName,string jsonData)        // Save
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json",createPath,fileName), FileMode.Create);     // 파일이 없을시 생성, 있을시 덮어쓰기
        byte[] data = Encoding.UTF8.GetBytes(jsonData);                                                                 // 인코딩을 시키지않으면 출력시 오류 발생
        fileStream.Write(data, 0, data.Length);                                                                         // Write(적을 데이터를 포함하는 버퍼,복사하기 시작할 오프셋, 사용할 최대 바이트 수)
        fileStream.Close();
    }

    public T LoadJsonFile<T>(string loadPath, string fileName)                            // Load  (PC 전용 ----- 현재 사용하지 않는중)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        return JsonToObject<T>(jsonData);
    }

    public T AndroidLoadJson<T>(string loadPath)                                           // Load (Android or PC 사용 가능)
    {
        return JsonToObject<T>(Resources.Load<TextAsset>($"{loadPath}").text);
    }
    
}
