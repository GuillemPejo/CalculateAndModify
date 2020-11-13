using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ConsoleApp3
{
    class Ex1
    {
        static void Main(string[] args)
        {
            //CREACIO DEL DIRECTORI
            string ruta = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\ExerciciParaules\";
            if (!Directory.Exists(ruta))
            {
                Directory.CreateDirectory(ruta);
            }
            // CALCUL
            string fitxerdentrada_1 = ruta + @"input.txt";
            string fitxerdentrada_2 = ruta + @"paraulesCensurades.txt";
            string fitxerdesortida_1 = ruta + @"ouput.txt";
            string text = System.IO.File.ReadAllText(fitxerdentrada_2).ToLower();
            string[] paraules_censurades = text.Split(new char[] { ' ', ',', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            //CALCUL NUMERO DE PARAULES
            StreamReader fe_1 = new StreamReader(fitxerdentrada_1);
            int num_paraules = 0;
            string puntuacio = " ,.";
            string linia = null;
            while (!fe_1.EndOfStream)
            {
                linia = fe_1.ReadLine();
                linia.Trim();
                num_paraules += linia.Split(puntuacio.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length;
            }
            fe_1.Close();

            //CALCUL NUMERO DE PARÀGRAFS
            
            StreamReader fe_2 = new StreamReader(fitxerdentrada_1);
            string[] num_paragrafs;
            linia = null;
            string pattern = @"[^\r\n]+((\r|\n|\r\n)[^\r\n]+)*";
            Regex rgx = new Regex(pattern);
            while (!fe_2.EndOfStream)
            {
                linia = fe_2.ReadLine();

                foreach (Match match in rgx.Matches(linia))
                    num_paragrafs = match.Value;

            }  




            string nom = Path.GetFileNameWithoutExtension(fitxerdentrada_1);

            //ESCRIPTURA PRIMER FITXER
            //CANVI DE PARAULES
            System.IO.File.Copy(fitxerdentrada_1, fitxerdesortida_1, true);
            for (int i = 0; i < paraules_censurades.Length; i++)
            {
                File.WriteAllText(fitxerdesortida_1, File.ReadAllText(fitxerdesortida_1).Replace(paraules_censurades[i], "[CENSURED]"));
            }

            //ESCRIPTURA SEGON FITXER
            StreamWriter fitxerdesortida_2 = new StreamWriter(ruta + "info.txt");
            fitxerdesortida_2.WriteLine("-----INFO DOCUMENT-----");
            fitxerdesortida_2.WriteLine("");
            fitxerdesortida_2.WriteLine("Nom del fitxer: " + nom);
            fitxerdesortida_2.WriteLine("Nombre de paraules: " + num_paraules);
            fitxerdesortida_2.WriteLine("Nombre de paràgrafs: " + num_paragrafs);
            fitxerdesortida_2.Close();

        }
    }
}
