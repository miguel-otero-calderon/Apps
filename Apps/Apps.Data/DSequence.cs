using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Entity;
using System.Data;

namespace Apps.Data
{
    public class DSequence:DData
    {
        public DataRow Select(ESequence sequence)
        {
            DaCommand command = new DaCommand("SequenceSelect");
            command.AddInParameter("@CodeSequence", DbType.String, sequence.CodeSequence);
            return ExecuteDataRow(command);
        }

        public void Insert(ESequence sequence)
        {
            DaCommand command = new DaCommand("SequenceInsert");
            command.AddInParameter("@CodeSequence", DbType.String, sequence.CodeSequence);
            command.AddInParameter("@Correlative", DbType.Int32, sequence.Correlative);
            ExecuteNonQuery(command);
        }

        public void Update(ESequence sequence)
        {
            DaCommand command = new DaCommand("SequenceUpdate");
            command.AddInParameter("@CodeSequence", DbType.String, sequence.CodeSequence);
            command.AddInParameter("@Correlative", DbType.Int32, sequence.Correlative);
            ExecuteNonQuery(command);
        }
    }
}
