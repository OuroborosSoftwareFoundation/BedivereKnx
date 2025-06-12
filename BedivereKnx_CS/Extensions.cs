using System.ComponentModel;
using System.Data;

namespace BedivereKnx
{

    public static class DataTableExtensions
    {

        /// <summary>
        /// Creates and adds a System.Data.DataColumn object that has the specified name and type to the System.Data.DataColumnCollection.
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="columnName">The System.Data.DataColumn.ColumnName to use when you create the column.</param>
        /// <param name="caption">The System.Data.DataColumn.Caption of the new column.</param>
        /// <param name="dataType">The System.Data.DataColumn.DataType of the new column.</param>
        /// <returns>The newly created System.Data.DataColumn.</returns>
        public static DataColumn Add(this DataColumnCollection columns, string columnName, string caption, Type dataType)
        {
            DataColumn column = new(columnName, dataType)
            {
                Caption = caption
            };
            columns.Add(column);
            return column;
        }

    }

    public static class EnumExtensions
    {

        //internal static KnxObjectType FromString(this KnxObjectType objType, string value)
        //{
        //    if (string.IsNullOrWhiteSpace(value))
        //        throw new NoNullAllowedException(string.Format(ResString.ExMsg_NoNullAllowed, "ObjectCode", $"Id={dr["Id"]}"));

        //}

    }

}
