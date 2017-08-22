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
        private DSequence data = new DSequence();
        public int GetCorrelative(ESequence sequence)
        {
            ESequence _sequence = Select(sequence);
            if (_sequence == null)
            {
                sequence.Correlative = 1;
                Insert(sequence);

                _sequence = Select(sequence);                
            }
            return _sequence.Correlative;
        }

        public void SetCorrelativo(ESequence sequence)
        {
            ESequence _sequence = Select(sequence);
            if(_sequence == null)
            {
                _sequence = new ESequence(CodeSequence: sequence.CodeSequence, Correlative: sequence.Correlative);
                _sequence.Correlative = 1;
                Insert(_sequence);
            }
            else
            {
                _sequence.Correlative++;
                Update(_sequence);
            }             
        }

        public ESequence Select(ESequence sequence)
        {
            DataRow row = data.Select(sequence);
            if (row != null)
            {
                ESequence _sequence = new ESequence(row, row.GetColumns());
                return _sequence;
            }
            else
                return null;
        }

        public void Insert(ESequence sequence)
        {
            data.Insert(sequence);
        }

        public void Update(ESequence sequence)
        {
            data.Update(sequence);
        }

        public void Delete(ESequence sequence)
        {
            data.Delete(sequence);
        }
    }
}
