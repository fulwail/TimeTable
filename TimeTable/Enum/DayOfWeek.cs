using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TimeTable.Enum
{
    public enum DayOfWeek : byte
    {
        [Description("Понедельник")]
        Monday = 1,

        [Description("Вторник")]
        Tuesday =2,
        [Description("Среда")]
        Wednesday =3,
        [Description("Четверг")]
        Thursday =4,
        [Description("Пятница")]
        Friday =5,
        [Description("Суббота")]
        Saturday =6
    }
}