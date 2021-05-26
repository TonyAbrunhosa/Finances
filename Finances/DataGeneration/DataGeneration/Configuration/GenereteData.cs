using DataGeneration.Entities;
using System;
using System.Collections.Generic;

namespace DataGeneration.Configuration
{
    public class GenereteData
    {
        private List<Ativos> ListAtivos { get; set; }

        public List<Ativos> ListarAtivos()
        {
            ListAtivos = new List<Ativos>();
            ListAtivos.Add(new Ativos("PETR4",25.00m));
            ListAtivos.Add(new Ativos("MGLU3", 20.08m));
            ListAtivos.Add(new Ativos("BIDI4", 68.94m));
            ListAtivos.Add(new Ativos("ITSA4", 10.77m));
            ListAtivos.Add(new Ativos("CASH3", 38.18m));
            ListAtivos.Add(new Ativos("BBDC4", 25.68m));
            ListAtivos.Add(new Ativos("VVAR3", 12.47m));
            ListAtivos.Add(new Ativos("AMAR3", 06.41m));
            ListAtivos.Add(new Ativos("HGTX3", 29.34m));
            ListAtivos.Add(new Ativos("ALPA3", 39.02m));
            ListAtivos.Add(new Ativos("CYRE3", 22.72m));
            ListAtivos.Add(new Ativos("GFSA3", 04.18m));
            ListAtivos.Add(new Ativos("MOVI3", 17.66m));
            ListAtivos.Add(new Ativos("RENT3", 63.47m));
            ListAtivos.Add(new Ativos("SQIA3", 22.80m));
            ListAtivos.Add(new Ativos("TOTS3", 33.17m));
            ListAtivos.Add(new Ativos("LWSA3", 24.28m));
            ListAtivos.Add(new Ativos("NGDR3", 06.65m));

            return ListAtivos;
        }

        public List<int> ListarAccount()
        {
            var listAccount = new List<int>();
            Random random = new Random();

            for (int i = 0; i < 18; i++)
            {
                listAccount.Add(random.Next(1000, 9999));
            }

            return listAccount;
        }
    }
}
