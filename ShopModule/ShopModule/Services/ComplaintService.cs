using Complaints;
using ShopModule.Data;
using ShopModule.Models;
using ShopModule_ApiClasses.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopModule.Services
{
    public interface IComplaintService
    {
        ComplaintMessage AddComplaint(ComplaintMessage complaint);
        Complaint AcceptComplaint(Guid complaintId);
        Complaint RejectComplaint(Guid complaintId);
        List<ComplaintMessage> PendingComplaintsPaginated(int page, int pageSize);
        Complaint GetComplaint(Guid complaintId);

    }
    public class ComplaintService : IComplaintService
    {
        private readonly ShopModuleDbContext _context;
        public ComplaintService(ShopModuleDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a complaint to the shop database
        /// </summary>
        /// <param name="complaint">Complaint object to be added</param>
        /// <returns>Returns a given complaint on success and a null on error</returns>
        public ComplaintMessage AddComplaint(ComplaintMessage message)
        {
            if (Enum.IsDefined(typeof(CurrentComplaintStateMessage), message.status))
            {
                Complaint complaint = new Complaint(message);
                _context.Complaints.Add(complaint);
                bool added = _context.SaveChanges() == 1;
                if (added)
                {
                    NotifyClientComplaintPending();
                    return message;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets existing Complaints that has status set as pending in paginated fashion
        /// </summary>
        /// <returns>List of Complaint objects</returns>
        public List<ComplaintMessage> PendingComplaintsPaginated(int page, int pageSize)
        {
            List<ComplaintMessage> complaints = new List<ComplaintMessage>();
            foreach (var complaint in _context.Complaints)
            {
                if (complaint.CurrentStatus == Complaints.CurrentComplaintState.Pending.ToString())
                {
                    complaints.Add(complaint.Convert(StaticData.defaultConverter));
                }
            }
            return complaints.Skip(page * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// Set complaint state to Accepted and refund the money
        /// </summary>
        /// <param name="complaintId">Complaint primary key</param>
        /// <returns>Returns found Complaint object or null if such doesn't exist</returns>
        public Complaint AcceptComplaint(Guid complaintId)
        {
            return ChangeState(complaintId, Complaints.CurrentComplaintState.Accepted);
        }

        /// <summary>
        /// Set complaint state to Rejected
        /// </summary>
        /// <param name="complaintId">Complaint primary key</param>
        /// <returns>Returns found Complaint object or null if such doesn't exist</returns>
        public Complaint RejectComplaint(Guid complaintId)
        {
            return ChangeState(complaintId, Complaints.CurrentComplaintState.Rejected);
        }

        private Complaint ChangeState(Guid complaintId, Complaints.CurrentComplaintState state)
        {
            var res = _context.Complaints.Find(complaintId);
            if (res != null)
            {
                res.CurrentStatus = state.ToString();
                switch (state)
                {
                    case Complaints.CurrentComplaintState.Rejected:
                        NotifyClientComplaintRejected();
                        break;
                    case Complaints.CurrentComplaintState.Accepted:
                        NotifyClientComplaintAccepted();
                        Refund();
                        break;
                    case Complaints.CurrentComplaintState.Pending:
                        NotifyClientComplaintPending();
                        break;
                    default:
                        break;
                }
                _context.SaveChanges();
                return res;
            }
            else
            {
                return null;
            }
        }

        private void Refund()
        {
            // TODO
            // Send a notification to the payment service
        }

        private void NotifyClientComplaintPending()
        {
            // TODO
            // Notify Client Complaint Pending
        }

        private void NotifyClientComplaintAccepted()
        {
            // TODO
            // Notify Client Complaint Accepted
        }

        private void NotifyClientComplaintRejected()
        {
            // TODO
            // Notify Client Complaint Rejected
        }

        private void NotifyClientComplaintReceived()
        {
            // TODO
            // Notify client
        }

        /// <summary>
        /// Get a complaint from a database
        /// </summary>
        /// <param name="complaintId"></param>
        /// <returns>Returns complaint on success and null on failure</returns>
        public Complaint GetComplaint(Guid complaintId)
        {
            return _context.Complaints.Find(complaintId);
        }
    }
}
