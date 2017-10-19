using System.Collections.Generic;

namespace PCS_Forms.DataOut
{
   public class DataResult
    {
        public int Id { get; private set; }
        public string Meaning { get; private set; }

        public DataResult(int id, string meaning)
        {
            this.Id = id;
            this.Meaning = meaning;
        }
    }
}
