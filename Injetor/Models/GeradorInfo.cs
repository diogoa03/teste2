using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Injetor.Models {
    public class GeradorInfo {

        Random r;

        public GeradorInfo() {
            r = new Random();
        }

        public string geraNomeCompleto() {
            int tamanho = r.Next(2, 8);
            int contador = 0;
            string nomeCompleto = "";
            while (contador < tamanho) {
                nomeCompleto += " " + geraNomeSimples(r.Next(3, 8));
                contador++;
            }
            return nomeCompleto;
        }
        private string geraNomeSimples(int comprimento) {
            string[] consoantes = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vogais = { "a", "e", "i", "o", "u", "ão", "ou", "io", "ui" };
            string Name = "";
            Name += consoantes[r.Next(consoantes.Length)].ToUpper();
            Name += vogais[r.Next(vogais.Length)];
            int b = 2;
            while (b < comprimento) {
                Name += consoantes[r.Next(consoantes.Length)];
                b++;
                Name += vogais[r.Next(vogais.Length)];
                b++;
            }
            return Name;
        }

        public string GeraTelefone() {
            return r.Next(100000000, 999999999).ToString();
        }

        public string GeraEspecialidade() {
            string[] espec = { "Otorrino", "Oftalmologista", "Médico Família", "Neurocirurgião", "Ortopedista", "Psiquiatra", "", "", "" };
            return espec[r.Next(0, 5)];
        }

        public string GeraNrOrdem() {
            return r.Next(1000, 9999).ToString();
        }

        public string GeraNSS() {
            return r.Next(10000000, 99999999).ToString();
        }
    }
}