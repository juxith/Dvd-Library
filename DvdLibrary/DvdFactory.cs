using DvdLibrary.Data.Interface;
using DvdLibrary.Data.Repos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DvdLibrary
{
    public static class DvdFactory
    {
        public static IDvdRepository Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "SampleData":
                    return new MockData();
                case "ADO":
                    return new ADORepository();
                case "EntityFramework":
                    return new EFRepository();
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
    }
}