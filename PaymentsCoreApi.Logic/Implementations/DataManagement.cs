using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using PaymentsCoreApi.Data.Contexts;
using PaymentsCoreApi.Domain.Dtos;
using PaymentsCoreApi.Logic.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace PaymentsCoreApi.Logic.Implementations
{
    public class DataManagement:IDataManagement
    {
        private DataBaseContext _dataBaseContext;
        private ICommonLogic _commonLogic;
        public DataManagement(DataBaseContext dataBaseContext, ICommonLogic commonLogic)
        {
            _dataBaseContext = dataBaseContext;
            _commonLogic = commonLogic;
        }

        public async Task<DataTable> InternalExecuteQuery(QueryRequestDto request)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_dataBaseContext.Database.GetConnectionString()))
                {
                    using (MySqlCommand command = new MySqlCommand(request.StoredProcedure, connection))
                    {
                        try
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            foreach (DictionaryEntry item in request.Parameters)
                            {
                                MySqlParameter parameter = new MySqlParameter(item.Key.ToString(), item.Value.ToString());
                                command.Parameters.Add(parameter);
                            }
                            connection.Open();

                            using (MySqlDataAdapter sda = new MySqlDataAdapter(command))
                            {
                                DataTable dt = new DataTable();
                                sda.Fill(dt);
                                return dt;

                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<QueryResponseDto> ExecuteQuery(QueryRequestDto request)
        {
            try
            {
                var credentails = _dataBaseContext.Channel.Where(c => c.ChannelKey == request.Apikey).FirstOrDefault();
                if (credentails == null)
                    return new QueryResponseDto()
                    { ResponseCode = "100", ResponseMessage = "Api access denied", Data = "" };

                var inputstring = credentails.ChannelKey + credentails.ChannelSecretKey + request.StoredProcedure + request.RequestTimestamp;
                if (!_commonLogic.IsValidCredentails(request.Signature, inputstring))
                    return new QueryResponseDto()
                    { ResponseCode = "100", ResponseMessage = "Api access denied", Data = "" };

                using (MySqlConnection connection = new MySqlConnection(_dataBaseContext.Database.GetConnectionString()))
                {
                    using (MySqlCommand command = new MySqlCommand(request.StoredProcedure, connection))
                    {
                        try
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            foreach (DictionaryEntry item in request.Parameters)
                            {
                                MySqlParameter parameter = new MySqlParameter(item.Key.ToString(), item.Value.ToString());
                                command.Parameters.Add(parameter);
                            }
                            connection.Open();
                            using (MySqlDataAdapter sda = new MySqlDataAdapter(command))
                            {
                                var dt = new DataTable();
                                sda.Fill(dt);
                                return new QueryResponseDto()
                                {
                                    ResponseCode = "200",
                                    ResponseMessage = "Success",
                                    Data = JsonConvert.SerializeObject(dt)
                                };

                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new QueryResponseDto() { ResponseCode = "100", ResponseMessage = "Api access denied" };
            }
        }
        public async Task<QueryResponseDto> ExecuteNonQuery(QueryRequestDto request)
        {
            try
            {
                var credentails = _dataBaseContext.Channel.Where(c => c.ChannelKey == request.Apikey).FirstOrDefault();
                if (credentails == null)
                    return new QueryResponseDto()
                    { ResponseCode = "100", ResponseMessage = "Api access denied", Data = "" };

                var inputstring = credentails.ChannelKey + credentails.ChannelSecretKey + request.StoredProcedure + request.RequestTimestamp;
                if (!_commonLogic.IsValidCredentails(request.Signature, inputstring))
                    return new QueryResponseDto()
                    { ResponseCode = "100", ResponseMessage = "Api access denied", Data = "" };
                using (MySqlConnection connection = new MySqlConnection(_dataBaseContext.Database.GetConnectionString()))
                {
                    using (MySqlCommand command = new MySqlCommand(request.StoredProcedure, connection))
                    {
                        try
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            foreach (DictionaryEntry item in request.Parameters)
                            {
                                MySqlParameter parameter = new MySqlParameter(item.Key.ToString(), item.Value.ToString());
                                command.Parameters.Add(parameter);
                            }
                            connection.Open();
                            command.ExecuteNonQuery();
                            return new QueryResponseDto()
                            {
                                ResponseCode = "200",
                                ResponseMessage = "Success",
                                Data = ""
                            };
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new QueryResponseDto() { ResponseCode = "100", ResponseMessage = "Api access denied", Data = ex.Message };
            }
        }
    }
}
