using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Data;
using Apps.Entity;

namespace Apps.Business
{
    public class BSequence
    {
        private DSequence data = new DSequence();
        public int GetCorrelative(ESequence sequence)
        {
            int correlative = data.GetCorrelative(sequence);
            if (correlative == -1)
            {
                sequence.Correlative = 1;
                data.SetCorrelativo(sequence);
            }
            return correlative;
        }

        public void SetCorrelativo(ESequence sequence)
        {
            data.SetCorrelativo(sequence);
        }
    }
}
