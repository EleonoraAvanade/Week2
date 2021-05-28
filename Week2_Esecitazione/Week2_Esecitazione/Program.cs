using System;
using System.IO;
using System.Collections;
using System.Globalization;

namespace Week2_Esecitazione
{
    class Task
    {
        public int id { get; set; }
        string descrizione;
        DateTime scadenza;
        byte importanza;
        public Task() { Random rand = new Random(); id = rand.Next(1, 10000); } 
        public string getDes() { return descrizione; }
        public void setDes(string d) { descrizione = d; }
        public DateTime getSc() { return scadenza; }
        public void setSc(DateTime d) { scadenza = d; }
        public byte getImportanza() { return importanza; }
        public void setImportanza(byte d) { importanza = d; }
        public override string ToString()
        {
            Console.WriteLine("Descrizione: {0}\nScadenza: {1}\nImportanza: {2}\n", descrizione, scadenza, importanza);
            return null;
        }
        public void writeInAFIle()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+"Tasks.txt";
            if (!File.Exists(path))
            {
                using(StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("Id: {3}\nDescrizione: {0}\nScadenza: {1}\nImportanza: {2}\n", descrizione, scadenza, importanza,id);
                }
            }
        }
        //public void deleteItFromAFile()
        //{
        //    string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "Tasks.txt";
        //    if (!File.Exists(path)) { Console.WriteLine("File inesistente\n"); return; }
        //    using(StreamReader sr = File.OpenText(path))
        //    {
        //        string s = sr.ReadLine();
        //        if (s == ("id: "+id.ToString()))
        //        {

        //        }
        //    }
        //}
    }
    class Program
    {
        static void Main(string[] args)
        {
            ArrayList Tasks = new ArrayList();
            Console.WriteLine("Ecco gestione Task, puoi:\n");
            while (true) {
                int choice = 0;
                do
                {
                    Console.WriteLine(
                    "1 - Vedere i task inseriti\n" +
                    "2 - Aggiungere un nuovo task\n" +
                    "3 - Eliminare un task\n" +
                    "4 - Filtrare i task per importanza\n\n" +
                    "Per uscire premere un qualunque altro NUMERO\n");
                }
                while (!Int32.TryParse(Console.ReadLine(), out choice));
                switch (choice)
                {
                    case 1:
                        foreach (var ob in Tasks)
                        {
                            ((Task)ob).ToString();
                            ((Task)ob).writeInAFIle();
                        }
                        break;
                    case 2:
                        Task obj = new Task();
                        Console.WriteLine("Inserisci una descrizione e premi invio:\n");
                        obj.setDes(Console.ReadLine());
                        Console.WriteLine("Inserisci una scadenza (gg/mm/YY) e premi invio:\n");
                        var cultureInfo = new CultureInfo("en-US");
                        DateTime dt=DateTime.Parse("1/1/1111");
                        try
                        {
                            dt= DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", cultureInfo, DateTimeStyles.AdjustToUniversal);
                            while (DateTime.Now >= dt)
                            {
                                Console.WriteLine("Immetti una data futura\n");
                                Console.WriteLine("Inserisci una scadenza (gg/mm/YY) e premi invio:\n");
                                dt = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", cultureInfo, DateTimeStyles.AdjustToUniversal);
                            }
                        }
                        catch(FormatException e)
                        {
                            Console.WriteLine("Immetti il formato corretto (gg/mm/yyyy)" + e.Message+ "\n");
                        }
                        obj.setSc(dt);
                        int tmp=1;
                        
                            Console.WriteLine("Inserisci un' importanza tra 1(basso), 2(medio), 3(alto) e premi invio:\n");
                            Int32.TryParse( Console.ReadLine(), out tmp);
                        obj.setImportanza((byte)tmp);
                        Tasks.Add(obj);
                        Console.WriteLine("Oggetto aggiunto con id: {0}\n", obj.id);
                        break;
                    case 3:
                        Console.WriteLine("Immetti l'id del Task: \n");
                        string s = Console.ReadLine();
                        bool here = false;
                        foreach (var i in Tasks)
                        {
                            here = false;
                            Int32.TryParse(s, out int temp);
                            if (((Task)i).id == temp) here = true;
                            if (here)
                            {
                                Tasks.Remove(i);
                                Console.WriteLine("Oggetto eliminato\n");
                                break;
                            }
                        }
                        if (!here) Console.WriteLine("Non esiste Task con questo id. \n");
                        break;
                    case 4:
                        //////PENSAVO SI DOVESSERO ORDINARE
                        //int max = 0;
                        //for (int index = 0; index < Tasks.Count; index++) {
                        //    max = index;
                        //    for (int j = index + 1; j < Tasks.Count; j++)
                        //    {
                        //        if (((Task)Tasks[index]).getImportanza() < ((Task)Tasks[j]).getImportanza()) max = j;
                        //    }
                        //    Object tempor = Tasks[index];
                        //    Tasks[index] = Tasks[max];
                        //    Tasks[max] = tempor;
                        //}
                        //foreach (var ogg in Tasks)
                        //{
                        //    ogg.ToString();
                        //}
                        int tmp1 = 1;
                            Console.WriteLine("Inserisci un' importanza tra alto, medio, basso e premi invio:\n");
                        Int32.TryParse(Console.ReadLine(), out tmp1);
                        foreach (var im in Tasks)
                        {
                            if (((Task)im).getImportanza() == tmp1) ((Task)im).ToString();
                        }
                        break;
                    default:
                        Console.WriteLine("Quit...");
                        return;
                }
            }
        }
    }
}
