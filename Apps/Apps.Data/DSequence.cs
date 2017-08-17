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
        public int GetCorrelative(ESequence sequence)
        {
            DaCommand command = new DaCommand("SequenceGetSequence");
            command.AddInParameter("@CodeSequence", DbType.String, sequence.CodeSequence);
            object value = ExecuteScalar(command);
            if (value != null && value != DBNull.Value)
                return Convert.ToInt32(value);
            else
                return -1;
        }

        public void SetCorrelativo(ESequence sequence)
        {
            DaCommand command = new DaCommand("SequenceSetSequence");
            command.AddInParameter("@CodeSequence", DbType.String, sequence.CodeSequence);
            command.AddInParameter("@Correlative", DbType.Int32, sequence.Correlative);
            ExecuteNonQuery(command);
        }
    }
}
