﻿using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;

namespace HouseWarehouseStore.Data.Dapper
{
    public class Dapperr : IDapper
    {
        private readonly IConfiguration _config;
        private string Connectionstring = "HouseWarehouseStoreDatabase";

        public Dapperr(IConfiguration config)
        {
            _config = config;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<T> GetAyncFirst<T>(string sp, DynamicParameters parms, CommandType commandType)
        {
            using var connection = new SqlConnection(_config.GetConnectionString(Connectionstring));
            return await connection.QueryFirstOrDefaultAsync<T>(sp, parms, commandType: commandType);
        }

        public async Task<IEnumerable<T>> GetAllAync<T>(string sp, DynamicParameters parms, CommandType commandType)
        {
            using var connection = new SqlConnection(_config.GetConnectionString(Connectionstring));
            return await connection.QueryAsync<T>(sp, parms, commandType: commandType);
        }

        public async Task<IEnumerable<T>> GetList<T>(string sp, DynamicParameters parms, CommandType commandType)
        {
            using var connection = new SqlConnection(_config.GetConnectionString(Connectionstring));
            return await connection.QueryAsync<T>(sp, parms, commandType: commandType);
        }

        public DbConnection GetDbconnection()
        {
            return new SqlConnection(_config.GetConnectionString(Connectionstring));
        }

        public IEnumerable<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using var connection = new SqlConnection(_config.GetConnectionString(Connectionstring));
            return connection.Query<T>(sp, parms, commandType: commandType).ToList();
        }

        public async Task<int> CheckName<T>(string name, string nameEntity)
        {
            using var connection = new SqlConnection(_config.GetConnectionString(Connectionstring));
            var sp = "select Id from Admins " + nameEntity + " where Username = @username";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@username", name);
            var res = await connection.QueryAsync<T>(sp, parameter, commandType: CommandType.Text);
            return res.Count();
        }
    }
}