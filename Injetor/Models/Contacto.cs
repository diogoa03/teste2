using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Injetor.Models {
    public class Contacto {
        private string _conexao = "";

        protected int _idContacto;
        private string _nome;
        private string _telefone;

        public int IdContacto {
            get { return _idContacto; }
            set {
                if (value != 0) {
                    _idContacto = value;
                }
            }
        }

        public string Nome {
            get { return _nome; }
            set {
                _nome = value;
                if (_nome.Length > 128) _nome = _nome.Substring(0, 128);
            }
        }

        public string Telefone {
            get { return _telefone; }
            set {
                _telefone = value;
                if (_telefone.Length > 20) _telefone = _telefone.Substring(0, 20);
            }
        }

        public Contacto(string conexao) {
            _conexao = conexao;
            Nome = "";
            Telefone = "";
        }

        public int Criar() {
            int retID = 0;
            try {
                SqlCommand sqlC = new SqlCommand();
                SqlParameter parmOut = new SqlParameter();
                parmOut.Direction = ParameterDirection.Output;
                parmOut.ParameterName = "@idContactoOut";
                parmOut.SqlDbType = SqlDbType.Int;
                parmOut.Value = 0;
                //----
                sqlC.Connection = new SqlConnection(_conexao);
                sqlC.Connection.Open();
                sqlC.CommandType = CommandType.StoredProcedure;
                sqlC.CommandText = "QContactoCriar";
                sqlC.Parameters.Add("@nome", SqlDbType.VarChar, 128).Value = Nome;
                sqlC.Parameters.Add("@telefone", SqlDbType.VarChar, 20).Value = Telefone;
                sqlC.Parameters.Add(parmOut);

                sqlC.ExecuteNonQuery();

                retID = Convert.ToInt32(parmOut.Value);
                sqlC.Connection.Close();
                sqlC.Connection.Dispose();
            }
            catch (Exception ex) {
                Console.WriteLine("ERRO: " + ex.Message);   //Nota--> Escrever em log de erros e não na console
                retID = 0;
            }
            return retID;
        }
    }
}