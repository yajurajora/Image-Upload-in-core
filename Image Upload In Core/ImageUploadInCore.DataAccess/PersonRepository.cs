using Dapper;
using ImageUploadInCore.Common;
using ImageUploadInCore.Common.Abstraction;
using ImageUploadInCore.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageUploadInCore.DataAccess
{
    public class PersonRepository : IPersonRepository
    {
        public  IConfiguration _configuration;
        public string connection;
        public PersonRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connection = configuration["ConnectionString:BankDetailsDbConnection"];
        }
        public async Task<Persons> AddAsync(Persons persons)
        {
            using (IDbConnection con = new SqlConnection(connection))
            { 
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@FirstName", persons.FirstName);
            queryParameters.Add("@LastName", persons.LastName);
            queryParameters.Add("@Photo", persons.Photo);
            queryParameters.Add("@Gender", persons.Gender);
            queryParameters.Add("@DateOfBirth", persons.DateOfBirth);
            queryParameters.Add("@Age", persons.Age);
            queryParameters.Add("@IncomePerMonth", persons.IncomePerMonth);
            await con.QueryAsync<int>(Procedure.addPerson, queryParameters, commandType: System.Data.CommandType.StoredProcedure);
            return persons;
        }
        }

        public async Task<bool> DeleteAsync(Persons persons)
        {
            using (IDbConnection con = new SqlConnection(connection))
            {
                var param = new DynamicParameters();
                param.Add("@PID", persons.PID);
                var result = await con.ExecuteAsync(Procedure.DeletePerson, param, commandType: System.Data.CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public async Task<ICollection<Persons>> GetAllAsync()
        {
            using (IDbConnection con = new SqlConnection(connection))
            {
                var result = await con.QueryAsync<Persons>(Procedure.GetPerson, commandType: System.Data.CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<Persons> GetByIdAsync(string personId)
        {
            using (IDbConnection con = new SqlConnection(connection))
            {
                var param = new DynamicParameters();
                param.Add("@PID", personId);
                var result = await con.QueryAsync<Persons>(Procedure.GetPersonById, param, commandType: System.Data.CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }

        public async Task<Persons> UpdateAsync(Persons persons, string id)
        {
            using (IDbConnection con = new SqlConnection(connection))
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@PID", persons.PID);
                queryParameters.Add("@FirstName", persons.FirstName);
                queryParameters.Add("@LastName", persons.LastName);
                queryParameters.Add("@Photo", persons.Photo);
                queryParameters.Add("@Gender", persons.Gender);
                queryParameters.Add("@DateOfBirth", persons.DateOfBirth);
                queryParameters.Add("@Age", persons.Age);
                queryParameters.Add("@IncomePerMonth", persons.IncomePerMonth);
                await con.QueryAsync<int>(Procedure.UpdatePerson, queryParameters, commandType: System.Data.CommandType.StoredProcedure);
                return persons;
            }
        }
    }
}
