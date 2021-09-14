﻿using Dapper;
using PAV_3K5_TP.Negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAV_3K5_TP.Datos
{
    public class UsuarioDao
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public UsuarioDao(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public Usuario obtenerPorUsernameYPassword(string usuario, string password)
        {
            string sql = @"SELECT ID_PERFIL, USUARIO, PASSWORD, EMAIL, ESTADO, BORRADO ";
            sql += "FROM USUARIOS ";
            sql += "WHERE USUARIO = @usuario AND PASSWORD = @password";

            return _connection.QueryFirstOrDefault<Usuario>(sql, new { usuario=usuario, password=password } , transaction: _transaction);
        }
    }
}
