using System;

namespace Complaints
{
    public class Complaint
    {
        public Complaint()
        {
        }
        public int Id { get; set; }
        public string Text { get; set; }
        public virtual Complaint ComplaintFK { get; set; }
    }
}
