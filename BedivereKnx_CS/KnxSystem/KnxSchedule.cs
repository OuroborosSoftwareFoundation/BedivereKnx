using Knx.Falcon;
using System.Data;

namespace BedivereKnx.KnxSystem
{

    /// <summary>
    /// KNX时间表
    /// </summary>
    public class KnxSchedule
    {

        /// <summary>
        /// 时间表触发事件
        /// </summary>
        public event ScheduleEventHandler? ScheduleEventTriggered;

        /// <summary>
        /// 定时器状态变化事件
        /// </summary>
        public event ScheduleTimerHandler? ScheduleTimerStateChanged;

        /// <summary>
        /// 数据表
        /// </summary>
        public readonly DataTable Table;

        /// <summary>
        /// 定时组数量
        /// </summary>
        public int Count => Table.Rows.Count;

        /// <summary>
        /// 定时队列
        /// </summary>
        public ScheduleSequence Sequence { get; private set; }

        /// <summary>
        /// 定时器状态
        /// </summary>
        public KnxScheduleTimerState TimerState
        {
            get => timerState;
            private set
            {
                if (value != timerState)
                {
                    timerState = value;
                    ScheduleTimerStateChanged?.Invoke(timerState);
                }
            }
        }

        private readonly Timer timer; //主定时器
        private KnxScheduleTimerState timerState;

        public KnxSchedule(DataTable dt)
        {
            Table = dt;
            Sequence = new ScheduleSequence();
            timer = new Timer(_timer_Callback, null, Timeout.Infinite, Timeout.Infinite);
        }

        /// <summary>
        /// 启动定时器
        /// </summary>
        public void TimerStart()
        {
            if (TimerState != KnxScheduleTimerState.Stoped) return; //定时器停止时才能启动
            if (Sequence.Count == 0) return; //定时队列为空的情况下不启动定时器
            TimerStop();
            int due = 60 - DateTime.Now.Minute; //同步至整分钟所差分钟数
            timer.Change(due, 60000); //定时器在下个整分钟触发，然后每分钟触发一次
            timerState = KnxScheduleTimerState.Starting;
        }

        /// <summary>
        /// 停止定时器
        /// </summary>
        public void TimerStop()
        {
            if (TimerState == KnxScheduleTimerState.Stoped) return;
            _ = timer.Change(Timeout.Infinite, Timeout.Infinite);
            timerState = KnxScheduleTimerState.Stoped;
        }

        /// <summary>
        /// 主定时器回调方法
        /// </summary>
        /// <param name="state"></param>
        private void _timer_Callback(object? state)
        {
            if (Sequence.Count == 0)
            {
                TimerStop();
                return;
            }
            if (TimerState != KnxScheduleTimerState.Running)
            {
                TimerState = KnxScheduleTimerState.Running;
            }
            TimeOnlyHM now = new(DateTime.Now); //只包含时分的当前时间
            EventTrigger(now); //触发当前时刻的事件
            if ((now.Hour == 0) && (now.Minute == 0)) //0点整刷新整个表的“已触发”字段
            {
                foreach (DataRow dr in Sequence.Table.Rows)
                {
                    dr["Triggered"] = false;
                }
            }
        }

        /// <summary>
        /// 定时事件触发
        /// </summary>
        private void EventTrigger(TimeOnlyHM time)
        {
            if (time < Sequence.NextTime) return; //当前时间早于当前时间跳过本次判断
            for (int i = Sequence.NextId; i < Sequence.Count; i++) //从下次事件ID开始检测
            {
                DataRow dr = Sequence.Table.Rows[i]; //当前行
                TimeOnlyHM trgTime = dr.Field<TimeOnlyHM>("TriggerTime"); //触发时间
                if (dr.Field<bool>("Enable") && (time == trgTime))
                {
                    GroupValue? val = dr.Field<GroupValue>("Value");
                    if (val != null)
                    {
                        string? ifCode = dr.Field<string>("InterfaceCode"); //接口编号
                        GroupAddress ga = dr.Field<GroupAddress>("GroupAddress"); //组地址
                        KnxGroupEventArgs wea = new(ifCode, ga);
                        string logCode = $"{dr["ScheduleCode"]}_{dr["Time"]}";
                        ScheduleEventTriggered?.Invoke(logCode, wea, val); //触发事件
                        dr["Triggered"] = true; //本次事件设置为已触发
                    }
                }
                else //此处必然是下一次定时的时间
                {
                    Sequence.NextId = dr.Field<int>("Id");
                }
                if (i == Sequence.Count - 1) //当前事件为最后一个的情况，重置计数
                {
                    Sequence.NextId = 0;
                }
            }
        }

    }

    /// <summary>
    /// 定时队列
    /// </summary>
    public class ScheduleSequence
    {

        private int nextId;

        /// <summary>
        /// 序列表
        /// </summary>
        public DataTable Table;

        /// <summary>
        /// 事件数量
        /// </summary>
        public int Count => Table.Rows.Count;

        /// <summary>
        /// 下个事件的ID
        /// </summary>
        public int NextId
        {
            get => nextId;
            set
            {
                nextId = Math.Max(Math.Min(value, Count - 1), 0); //限制ID范围
            }
        }

        /// <summary>
        /// 下个事件的触发时间
        /// </summary>
        public TimeOnlyHM NextTime => Table.Rows[nextId].Field<TimeOnly>("TriggerTime");

        public ScheduleSequence()
        {
            Table = new DataTable();
            Table.Columns.Add(new DataColumn("Id", typeof(int)) //ID
            {
                Caption = "ID"
            });
            Table.Columns.Add(new DataColumn("Enable", typeof(bool)) //使能
            {
                Caption = "Enable"
            });
            Table.Columns.Add(new DataColumn("ScheduleCode", typeof(string)) //定时组编号
            {
                Caption = "Schedule Code"
            });
            Table.Columns.Add(new DataColumn("ScheduleName", typeof(string)) //定时组名称
            {
                Caption = "Schedule Name"
            });
            Table.Columns.Add(new DataColumn("TriggerTime", typeof(TimeOnlyHM)) //触发时间
            {
                Caption = "Trigger Time"
            });
            Table.Columns.Add(new DataColumn("TargetType", typeof(KnxObjectPart)) //目标类型
            {
                Caption = "Target Type"
            });
            Table.Columns.Add(new DataColumn("TargetValue", typeof(object)) //目标值
            {
                Caption = "Target Value"
            });
            Table.Columns.Add(new DataColumn("InterfaceCode", typeof(string)) //接口编号
            {
                Caption = "Interface"
            });
            Table.Columns.Add(new DataColumn("GroupAddress", typeof(GroupAddress)) //组地址
            {
                Caption = "Group Address"
            });
            Table.Columns.Add(new DataColumn("GroupDpt", typeof(string)) //DPT
            {
                Caption = "DPT"
            });
            Table.Columns.Add(new DataColumn("Value", typeof(GroupValue)) //KNX值
            {
                Caption = "Group Value"
            });
            Table.Columns.Add(new DataColumn("Triggered", typeof(bool)) //已触发
            {
                Caption = "Triggered"
            });
            //初始化部分在KnxSystem类里完成
        }

    }

}
