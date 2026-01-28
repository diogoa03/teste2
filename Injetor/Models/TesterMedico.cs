using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Injetor.Models {
    public class TesterMedico {
        private string _conexao = "";

        public TesterMedico(string conexao) {
            _conexao = conexao;
        }

        public List<Medico> GetRandom10PercentMedico() {
            List<Medico> retList = new List<Medico>();
            DataTable dtC = new DataTable();
            SqlDataAdapter SqlA = new SqlDataAdapter();
            //--
            try {
                SqlA.SelectCommand = new SqlCommand();
                SqlA.SelectCommand.Connection = new SqlConnection(_conexao);
                SqlA.SelectCommand.Connection.Open();
                SqlA.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlA.SelectCommand.CommandText = "QTester_GetTop10Percent_Medico";
                //---
                SqlA.Fill(dtC);
                //--- 
                SqlA.SelectCommand.Connection.Close();
                SqlA.SelectCommand.Connection.Dispose();
            }
            catch (Exception ex) {
                dtC = null;
            }
            if (dtC != null) {
                foreach (DataRow r in dtC.Rows) {
                    Medico m = new Medico(_conexao);
                    m.IdContacto = Convert.ToInt32(r["idContacto"]);
                    m.IdMedico = Convert.ToInt32(r["idMedico"]);
                    m.Nome = r["nome"].ToString();
                    m.Telefone = r["telefone"].ToString();
                    m.Especialidade = r["especialidade"].ToString();
                    m.NrOrdem = r["nrOrdem"].ToString();
                    retList.Add(m);
                }
            }
            return retList;
        }
    }
}