Imports DocumentFormat.OpenXml.Office2010.ExcelAc
Imports Knx.Falcon.IoT.RobustOperation
Module mdlDataFile_EtsGa
    '    using System;
    'using System.Collections.Generic;
    'using System.Data;
    'using System.Linq;

    'class Program
    '{
    '    static void Main()
    '    {
    '        // 创建示例DataTable
    '        DataTable table = new DataTable();
    '        table.Columns.Add("Value", typeof(string));

    '        // 添加示例数据
    '        table.Rows.Add("123.A");
    '        table.Rows.Add("123.B");
    '        table.Rows.Add("123.C");
    '        table.Rows.Add("4542");
    '        table.Rows.Add("3457");
    '        table.Rows.Add("4542.X"); // 假设这样的数据也可能存在，虽然它不符合“小数点后”的严格定义，但逻辑上可处理

    '        // 创建一个字典来存储结果
    '        Dictionary<string, List<int>> result = new Dictionary<string, List<int>>();

    '        // 遍历DataTable并处理数据
    '        for (int i = 0; i < table.Rows.Count; i++)
    '        {
    '            string value = table.Rows[i]["Value"].ToString();
    '            string prefix = GetPrefix(value);

    '            if (!string.IsNullOrEmpty(prefix))
    '            {
    '                if (!result.ContainsKey(prefix))
    '                {
    '                    result[prefix] = new List<int>();
    '                }
    '                result[prefix].Add(i);
    '            }
    '        }

    '        // 过滤出小数点后部分不同的前缀（即至少有两行具有相同前缀）
    '        var filteredResult = result.Where(kvp => kvp.Value.Count > 1)
    '                                   .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToArray());

    '        // 输出结果
    '        foreach (var kvp in filteredResult)
    '        {
    '            Console.WriteLine($"Prefix: {kvp.Key}, Row Indices: [{string.Join(", ", kvp.Value)}]");
    '        }
    '    }

    '    // 提取小数点前的部分
    '    static string GetPrefix(string value)
    '    {
    '        int dotIndex = value.IndexOf('.');
    '        if (dotIndex > 0) // 确保有小数点且小数点不在开头
    '        {
    '            return value.Substring(0, dotIndex);
    '        }
    '        return null; // 如果没有小数点，返回null（可根据需求调整）
    '    }
    '}

End Module
