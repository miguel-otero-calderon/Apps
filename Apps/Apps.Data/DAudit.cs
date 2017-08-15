using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Entity;
using System.Data;

namespace Apps.Data
{
    public class DAudit:DData
    {
        public void Insert(EAudit entity)
        {
            DaCommand command = new DaCommand("AuditInsert");
            command.AddInParameter("@CodeCompany", DbType.String, entity.CodeCompany);
            command.AddInParameter("@CodeEntity", DbType.String, entity.CodeEntity);
            command.AddInParameter("@Code", DbType.String, entity.Code);
            command.AddInParameter("@Sequence", DbType.String, entity.Sequence);
            command.AddInParameter("@TypeEvent", DbType.String, entity.TypeEvent);
            command.AddInParameter("@UserRegister", DbType.String, entity.UserRegister);
            ExecuteCommand(command);
        }
    }
}
