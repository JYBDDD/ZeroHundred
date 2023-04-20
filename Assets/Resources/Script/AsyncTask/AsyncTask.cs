using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;

public class AsyncTask
{
    public async void Run(Action action,float wait = 0f)
    {
        await Task.Run(() =>
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            while (true)
            {
                string time = watch.ElapsedMilliseconds.ToString("F0");
                if (int.Parse(time) >= wait * 1000)
                {
                    watch.Stop();
                    action.Invoke();
                    break;
                }
            }
        });
    }
}
