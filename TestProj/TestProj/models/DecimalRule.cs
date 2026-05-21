namespace TestProj.Models
{
    /// <summary>
    /// 一条批量精确位数规则：TagName 以 Prefix 开头的变量统一设为 DecimalPlaces 位。
    /// 规则按列表顺序匹配，第一条命中即生效。
    /// </summary>
    public class DecimalRule
    {
        public string Prefix        { get; set; } = "";
        public int    DecimalPlaces { get; set; } = 2;
    }
}
