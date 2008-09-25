using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infocorp.TITA.DataTypes;

namespace infocorp.TITA.DataBasesAcces
{
    class Program
    {
        static void Main(string[] args)
        {
            
            
            DTContract dc = new DTContract();
            dc.ContractId = "contrato5";
            dc.Site = "marcos";
            dc.UserName = "modificado";
            DataBaseAcces da = new DataBaseAcces();
           //da.AddContract(dc);

            //da.DeleteContract(dc.ContractId);
            List<Contract> lis = da.ContractList();
            foreach (var iter in lis)
                Console.WriteLine(iter.id_contract);
            Console.ReadLine();
            /*string salida = da.ContractSite(dc.idContract);
            Console.WriteLine(salida);
            Console.ReadLine();*/
           //da.ModifyContract(dc);
        }
    }
}
