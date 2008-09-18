using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace infocorp.TITA.DataBasesAcces
{
    class Program
    {
        static void Main(string[] args)
        {
            DataContract dc = new DataContract("contrato3", "marcos", "mussio");
            IDataBaseAcces da = new DataBaseAcces();
            //da.AddContract(dc);

            //da.DeleteContract(dc.idContract);
            /*List<Contracts> lis = da.ContractList();
            foreach (var iter in lis)
                Console.WriteLine(iter.id_contract);
            Console.ReadLine();*/
            /*string salida = da.ContractSite(dc.idContract);
            Console.WriteLine(salida);
            Console.ReadLine();*/
            da.ModifyContract(dc);
        }
    }
}
