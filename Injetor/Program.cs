namespace Injetor
{
    internal class Program
    {
        private static string con = "SERVER=DESKTOP-5R6FFRV; Database=istecDB; uid=istecUser; pwd=istecUser;TrustServerCertificate=True;";

        static void Main(string[] args)
        {
            DateTime StartMedico = DateTime.Now;
            int ret = 0;
            GeradorInfo gi = new GeradorInfo();
            for (int contador = 0; contador < 1000; contador++) {
                Medico m = new Medico(con);
                m.Nome = gi.geraNomeCompleto();
                m.Telefone = gi.GeraTelefone(); ;
                m.NrOrdem = gi.GeraNrOrdem();
                m.Especialidade = gi.GeraEspecialidade();
                ret = m.Criar();
                Console.WriteLine("ID Medico:" + ret);
            }
            DateTime EndMedico = DateTime.Now;
            Console.WriteLine("Demorou " + (EndMedico - StartMedico).TotalSeconds + " a criar 1000 Médicos");
            //----------------------------------------------------------------------------------------------------
            //Teste Leitura Aleatoria
            //----------------------------------------------------------------------------------------------------
            DateTime StartRandomMedico = DateTime.Now;
            TesterMedico tm = new TesterMedico(con);
            List<Medico> randMedico = tm.GetRandom10PercentMedico();
            Console.WriteLine("\n---------------------\nLista de Médicos\n-------------------\n");
            foreach (Medico m in randMedico) {
                Console.WriteLine($"Nome: {m.Nome}; Especialidade: {m.Especialidade}\n");
            }
            DateTime EndRandomMedico = DateTime.Now;
            Console.WriteLine("Demorou " + (EndRandomMedico - StartRandomMedico).TotalSeconds + $" a ler 10% ({randMedico.Count} Registos) de Médicos de forma aleatória");
            //----------------------------------------------------------------------------------------------------
            //Teste Update Aleatorio
            //----------------------------------------------------------------------------------------------------
            DateTime StartUpdateMedico = DateTime.Now;
            foreach (Medico m in randMedico) {
                m.Nome = gi.geraNomeCompleto();
                m.Especialidade = gi.GeraEspecialidade();
                m.Atualizar();
                //Console.WriteLine($"Nome: {m.Nome}; Especialidade: {m.Especialidade}\n");
            }
            DateTime EndUpdateMedico = DateTime.Now;
            Console.WriteLine("Demorou " + (EndUpdateMedico - StartUpdateMedico).TotalSeconds + $" a atualizar os 10% ({randMedico.Count} Registos) de Médicos lidos de forma aleatória");
            //----------------------------------------------------------------------------------------------------
            Console.ReadLine();

        }
    }

}
