using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TaskManager : MonoSingleton<TaskManager>
{
    /// <summary>
    /// 딜레이후 액션 실행
    /// </summary>
    /// <param name="delayTime"></param>
    /// <param name="ac"></param>
    /// <returns></returns>
    public async UniTaskVoid TaskDelayAction(float delayTime, Action ac)
    {
        CancellationTokenSource tokenS = new CancellationTokenSource();

        int dT = (int)(1000 * delayTime);

        await UniTask.Delay(dT, false, PlayerLoopTiming.Update, tokenS.Token);

        ac.Invoke();

        TaskOff(tokenS);
    }

    /// <summary>
    /// 업데이트 Task
    /// </summary>
    /// <param name="ac"></param>
    /// <param name="updateOff"></param>
    /// <returns></returns>
    public async UniTaskVoid Update_LoopTime(Action ac, bool updateOff)
    {
        CancellationTokenSource tokenS = new CancellationTokenSource();
        
        while(true)
        {
            await UniTask.Yield();
            ac.Invoke();

            if(updateOff == true)
            {
                TaskOff(tokenS);
                break;
            }
        }
    }

    public void TaskOff(CancellationTokenSource source)
    {
        source.Cancel();
        source.Dispose();
    }
    


}
