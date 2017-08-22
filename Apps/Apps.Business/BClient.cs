using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Entity;
using Apps.Data;
using Apps.Extension;
using System.Transactions;

namespace Apps.Business
{
    public class BClient
    {
        private DClient data = new DClient();
        BAudit audit = new BAudit();

        public EClient Insert(EClient client)
        {
            BSequence bSequence = new BSequence();
            ESequence sequence = new ESequence(client);
            int CodeClient = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                /*sequence*/
                CodeClient = bSequence.GetCorrelative(sequence);

                /*client*/
                client.CodeClient = CodeClient;
                data.Insert(client);

                /*audit*/
                client.Audit.TypeEvent = "Insert";
                audit.Insert(client.Audit);

                /*sequence*/
                sequence.Correlative++;
                bSequence.SetCorrelativo(sequence);

                scope.Complete();
            }
            return FindByCodeClient(CodeClient);
        }

        public EClient FindByCodeClient(int CodeClient)
        {
            DataRow row = data.FindByCodeClient(CodeClient);
            EClient client = new EClient(row, row.GetColumns());
            return client;
        }
    }
}
