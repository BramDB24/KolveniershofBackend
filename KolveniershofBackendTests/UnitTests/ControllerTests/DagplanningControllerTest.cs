using kolveniershofBackend.Controllers;
using kolveniershofBackend.DTO;
using kolveniershofBackend.Enums;
using kolveniershofBackend.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace KolveniershofBackendTests.UnitTests.ControllerTests
{
    public class DagplanningControllerTest
    {
        Mock<IGebruikerRepository> mockGebruikers = new Mock<IGebruikerRepository>();

        Mock<IAtelierRepository> mockAteliers = new Mock<IAtelierRepository>();

        Mock<IDagPlanningTemplateRepository> mockDagplanning = new Mock<IDagPlanningTemplateRepository>();


        //[Fact]
        //public void GetDagplanningFromDatum()
        //{
        //    mockDagplanning.Setup(x => x.GetTemplateByWeeknummerEnDagnummer(It.IsAny<int>(), It.IsAny<Weekdag>())).Returns(new DagPlanning() { DagAteliers = new List<DagAtelier>(), Datum = DateTime.Today});
        //    mockDagplanning.Setup(x => x.GetByDatum(It.IsAny<DateTime>())).Returns(new DagPlanning() { DagAteliers = new List<DagAtelier>(), Datum = DateTime.Today });   

        //    var controller = new DagPlanningController(mockDagplanning.Object,mockGebruikers.Object, mockAteliers.Object);
        //    var response = controller.GetDagPlanning("2050-03-08");
        //    var value = response.Value;

        //    var dto = new DagplanningDTO() { DagAteliers = new List<DagAtelierDTO>(), Datum = DateTime.Today};
        //    Assert.Equal(dto.DagplanningId, value.DagplanningId);
        //}
    }
}
