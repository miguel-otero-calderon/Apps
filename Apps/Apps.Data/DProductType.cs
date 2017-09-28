using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Entity;

namespace Apps.Data
{
    public class DProductType: DData
    {
        public DataRow Select(EProductType eProductType)
        {
            DaCommand command = new DaCommand("ProductTypeSelect");
            command.AddInParameter("@CodeProductType", DbType.String, eProductType.CodeProductType);
            return ExecuteDataRow(command);
        }
        public void Insert(EProductType eProductType)
        {
            DaCommand command = new DaCommand("ProductTypeInsert");
            command.AddInParameter("@CodeProductType", DbType.String, eProductType.CodeProductType);
            command.AddInParameter("@Description", DbType.String, eProductType.Description);
            command.AddInParameter("@CodeSunatExistence", DbType.String, eProductType.CodeSunatExistence);
            command.AddInParameter("@State", DbType.Int16, eProductType.State);
            ExecuteNonQuery(command);
        }
        public void Update(EProductType eProductType)
        {
            DaCommand command = new DaCommand("ProductTypeUpdate");
            command.AddInParameter("@CodeProductType", DbType.String, eProductType.CodeProductType);
            command.AddInParameter("@Description", DbType.String, eProductType.Description);
            command.AddInParameter("@CodeSunatExistence", DbType.String, eProductType.CodeSunatExistence);
            command.AddInParameter("@State", DbType.Int16, eProductType.State);
            ExecuteNonQuery(command);
        }
        public void Delete(EProductType eProductType)
        {
            DaCommand command = new DaCommand("ProductTypeDelete");
            command.AddInParameter("@CodeProductType", DbType.String, eProductType.CodeProductType);
            ExecuteNonQuery(command);
        }

    }
}
