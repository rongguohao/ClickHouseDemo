using FreeSql;
using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class FreeSqlExtensions
    {
        public static string ToClickHouseUpdateSql<T>(this IUpdate<T> update)
        {
            string sql = update.ToSql();
            var idx = sql.IndexOf(" SET ");
            var sb = new StringBuilder();
            sb.Append("ALTER TABLE ")
                .Append(sql.Remove(idx).Replace("UPDATE ", ""))
                .Append(" UPDATE ").Append(sql.Substring(idx+5));
            return sb.ToString();
        }

        public static string ToClickHouseDeleteSql<T>(this IDelete<T> delete)
        {
            string sql = delete.ToSql();
            var idx = sql.IndexOf(" WHERE ");
            var sb = new StringBuilder();
            sb.Append("ALTER TABLE ")
                .Append(sql.Remove(idx).Replace("DELETE FROM ", ""))
                .Append(" DELETE WHERE ").Append(sql.Substring(idx+7));
            return sb.ToString();
        }
    }
}
