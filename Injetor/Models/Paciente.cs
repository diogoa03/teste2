using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Injetor.Models {
    public class Paciente : Contacto {

        private string _conexao = "";

        private string _nrSS;

        public string NrSS {
            get { return _nrSS; }
            set {
                _nrSS = value;
                if (_nrSS.Length > 20) _nrSS = _nrSS.Substring(0, 20);
            }
        }

        public Paciente(string con) : base(con) {
            _conexao = con;
            NrSS = "";
        }

        public int Criar() {
            int retID = 0;
            int retIdContacto = base.Criar();
            if (retIdContacto > 0) {
                try {
                    SqlCommand sqlC = new SqlCommand();
                    SqlParameter parmOut = new SqlParameter();
                    parmOut.Direction = ParameterDirection.Output;
                    parmOut.ParameterName = "@idPacienteOut";
                    parmOut.SqlDbType = SqlDbType.Int;
                    parmOut.Value = 0;
                    //----
                    sqlC.Connection = new SqlConnection(_conexao);
                    sqlC.Connection.Open();
                    sqlC.CommandType = CommandType.StoredProcedure;
                    sqlC.CommandText = "QPacienteCriar";
                    sqlC.Parameters.Add("@idContacto", SqlDbType.Int).Value = retIdContacto;
                    sqlC.Parameters.Add("@nrSS", SqlDbType.VarChar, 20).Value = NrSS;
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