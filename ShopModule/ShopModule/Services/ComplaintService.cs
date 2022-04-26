﻿using Complaints;
using ShopModule.Data;
using System.Collections.Generic;

namespace ShopModule.Services
{
    public interface IComplaintService
    {
        Complaint AddComplaint(Complaint complaint);
        Complaint AcceptComplaint(string complaintId);
        Complaint RejectComplaint(string complaintId);
        List<Complaint> PendingComplaints();

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
        public Complaint AddComplaint(Complaint complaint)
        {
            _context.Complaints.Add(complaint);
            return _context.SaveChanges() == 1 ? complaint : null;
        }

        /// <summary>
        /// Gets all of existing Complaints that has status set as pending
        /// </summary>
        /// <returns>List of Complaint objects</returns>
        public List<Complaint> PendingComplaints()
        {
            List<Complaint> res = new List<Complaint>();
            foreach (var complaint in _context.Complaints)
            {
                if (complaint.CurrentStatus == Complaints.CurrentState.Pending)
                {
                    res.Add(complaint);
                }
            }
            return res;
        }

        /// <summary>
        /// Set complaint state to Accepted and refund the money
        /// </summary>
        /// <param name="complaintId">Complaint primary key</param>
        /// <returns>Returns found Complaint object or null if such doesn't exist</returns>
        public Complaint AcceptComplaint(string complaintId)
        {
            return ChangeState(complaintId, Complaints.CurrentState.Accepted);
        }

        /// <summary>
        /// Set complaint state to Rejected
        /// </summary>
        /// <param name="complaintId">Complaint primary key</param>
        /// <returns>Returns found Complaint object or null if such doesn't exist</returns>
        public Complaint RejectComplaint(string complaintId)
        {
            return ChangeState(complaintId, Complaints.CurrentState.Rejected);
        }

        private Complaint ChangeState(string complaintId, Complaints.CurrentState state)
        {
            var res = _context.Complaints.Find(complaintId);
            if (res != null)
            {
                res.CurrentStatus = state;
                return _context.SaveChanges() == 1 ? res : null;
            }
            else
            {
                return null;
            }
        }
    }
}
