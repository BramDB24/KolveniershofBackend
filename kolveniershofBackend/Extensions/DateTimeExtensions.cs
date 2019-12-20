using kolveniershofBackend.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Extensions
{
    public static class DateTimeExtensions
    {
        public static int DagVanWeek(this DateTime dateTime)
        {
            var temp = ((int)dateTime.DayOfWeek - 1 + 7) % 7 ;
            return temp == 0 ? (int)Weekdag.Maandag : temp;
        }

        public static int WeekNummer(this DateTime newDateTime, DateTime oldDateTime, int oldWeekNumber)
        {
            var x = newDateTime;
            int weekdaydiff = newDateTime.DagVanWeek() - oldDateTime.DagVanWeek();
            int weekverschil;
            if (weekdaydiff > 0)
            {
                var tempdate = oldDateTime.AddDays(weekdaydiff);
                weekverschil = (newDateTime - tempdate).Days / 7;
            }
            else
            {
                var test = weekdaydiff * -1;
                var tempdate = newDateTime.AddDays((test));
                weekverschil = (tempdate - oldDateTime).Days / 7;
            }
            if (weekverschil < 0)
                return 4- Math.Abs((oldWeekNumber + weekverschil) % 4);
            return (oldWeekNumber -1 + weekverschil) % 4 + 1;
        }
    }
}
