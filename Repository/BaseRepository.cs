using Common.CommonMethods;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Model.AppSettingsJason;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class BaseRepository
    {
        public readonly  INonStaticCommonMethods NonStaticCommonMethods;
        public BaseRepository(INonStaticCommonMethods NonStaticCommonMethods)
        {
            this.NonStaticCommonMethods = NonStaticCommonMethods;
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (SqlConnection con = new SqlConnection(NonStaticCommonMethods.GetConfigurationValue(AppSettingsJason.AppSettings.ConnectionString)))
            {
                await con.OpenAsync();
                return await con.QueryFirstOrDefaultAsync<T>(sql, param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (SqlConnection con = new SqlConnection(NonStaticCommonMethods.GetConfigurationValue(AppSettingsJason.AppSettings.ConnectionString)))
            {
                await con.OpenAsync();
                return await con.QueryAsync<T>(sql, param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<object> ExecuteScalarAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (SqlConnection con = new SqlConnection(NonStaticCommonMethods.GetConfigurationValue(AppSettingsJason.AppSettings.ConnectionString)))
            {
                await con.OpenAsync();
                return await con.ExecuteScalarAsync<object>(sql, param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> ExecuteAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (SqlConnection con = new SqlConnection(NonStaticCommonMethods.GetConfigurationValue(AppSettingsJason.AppSettings.ConnectionString)))
            {
                await con.OpenAsync();
                return await con.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<SqlMapper.GridReader> QueryMultipleAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (SqlConnection con = new SqlConnection(NonStaticCommonMethods.GetConfigurationValue(AppSettingsJason.AppSettings.ConnectionString)))
            {
                await con.OpenAsync();
                return await con.QueryMultipleAsync(sql, param, commandType: CommandType.StoredProcedure);
            }
        }
    }
}

