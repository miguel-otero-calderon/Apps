using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Data;
using Apps.Entity;
using Apps.Extension;
using System.Data;

namespace Apps.Business
{
    public class BSequence
    {
        private DSequence dSequence = new DSequence();
        public int GetCorrelative(ESequence sequence)
        {
            ESequence eSequence = Select(sequence);
            if (eSequence == null)
            {
                sequence.Correlative = 1;
                Insert(sequence);

                eSequence = Select(sequence);                
            }
            return eSequence.Correlative;
        }

        public void SetCorrelativo(ESequence sequence)
        {
            ESequence eSequence = Select(sequence);
            if(eSequence == null)
            {
                eSequence = new ESequence(
                    CodeSequence: sequence.CodeSequence, 
                    Correlative: sequence.Correlative);
                eSequence.Correlative = 1;
                Insert(eSequence);
            }
            else
            {
                eSequence.Correlative++;
                Update(eSequence);
            }             
        }

        public ESequence Select(ESequence sequence)
        {
            DataRow rowSequence = dSequence.Select(sequence);
            if (rowSequence != null)
            {
                ESequence eSequence = new ESequence(rowSequence, rowSequence.GetColumns());
                return eSequence;
            }
            else
                return null;
        }

        public void Insert(ESequence sequence)
        {
            dSequence.Insert(sequence);
        }

        public void Update(ESequence sequence)
        {
            dSequence.Update(sequence);
        }

        public void Delete(ESequence sequence)
        {
            dSequence.Delete(sequence);
        }
    }
}
