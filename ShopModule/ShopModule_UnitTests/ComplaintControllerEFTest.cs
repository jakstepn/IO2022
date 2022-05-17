using Complaints;
using Microsoft.EntityFrameworkCore;
using Moq;
using ShopModule.Complaints;
using ShopModule.Data;
using ShopModule.Location;
using ShopModule.Models;
using ShopModule.Orders;
using ShopModule.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopModule_UnitTests
{
    public class ComplaintControllerEFTest
    {
        [Fact]
        public void CreateComplaintTest()
        {
            var mockOrderSet = new Mock<DbSet<Complaint>>();

            var mockContext = new Mock<ShopModuleDbContext>();
            mockContext.Setup(x => x.Complaints).Returns(mockOrderSet.Object);

            var service = new ComplaintService(mockContext.Object);

            var testComplaint = new Complaint { CurrentStatus = CurrentComplaintState.Pending,
                                                Id = Guid.NewGuid(), Text="testcomplaint" };

            service.AddComplaint(testComplaint);

            mockOrderSet.Verify(m => m.Add(It.IsAny<Complaint>()), Times.Once);
        }

        [Fact]
        public void GetPendingComplaintsTest()
        {
            var mockOrderSet = new Mock<DbSet<Complaint>>();

            var mockContext = new Mock<ShopModuleDbContext>();
            mockContext.Setup(x => x.Complaints).Returns(mockOrderSet.Object);

            var service = new ComplaintService(mockContext.Object);

            var testComplaint1 = new Complaint
            {
                CurrentStatus = CurrentComplaintState.Accepted,
                Id = Guid.NewGuid(),
                Text = "testcomplaint"
            };

            var testComplaint2 = new Complaint
            {
                CurrentStatus = CurrentComplaintState.Pending,
                Id = Guid.NewGuid(),
                Text = "testcomplaint"
            };

            var testComplaint3 = new Complaint
            {
                CurrentStatus = CurrentComplaintState.Rejected,
                Id = Guid.NewGuid(),
                Text = "testcomplaint"
            };

            service.AddComplaint(testComplaint1);
            service.AddComplaint(testComplaint2);
            service.AddComplaint(testComplaint3);

            var complaints = service.PendingComplaints();

            Assert.Contains(testComplaint2.Convert(StaticData.defaultConverter), complaints);
        }

        [Fact]
        public void AcceptComplaintTest()
        {
            var mockOrderSet = new Mock<DbSet<Complaint>>();

            var mockContext = new Mock<ShopModuleDbContext>();
            mockContext.Setup(x => x.Complaints).Returns(mockOrderSet.Object);

            var service = new ComplaintService(mockContext.Object);

            var testComplaint = new Complaint
            {
                CurrentStatus = CurrentComplaintState.Pending,
                Id = Guid.NewGuid(),
                Text = "testcomplaint"
            };

            service.AddComplaint(testComplaint);
            var complaint = service.AcceptComplaint(testComplaint.Id);
            Assert.Equal(CurrentComplaintState.Accepted, complaint.CurrentStatus);
        }

        [Fact]
        public void RejectComplaintTest()
        {
            var mockOrderSet = new Mock<DbSet<Complaint>>();

            var mockContext = new Mock<ShopModuleDbContext>();
            mockContext.Setup(x => x.Complaints).Returns(mockOrderSet.Object);

            var service = new ComplaintService(mockContext.Object);

            var testComplaint = new Complaint
            {
                CurrentStatus = CurrentComplaintState.Pending,
                Id = Guid.NewGuid(),
                Text = "testcomplaint"
            };

            service.AddComplaint(testComplaint);
            var complaint = service.RejectComplaint(testComplaint.Id);
            Assert.Equal(CurrentComplaintState.Rejected, complaint.CurrentStatus);
        }
    }
}
