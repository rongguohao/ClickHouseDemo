using FreeSql.Internal.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SinoFramework.ClickHouse
{
    public class ClickHouseAdapter : FreeSql.Custom.CustomAdapter
    {
        public override char QuoteSqlNameLeft => '`';

        public override char QuoteSqlNameRight => '`';

        public override string UnicodeStringRawSql(object value, ColumnInfo mapColumn)
        {
            return value == null ? "NULL" : string.Concat("'", value.ToString().Replace("'", "''"), "'");
        }
    }
}
