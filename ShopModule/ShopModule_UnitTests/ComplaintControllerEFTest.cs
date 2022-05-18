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
            var mockService = new Mock<IComplaintService>();

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

            mockService.Setup(x => x.PendingComplaints())
                .Returns(new List<ShopModule_ApiClasses.Messages.ComplaintMessage>
                {
                    testComplaint2.Convert(StaticData.defaultConverter)
                });

            var complaints = mockService.Object.PendingComplaints();

            Assert.Equal(testComplaint2.Convert(StaticData.defaultConverter).complaintId, complaints.ElementAt(0).complaintId);
        }

        [Fact]
        public void AcceptComplaintTest()
        {
            var mockOrderSet = new Mock<DbSet<Complaint>>();
            var mockService = new Mock<IComplaintService>();

            var mockContext = new Mock<ShopModuleDbContext>();
            mockContext.Setup(x => x.Complaints).Returns(mockOrderSet.Object);

            var service = new ComplaintService(mockContext.Object);

            var compId = Guid.NewGuid();

            var testComplaint = new Complaint
            {
                CurrentStatus = CurrentComplaintState.Pending,
                Id = compId,
                Text = "testcomplaint"
            };

            var resComplaint = new Complaint
            {
                CurrentStatus = CurrentComplaintState.Accepted,
                Id = compId,
                Text = "testcomplaint"
            };

            service.AddComplaint(testComplaint);

            mockService.Setup(x => x.AcceptComplaint(compId))
                .Returns(resComplaint);

            var complaint = mockService.Object.AcceptComplaint(testComplaint.Id);
            Assert.Equal(CurrentComplaintState.Accepted, complaint.CurrentStatus);
        }

        [Fact]
        public void RejectComplaintTest()
        {
            var mockOrderSet = new Mock<DbSet<Complaint>>();
            var mockService = new Mock<IComplaintService>();

            var mockContext = new Mock<ShopModuleDbContext>();
            mockContext.Setup(x => x.Complaints).Returns(mockOrderSet.Object);

            var service = new ComplaintService(mockContext.Object);

            var compId = Guid.NewGuid();

            var testComplaint = new Complaint
            {
                CurrentStatus = CurrentComplaintState.Pending,
                Id = compId,
                Text = "testcomplaint"
            };

            var resComplaint = new Complaint
            {
                CurrentStatus = CurrentComplaintState.Rejected,
                Id = compId,
                Text = "testcomplaint"
            };

            service.AddComplaint(testComplaint);

            mockService.Setup(x => x.RejectComplaint(compId))
                .Returns(resComplaint);

            var complaint = mockService.Object.RejectComplaint(testComplaint.Id);
            Assert.Equal(CurrentComplaintState.Rejected, complaint.CurrentStatus);
        }
    }
}
