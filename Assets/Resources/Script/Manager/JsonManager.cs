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
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json",createPath,fileName), FileMode.Create);     // ������ ������ ����, ������ �����
        byte[] data = Encoding.UTF8.GetBytes(jsonData);                                                                 // ���ڵ��� ��Ű�������� ��½� ���� �߻�
        fileStream.Write(data, 0, data.Length);                                                                         // Write(���� �����͸� �����ϴ� ����,�����ϱ� ������ ������, ����� �ִ� ����Ʈ ��)
        fileStream.Close();
    }

    public T LoadJsonFile<T>(string loadPath, string fileName)                            // Load  (PC ���� ----- ���� ������� �ʴ���)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        return JsonToObject<T>(jsonData);
    }

    public T AndroidLoadJson<T>(string loadPath)                                           // Load (Android or PC ��� ����)
    {
        return JsonToObject<T>(Resources.Load<TextAsset>($"{loadPath}").text);
    }
    
}
