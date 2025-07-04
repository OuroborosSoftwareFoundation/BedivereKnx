using System.Globalization;

namespace BedivereKnx
{

    /// <summary>
    /// 只包含时和分的数据结构
    /// </summary>
    public struct TimeOnlyHM
    {

        /// <summary>
        /// 总共分钟数
        /// </summary>
        private readonly int _totalMinutes;

        /// <summary>
        /// 小时
        /// </summary>
        public readonly int Hour => _totalMinutes / 60;

        /// <summary>
        /// 分钟
        /// </summary>
        public readonly int Minute => _totalMinutes % 60;

        public TimeOnlyHM(int hour, int minute)
        {
            if (hour < 0 || hour > 23)
                throw new ArgumentOutOfRangeException(nameof(hour));
            if (minute < 0 || minute > 59)
                throw new ArgumentOutOfRangeException(nameof(minute));

            _totalMinutes = hour * 60 + minute;
        }

        public TimeOnlyHM(DateTime time)
            : this(time.Hour, time.Minute)
        { }

        public TimeOnlyHM(string timeText)
            : this(Convert.ToDateTime(timeText))
        { }

        //【以下内容为AI生成】

        // 显式/隐式转换实现
        public static implicit operator TimeOnly(TimeOnlyHM time) => new TimeOnlyHM(time.Hour, time.Minute);
        public static implicit operator TimeOnlyHM(TimeOnly time) => new TimeOnlyHM(time.Hour, time.Minute);
        public static implicit operator TimeSpan(TimeOnlyHM time) => new TimeSpan(time.Hour, time.Minute, 0);
        public static implicit operator TimeOnlyHM(TimeSpan span) => new TimeOnlyHM(span.Hours, span.Minutes);
        public static explicit operator DateTime(TimeOnlyHM time) => DateTime.Today.AddMinutes(time._totalMinutes);
        public static explicit operator TimeOnlyHM(DateTime dt) => new TimeOnlyHM(dt.Hour, dt.Minute);

        // 比较方法
        public int CompareTo(TimeOnlyHM other) => _totalMinutes.CompareTo(other._totalMinutes);
        public int CompareTo(object? obj) => obj is TimeOnlyHM other ? CompareTo(other) : throw new ArgumentException();

        // 相等性比较
        public bool Equals(TimeOnlyHM other) => _totalMinutes == other._totalMinutes;
        public override bool Equals(object? obj) => obj is TimeOnlyHM other && Equals(other);
        public override int GetHashCode() => _totalMinutes.GetHashCode();

        // 运算符重载
        public static bool operator ==(TimeOnlyHM left, TimeOnlyHM right) => left.Equals(right);
        public static bool operator !=(TimeOnlyHM left, TimeOnlyHM right) => !left.Equals(right);
        public static bool operator <(TimeOnlyHM left, TimeOnlyHM right) => left._totalMinutes < right._totalMinutes;
        public static bool operator >(TimeOnlyHM left, TimeOnlyHM right) => left._totalMinutes > right._totalMinutes;
        public static bool operator <=(TimeOnlyHM left, TimeOnlyHM right) => left._totalMinutes <= right._totalMinutes;
        public static bool operator >=(TimeOnlyHM left, TimeOnlyHM right) => left._totalMinutes >= right._totalMinutes;

        // 时间加减
        public TimeOnlyHM AddHours(int hours) => new TimeOnlyHM((Hour + hours) % 24, Minute);
        public TimeOnlyHM AddMinutes(int minutes)
        {
            var total = _totalMinutes + minutes;
            return new TimeOnlyHM((total / 60) % 24, total % 60);
        }

        // 格式化输出
        public string ToString(string? format, IFormatProvider? formatProvider) =>
            DateTime.MinValue.AddHours(Hour).AddMinutes(Minute).ToString(format, formatProvider);

        public override string ToString() => $"{Hour:D2}:{Minute:D2}";

        // 解析方法
        public static TimeOnlyHM Parse(string s) => Parse(s, null);
        public static TimeOnlyHM Parse(string s, IFormatProvider? provider)
        {
            if (DateTime.TryParseExact(s, new[] { "HH:mm", "H:mm" }, provider, DateTimeStyles.None, out var dt))
                return new TimeOnlyHM(dt.Hour, dt.Minute);
            throw new FormatException();
        }

        public static bool TryParse(string? s, out TimeOnlyHM result) => TryParse(s, null, out result);
        public static bool TryParse(string? s, IFormatProvider? provider, out TimeOnlyHM result)
        {
            result = default;
            if (s == null) return false;
            return DateTime.TryParseExact(s, new[] { "HH:mm", "H:mm" }, provider, DateTimeStyles.None, out var dt)
                ? (result = new TimeOnlyHM(dt.Hour, dt.Minute), true).Item2
                : false;
        }



    }

}
