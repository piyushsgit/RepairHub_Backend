using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Model;
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
        public readonly IOptions<AppSettings> _connectionString;
        public BaseRepository(IOptions<AppSettings> connection)
        {
            _connectionString = connection;
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (SqlConnection con = new SqlConnection(_connectionString.Value.DatabaseConnection))
            {
                await con.OpenAsync();
                return await con.QueryFirstOrDefaultAsync<T>(sql, param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (SqlConnection con = new SqlConnection(_connectionString.Value.DatabaseConnection))
            {
                await con.OpenAsync();
                return await con.QueryAsync<T>(sql, param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<object> ExecuteScalarAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (SqlConnection con = new SqlConnection(_connectionString.Value.DatabaseConnection))
            {
                await con.OpenAsync();
                return await con.ExecuteScalarAsync<object>(sql, param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> ExecuteAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (SqlConnection con = new SqlConnection(_connectionString.Value.DatabaseConnection))
            {
                await con.OpenAsync();
                return await con.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<SqlMapper.GridReader> QueryMultipleAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (SqlConnection con = new SqlConnection(_connectionString.Value.DatabaseConnection))
            {
                await con.OpenAsync();
                return await con.QueryMultipleAsync(sql, param, commandType: CommandType.StoredProcedure);
            }
        }
    }
}

