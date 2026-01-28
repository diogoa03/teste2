using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Injetor.Models {
    public class Medico : Contacto {

        private string _conexao = "";
        protected int _idMedico;
        private string _nrOrdem;
        private string _especialidade;

        public int IdMedico {
            get { return _idMedico; }
            set {
                if (value != 0) {
                    _idMedico = value;
                }
            }
        }

        public string NrOrdem {
            get { return _nrOrdem; }
            set {
                _nrOrdem = value;
                if (_nrOrdem.Length > 20) _nrOrdem = _nrOrdem.Substring(0, 20);
            }
        }

        public string Especialidade {
            get { return _especialidade; }
            set {
                _especialidade = value;
                if (_especialidade.Length > 50) _especialidade = _especialidade.Substring(0, 50);
            }
        }
        public Medico(string con) : base(con) {
            _conexao = con;
            Especialidade = "";
            NrOrdem = "";
        }

        public int Criar() {
            int retID = 0;
            int retIdContacto = base.Criar();
            if (retIdContacto > 0) {
                try {
                    SqlCommand sqlC = new SqlCommand();
                    SqlParameter parmOut = new SqlParameter();
                    parmOut.Direction = ParameterDirection.Output;
                    parmOut.ParameterName = "@idMedicoOut";
                    parmOut.SqlDbType = SqlDbType.Int;
                    parmOut.Value = 0;
                    //----
                    sqlC.Connection = new SqlConnection(_conexao);
                    sqlC.Connection.Open();
                    sqlC.CommandType = CommandType.StoredProcedure;
                    sqlC.CommandText = "QMedicoCriar";
                    sqlC.Parameters.Add("@idContacto", SqlDbType.Int).Value = retIdContacto;
                    sqlC.Parameters.Add("@nrOrdem", SqlDbType.VarChar, 20).Value = NrOrdem;
                    sqlC.Parameters.Add("@especialidade", SqlDbType.VarChar, 50).Value = Especialidade;
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
            }
            return retID;
        }

        public int Atualizar() {        //Versão Estapafúrdia.... Qual seria a versão mais correta?
            int retID = 0;
            int retIdContacto = base.Criar();
            if (retIdContacto > 0) {
                try {
                    SqlCommand sqlC = new SqlCommand();
                    SqlParameter parmOut = new SqlParameter();
                    parmOut.Direction = ParameterDirection.Output;
                    parmOut.ParameterName = "@idMedicoOut";
                    parmOut.SqlDbType = SqlDbType.Int;
                    parmOut.Value = 0;
                    //----
                    sqlC.Connection = new SqlConnection(_conexao);
                    sqlC.Connection.Open();
                    sqlC.CommandType = CommandType.StoredProcedure;
                    sqlC.CommandText = "QMedicoUpdate";
                    sqlC.Parameters.Add("@idContacto", SqlDbType.Int).Value = base.IdContacto;
                    sqlC.Parameters.Add("@idMedico", SqlDbType.Int).Value = IdMedico;
                    sqlC.Parameters.Add("@nome", SqlDbType.VarChar, 128).Value = base.Nome;
                    sqlC.Parameters.Add("@telefone", SqlDbType.VarChar, 20).Value = base.Telefone;
                    sqlC.Parameters.Add("@nrOrdem", SqlDbType.VarChar, 20).Value = NrOrdem;
                    sqlC.Parameters.Add("@especialidade", SqlDbType.VarChar, 50).Value = Especialidade;
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
            }
            return retID;
        }
    }
}