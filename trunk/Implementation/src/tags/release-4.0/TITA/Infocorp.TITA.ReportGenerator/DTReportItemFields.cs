using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infocorp.TITA.ReportGenerator
{
    public class DTReportItemFileds
    {
        private string _category;
        private string _status;
        private int _count;


        public DTReportItemFileds(string category, string status)
        {
            _category = category;
            _status = status;
            _count = 0;
        }
        public string GetCategory()
        {
            return _category;
        }
        public string GetStatus()
        {
            return _status;
        }
        public int GetCount()
        {
            return _count;
        }
        public void AddReportFounded()
        {
            _count++;
        }
        
        public override bool Equals(object obj)
        {
            return (_status.Equals(((DTReportItemFileds)obj).GetStatus()) && _category.Equals(((DTReportItemFileds)obj).GetCategory()));
        }
    }    
}
