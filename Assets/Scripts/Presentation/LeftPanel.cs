using System;
using OC.Core;
using TMPro;
using UnityEngine;

namespace OC.Presentation
{
    public class LeftPanel : Singleton<LeftPanel>
    {
        [SerializeField] private TextMeshProUGUI infoText;
        
        public void InfoRefresh()
        {
            var str = $"￥{GameMaster.GameRun.Money:f0}\t{GameMaster.GameRun.TimeInfo.GetPeriodString()}\n";
            str += $"{GameMaster.GameRun.TimeInfo.DayInWeek()}\t{GameMaster.GameRun.TimeInfo.Month}月{GameMaster.GameRun.TimeInfo.Day}日\n";
            str += $"{GameMaster.GameRun.CurrentLocation.FullName}";
            infoText.text = str;
        }
    }
}