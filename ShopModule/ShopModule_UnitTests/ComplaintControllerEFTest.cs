using Complaints;
using Microsoft.EntityFrameworkCore;
using Moq;
using ShopModule.Complaints;
using ShopModule.Data;
using ShopModule.Location;
using ShopModule.Models;
using ShopModule.Orders;
using ShopModule.Services;
using ShopModule_ApiClasses.Messages;
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

            var testComplaint = new ComplaintMessage { status = CurrentComplaintState.Pending.ToString(),
                                                complaintId = Guid.NewGuid(), text="testcomplaint" };

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

            var testComplaint1 = new ComplaintMessage
            {
                status = CurrentComplaintState.Accepted.ToString(),
                complaintId = Guid.NewGuid(),
                text = "testcomplaint"
            };

            var testComplaint2 = new ComplaintMessage
            {
                status = CurrentComplaintState.Pending.ToString(),
                complaintId = Guid.NewGuid(),
                text = "testcomplaint"
            };

            var testComplaint3 = new ComplaintMessage
            {
                status = CurrentComplaintState.Rejected.ToString(),
                complaintId = Guid.NewGuid(),
                text = "testcomplaint"
            };

            service.AddComplaint(testComplaint1);
            service.AddComplaint(testComplaint2);
            service.AddComplaint(testComplaint3);

            mockService.Setup(x => x.PendingComplaintsPaginated(0, 1))
                .Returns(new List<ShopModule_ApiClasses.Messages.ComplaintMessage>
                {
                    testComplaint2
                });

            var complaints = mockService.Object.PendingComplaintsPaginated(0,1);

            Assert.Equal(testComplaint2.complaintId, complaints.ElementAt(0).complaintId);
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
                CurrentStatus = CurrentComplaintState.Pending.ToString(),
                Id = compId,
                Text = "testcomplaint"
            };

            var resComplaint = new Complaint
            {
                CurrentStatus = CurrentComplaintState.Accepted.ToString(),
                Id = compId,
                Text = "testcomplaint"
            };

            service.AddComplaint(testComplaint.Convert(StaticData.defaultConverter));

            mockService.Setup(x => x.AcceptComplaint(compId))
                .Returns(resComplaint);

            var complaint = mockService.Object.AcceptComplaint(testComplaint.Id);
            Assert.Equal(CurrentComplaintState.Accepted.ToString(), complaint.CurrentStatus);
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
                CurrentStatus = CurrentComplaintState.Pending.ToString(),
                Id = compId,
                Text = "testcomplaint"
            };

            var resComplaint = new Complaint
            {
                CurrentStatus = CurrentComplaintState.Rejected.ToString(),
                Id = compId,
                Text = "testcomplaint"
            };

            service.AddComplaint(testComplaint.Convert(StaticData.defaultConverter));

            mockService.Setup(x => x.RejectComplaint(compId))
                .Returns(resComplaint);

            var complaint = mockService.Object.RejectComplaint(testComplaint.Id);
            Assert.Equal(CurrentComplaintState.Rejected.ToString(), complaint.CurrentStatus);
        }
    }
}
